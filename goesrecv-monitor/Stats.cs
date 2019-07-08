using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                Console.WriteLine("[DEMOD] Stopping\n[DECODER] Stopping");
                DemodThread.Abort();
                DecoderThread.Abort();
            }
        }


        /// <summary>
        /// Demodulator statistics thread method
        /// </summary>
        static void DemodLoop()
        {
            Console.WriteLine("[DEMOD] Started");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Connect socket
                s.Connect(IP, DemodPort);
                Console.WriteLine("[DEMOD] Connected to {0}:{1}", IP, DemodPort.ToString());

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
                byte[] res = new byte[8];
                s.Receive(res);
                if (res.SequenceEqual(nnires))
                {
                    Console.WriteLine("[DEMOD] Nanomsg OK");
                }
                else
                {
                    string resHex = BitConverter.ToString(res);
                    Console.WriteLine("[DEMOD] Nanomsg error: {0} (Expected: {1})", resHex, BitConverter.ToString(nnires));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[DEMOD] Failed to connect");

                // Reset UI and alert user
                Program.MainWindow.ResetUI();
                System.Windows.Forms.MessageBox.Show("Unable to connect to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                // Stop all threads
                Stop();
                return;
            }

            // Continually receive data
            while (true)
            {
                byte[] dres = new byte[5120];
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Console.WriteLine("[DEMOD] No data");

                    // Reset UI and alert user
                    Program.MainWindow.ResetUI();
                    System.Windows.Forms.MessageBox.Show("Lost connection to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    // Stop all threads
                    Stop();
                    return;
                }

                // Tidy up raw data
                string data;
                string line;
                try
                {
                    data = Encoding.ASCII.GetString(dres);      // Convert to ASCII
                    data = data.TrimEnd('\0');                  // Trim trailing null bytes
                    data = data.TrimEnd('\n');                  // Trim trailing newline
                    line = data.Substring(data.IndexOf('{'), data.IndexOf('}') + 1);

                    // Handle double open brace
                    if (line.StartsWith("{{\""))
                    {
                        line = line.Substring(1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[DEMOD] Error trimming raw data");
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
                    Console.WriteLine("[DEMOD] Error parsing JSON: {0}", e.ToString());
                    Console.WriteLine("[DEMOD] {0}", line);
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
            Console.WriteLine("[DECODER] Started");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Connect socket
                s.Connect(IP.ToString(), DecoderPort);
                Console.WriteLine("[DECODER] Connected to {0}:{1}", IP, DecoderPort.ToString());

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
                byte[] res = new byte[8];
                int bytesRec = s.Receive(res);
                if (res.SequenceEqual(nnires))
                {
                    Console.WriteLine("[DECODER] Nanomsg OK");
                }
                else
                {
                    string resHex = BitConverter.ToString(res);
                    Console.WriteLine("[DECODER] Nanomsg error: {0} (Expected: {1})", resHex, BitConverter.ToString(nnires));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[DECODER] Failed to connect");
                return;
            }

            // Continually receive data
            while (true)
            {
                byte[] dres = new byte[1024];
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Console.WriteLine("[DECODER] No data");
                    return;
                }

                // Tidy up raw data
                string data;
                try
                {
                    data = Encoding.ASCII.GetString(dres);      // Convert to ASCII
                    data = data.TrimEnd('\0');                  // Trim trailing null bytes
                    data = data.TrimEnd('\n');                  // Trim trailing newline
                }
                catch (Exception e)
                {
                    Console.WriteLine("[DECODER] Error trimming raw data");
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
                            Console.WriteLine("[DECODER] No JSON object found");
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
                    Console.WriteLine("[DECODER] Error parsing JSON: {0}", e.ToString());
                    Console.WriteLine("[DECODER] {0}", errLine);
                    continue;
                }

                // Signal lock indicator
                bool locked = (int)json[0]["ok"] != 0;

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
