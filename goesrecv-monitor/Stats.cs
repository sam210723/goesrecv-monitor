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
            int num;
            JObject json;

            // Continually receive data
            while (true)
            {
                // Receive nanomsg header
                num = s.Receive(dres, 8, SocketFlags.None);

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                
                    // Reset UI and alert user
                    Program.MainWindow.ResetUI();
                    System.Windows.Forms.MessageBox.Show("Lost connection to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    // Stop all threads
                    Stop();
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
                Program.Log(logsrc, string.Format("FREQUENCY: {0}", freqStr));

                // Update UI
                Program.MainWindow.FrequencyOffset = freqStr;
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

                // Signal quality
                int vit = (int)json["viterbi_errors"];
                float vitLow = 30f;
                float vitHigh = 1000f;
                float sigQ = 100 - (((vit - vitLow) / (vitHigh - vitLow)) * 100);

                // Cap signal quality value
                sigQ = (sigQ > 100) ? 100 : sigQ;
                sigQ = (sigQ < 0) ? 0 : sigQ;

                // Reed-Solomon errors
                int rs = (int)json["reed_solomon_errors"];
                rs = (rs > 0) ? rs : 0;

                // Write parsed data to log
                Program.Log(logsrc, string.Format("LOCK: {0}    QUALITY: {1}%    VITERBI: {2}    RS: {3}", locked, sigQ, vit, rs));

                // Update UI
                Program.MainWindow.SignalLock = locked;
                Program.MainWindow.SignalQuality = (int)sigQ;
                Program.MainWindow.ViterbiErrors = vit;
                Program.MainWindow.RSErrors = rs;
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
