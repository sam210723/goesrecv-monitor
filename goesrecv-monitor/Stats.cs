using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            int num;
            JObject json;

            // Averaging
            long timeAvg = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<decimal> freqAvg = new List<decimal>();

            // Continually receive data
            while (true)
            {
                // Receive nanomsg header
                num = s.Receive(dres, 8, SocketFlags.None);

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    return;
                }

                // Get message length
                int msglen = dres[7];

                // Receive message content
                num = s.Receive(dres, msglen, SocketFlags.None);

                // Log message bytes
                //Program.Log(logsrc, BitConverter.ToString(dres).Replace("-", ""));

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    return;
                }

                // Convert message bytes to ASCII
                string data = Encoding.ASCII.GetString(dres);

                // Trim message length and remove trailing new line
                data = data.Substring(0, msglen);
                data = data.TrimEnd('\n');

                // Parse JSON objects
                try
                {
                    json = JObject.Parse(data);
                    Program.Log(logsrc, string.Format("OK: {0}", data));
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    Program.Log(logsrc, string.Format("Error parsing JSON: {0}", data));
                    Program.Log(logsrc, e.ToString());
                    continue;
                }
                
                // kHz vs Hz
                decimal freq = Math.Round((decimal)json["frequency"], 2);

                // Write parsed data to log
                Program.Log(logsrc, string.Format("FREQUENCY: {0}", freq));

                // Add values to average lists
                freqAvg.Add(freq);

                // Calculate average every second
                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timeAvg > 1000)
                {
                    decimal avg = freqAvg.Average();
                    string avgStr;

                    if (avg > 999 || avg < -999)
                    {
                        avg = avg / 1000;
                        avg = Math.Round(avg, 2);
                        avgStr = avg.ToString() + " kHz";
                    }
                    else
                    {
                        avg = Math.Round(avg);
                        avgStr = avg + " Hz";
                    }

                    Program.Log(logsrc, string.Format("AVERAGE FREQUENCY: {0}", avgStr));

                    // Update main UI
                    Program.MainWindow.FrequencyOffset = avgStr;

                    freqAvg.Clear();
                    timeAvg = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
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
            int num;
            JObject json;

            // Averaging
            long timeAvg = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<int> vitAvg = new List<int>();
            List<int> rsAvg = new List<int>();

            // Continually receive data
            while (true)
            {
                // Receive nanomsg header
                num = s.Receive(dres, 8, SocketFlags.None);

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    return;
                }

                // Get message length
                int msglen = dres[7];

                // Receive message content
                num = s.Receive(dres, msglen, SocketFlags.None);

                // Log message bytes
                //Program.Log(logsrc, BitConverter.ToString(dres).Replace("-", ""));

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    return;
                }

                // Convert message bytes to ASCII
                string data = Encoding.ASCII.GetString(dres);

                // Trim message length and remove trailing new line
                data = data.Substring(0, msglen);
                data = data.TrimEnd('\n');
                
                // Parse JSON object
                try
                {
                    json = JObject.Parse(data);
                    Program.Log(logsrc, string.Format("OK: {0}", data));
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    Program.Log(logsrc, string.Format("Error parsing JSON: {0}", data));
                    Program.Log(logsrc, e.ToString());
                    continue;
                }

                // Signal lock indicator
                bool locked = (json["ok"] != null) ? ((int)json["ok"] != 0) : false;

                // Viterbi errors
                int vit = (int)json["viterbi_errors"];

                // Reed-Solomon errors
                int rs = (int)json["reed_solomon_errors"];
                rs = (rs > 0) ? rs : 0;

                // Write parsed data to log
                Program.Log(logsrc, string.Format("LOCK: {0}    VITERBI: {1}    RS: {2}", locked, vit, rs));

                // Add values to average lists
                vitAvg.Add(vit);
                rsAvg.Add(rs);

                // Calculate average every second
                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - timeAvg > 1000)
                {
                    // Signal quality
                    float vitLow = 30f;
                    float vitHigh = 1000f;
                    float sigQ = 100 - (((vit - vitLow) / (vitHigh - vitLow)) * 100);

                    // Cap signal quality value
                    sigQ = (sigQ > 100) ? 100 : sigQ;
                    sigQ = (sigQ < 0) ? 0 : sigQ;

                    Program.Log(logsrc, string.Format("AVERAGE QUALITY: {0}%    AVERAGE VITERBI: {1}    AVERAGE RS: {2}", sigQ, (int)vitAvg.Average(), (int)rsAvg.Average()));

                    // Update main UI
                    Program.MainWindow.SignalLock = locked;
                    Program.MainWindow.SignalQuality = (int)sigQ;
                    Program.MainWindow.ViterbiErrors = (int)vitAvg.Average();
                    Program.MainWindow.RSErrors = (int)rsAvg.Average();

                    // Update large stats UI
                    if (Program.BigWindow.Visible)
                    {
                        Program.BigWindow.SignalLock = locked;
                        Program.BigWindow.SignalQuality = (int)sigQ;
                        Program.BigWindow.ViterbiErrors = (int)vitAvg.Average();
                        Program.BigWindow.RSErrors = (int)rsAvg.Average();
                    }

                    // Reset average time
                    vitAvg.Clear();
                    rsAvg.Clear();
                    timeAvg = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
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
