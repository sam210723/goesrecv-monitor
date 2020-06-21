using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace goesrecv_monitor
{
    /// <summary>
    /// Statistics connection and data handler
    /// </summary>
    public static class Stats
    {
        static Thread DemodThread;
        static Thread DecoderThread;
        static byte[] nninit = { 0x00, 0x53, 0x50, 0x00, 0x00, 0x21, 0x00, 0x00 };
        static byte[] nnires = { 0x00, 0x53, 0x50, 0x00, 0x00, 0x20, 0x00, 0x00 };

        /// <summary>
        /// Starts statistics threads
        /// </summary>
        public static void Start()
        {
            // Create threads
            DemodThread = new Thread(new ThreadStart(DemodLoop));
            DecoderThread = new Thread(new ThreadStart(DecoderLoop));
            

            // Start threads
            DemodThread.Start();
            DecoderThread.Start();
        }

        /// <summary>
        /// Stops statistics threads
        /// </summary>
        public static void Stop()
        {
            if (DemodThread != null && DecoderThread != null)
            {
                Program.Log("DEMOD", "Stopping");
                DemodThread.Abort();

                Program.Log("DECODER", "Stopping");
                DecoderThread.Abort();
            }
        }


        /// <summary>
        /// Demodulator statistics thread method
        /// </summary>
        static void DemodLoop()
        {
            string logsrc = "DEMOD";
            Program.Log(logsrc, "START");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Program.Log(logsrc, "Socket created");

            try
            {
                // Connect socket
                s.Connect(IP, DemodPort);
                Program.Log(logsrc, string.Format("Connected to {0}:{1}", IP, DemodPort.ToString()));

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
                byte[] res = new byte[8];
                s.Receive(res);
                if (res.SequenceEqual(nnires))
                {
                    Program.Log(logsrc, "nanomsg OK");
                }
                else
                {
                    string resHex = BitConverter.ToString(res);
                    Program.Log(logsrc, string.Format("nanomsg error: {0} (Expected: {1})", resHex, BitConverter.ToString(nnires)));
                }
            }
            catch (Exception e)
            {
                Program.Log(logsrc, "Failed to connect");

                // Reset UI and alert user
                Program.MainWindow.ResetUI();
                System.Windows.Forms.MessageBox.Show("Unable to connect to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                // Stop all threads
                Stop();
                return;
            }

            byte[] dres = new byte[256];

            // Continually receive data
            while (true)
            {
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");

                    // Reset UI and alert user
                    Program.MainWindow.ResetUI();
                    System.Windows.Forms.MessageBox.Show("Lost connection to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    // Stop all threads
                    Stop();
                    return;
                }

                string data = Encoding.ASCII.GetString(dres);   // Convert to ASCII;
                string line;
                try
                {
                    // Tidy up raw data
                    data = data.Replace("\0", "");              // Remove null bytes
                    data = data.Replace("\n", "");              // Remove newlines
                    data = data.Replace("{{", "{");             // Remove double braces
                    data = data.Replace("|", "");               // Remove pipes (?)

                    // JSON object substring
                    line = data.Substring(data.IndexOf('{'), data.IndexOf('}') + 1);

                    // Write cleaned line to log
                    Program.Log(logsrc, string.Format("OK: {0}", line));
                }
                catch (Exception e)
                {
                    Program.Log(logsrc, "Error trimming raw data:");

                    // Print exception line-by-line
                    string[] elines = e.Message.Split('\r');
                    foreach (string l in elines) {
                        string ln = l.Replace("\r", "");
                        ln = ln.Replace("\n", "");

                        Program.Log(logsrc, ln);
                    }

                    // Write bad line to log
                    Program.Log(logsrc, string.Format("BAD: {0}", data));

                    // Skip parsing
                    continue;
                }

                // Parse JSON objects
                JObject json;
                try
                {
                    json = JObject.Parse(line);
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    Program.Log(logsrc, string.Format("Error parsing JSON: {0}", e.ToString()));
                    Program.Log(null, line);
                    continue;
                }

                // kHz vs Hz
                decimal freq = (decimal)json["frequency"];
                string freqStr;
                if (freq > 999 || freq < -999)
                {
                    freq = freq / 1000;
                    freq = Math.Round(freq, 2);
                    freqStr = freq.ToString() + " kHz";
                }
                else
                {
                    freq = Math.Round(freq);
                    freqStr = freq + " Hz";
                }

                // Write parsed data to log
                Program.Log(null, string.Format("FREQUENCY: {0}", freqStr));

                // Update UI
                Program.MainWindow.FrequencyOffset = freqStr;
                
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Decoder statistics thread method
        /// </summary>
        static void DecoderLoop()
        {
            string logsrc = "DECODER";
            Program.Log(logsrc, "START");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Program.Log(logsrc, "Socket created");

            try
            {
                // Connect socket
                s.Connect(IP.ToString(), DecoderPort);
                Program.Log(logsrc, string.Format("Connected to {0}:{1}", IP, DecoderPort.ToString()));

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
                byte[] res = new byte[8];
                int bytesRec = s.Receive(res);
                if (res.SequenceEqual(nnires))
                {
                    Program.Log(logsrc, "nanomsg OK");
                }
                else
                {
                    string resHex = BitConverter.ToString(res);
                    Program.Log(logsrc, string.Format("nanomsg error: {0} (Expected: {1})", resHex, BitConverter.ToString(nnires)));
                }
            }
            catch (Exception e)
            {
                Program.Log(logsrc, "Failed to connect");
                return;
            }

            byte[] dres = new byte[256];

            // Continually receive data
            while (true)
            {
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    return;
                }

                string data = Encoding.ASCII.GetString(dres);   // Convert to ASCII
                try
                {
                    // Tidy up raw data
                    data = data.Replace("\0", "");              // Remove null bytes
                    data = data.Replace("{{", "{");             // Remove double braces
                    data = data.Replace("|", "");               // Remove pipes (?)
                    data = data.TrimEnd('\n');                  // Remove trailing newline

                    // Write cleaned line to log
                    Program.Log(logsrc, string.Format("OK: {0}", data.Replace("\n", "")));
                }
                catch (Exception e)
                {
                    Program.Log(logsrc, string.Format("Error trimming raw data: {0}", e.Message.Replace("\r\n", ": ")));
                    Program.Log(logsrc, Encoding.ASCII.GetString(dres));
                    continue;
                }


                // Parse JSON objects
                List<JObject> json = new List<JObject>();
                string[] lines = data.Split('\n');
                string errLine = "";
                try
                {
                    // Loop through each JSON line
                    foreach (string l in lines)
                    {
                        // Parse Line
                        string line = l;
                        if (line.IndexOf('{') != -1)
                        {
                            // Trim leading bytes before JSON string
                            line = line.Substring(l.IndexOf('{'));
                        }
                        else
                        {
                            Program.Log(logsrc, "No JSON object found");
                            continue;
                        }

                        // Handle double open brace
                        if (line.StartsWith("{{\""))
                        {
                            line = line.Substring(1);
                        }

                        errLine = line;
                        json.Add(JObject.Parse(line));
                    }
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    Program.Log(logsrc, string.Format("Error parsing JSON: {0}", e.ToString()));
                    Program.Log(null, errLine);
                    continue;
                }

                // Signal lock indicator
                bool locked;
                if (json[0]["ok"] != null)
                {
                    locked = (int)json[0]["ok"] != 0;
                }
                else
                {
                    locked = false;
                }

                // Signal quality
                int vit = (int)json[0]["viterbi_errors"];
                float vitLow = 30f;
                float vitHigh = 1000f;
                float sigQ;
                sigQ = 100 - (((vit - vitLow) / (vitHigh - vitLow)) * 100);

                // Cap signal quality value
                if (sigQ > 100)
                {
                    sigQ = 100;
                }
                else if (sigQ < 0)
                {
                    sigQ = 0;
                }

                // Count RS errors
                int rs = 0;
                foreach (JObject j in json)
                {
                    if ((int)j["reed_solomon_errors"] != -1)
                    {
                        rs += (int)j["reed_solomon_errors"];
                    }
                }

                // Write parsed data to log
                Program.Log(null, string.Format("LOCK: {0}    QUALITY: {1}%    VITERBI: {2}    RS: {3}", locked, sigQ, vit, rs));

                // Update UI
                Program.MainWindow.SignalLock = locked;
                Program.MainWindow.SignalQuality = (int)sigQ;
                Program.MainWindow.ViterbiErrors = vit;
                Program.MainWindow.RSErrors = rs;

                Thread.Sleep(500);
            }
        }


        // Properties
        public static string IP { get; set; }
        static readonly int DemodPort = 6001;
        static readonly int DecoderPort = 6002;

        /// <summary>
        /// Indicates if statistics threads are running
        /// </summary>
        public static bool Running
        {
            get
            {
                if (DemodThread == null)
                {
                    return false;
                }
                else
                {
                    if (DemodThread.ThreadState == ThreadState.Running || DemodThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
