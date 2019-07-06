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
                int bytesRec = s.Receive(res);
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
                Console.WriteLine(e.ToString());
            }

            // Continually receive data
            while (true)
            {
                byte[] dres = new byte[5120];
                s.Receive(dres);

                // Convert to string and trim
                string data = Encoding.ASCII.GetString(dres);
                data = data.Substring(8, data.IndexOf('\n'));
                data = data.TrimEnd('\0').TrimEnd('\n');

                // Parse frequency value
                string freqStr = data.Substring(data.IndexOf("frequency") + 12);
                freqStr = freqStr.Substring(0, freqStr.IndexOf(","));
                decimal freq = Math.Round(Decimal.Parse(freqStr, System.Globalization.NumberStyles.Float));

                // kHz vs Hz
                string freqUIStr;
                if (freq > 999 || freq < -999)
                {
                    freq = freq / 1000;
                    freqUIStr = freq.ToString() + " kHz";
                }
                else
                {
                    freqUIStr = freq + " Hz";
                }

                // Update UI
                Program.MainWindow.FrequencyOffset = freqUIStr;

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
                Console.WriteLine(e.ToString());
            }

            // Continually receive data
            while (true)
            {
                byte[] dres = new byte[1024];
                s.Receive(dres);

                // Convert to string and trim
                string data = Encoding.ASCII.GetString(dres).TrimEnd('\0').TrimEnd('\n');

                // Get Virerbi error count
                string vitStr = data.Substring(data.IndexOf("viterbi_errors") + 17);
                vitStr = vitStr.Substring(0, vitStr.IndexOf(','));
                int vitErr = int.Parse(vitStr);

                // Get lock state
                bool locked;
                string lockStr = data.Substring(data.IndexOf("ok") + 5, 1);
                if (lockStr == "1")
                {
                    locked = true;
                }
                else
                {
                    locked = false;
                }

                // Split data into lines
                int rsErr = 0;
                string[] lines = data.Split('\n');
                foreach (string l in lines)
                {
                    // Parse Line
                    string rsStr = l.Substring(l.IndexOf("reed_solomon_errors") + 22);
                    rsStr = rsStr.Substring(0, rsStr.IndexOf(','));

                    if (rsStr != "-1")
                    {
                        rsErr += int.Parse(rsStr);
                    }
                }

                // Cap viterbi in range for signal quality
                float vitErrQ = vitErr;
                float vitLower = 30f;
                float vitUpper = 1000f;
                if (vitErr < vitLower)
                {
                    vitErrQ = vitLower;
                }
                else if (vitErr > vitUpper)
                {
                    vitErrQ = vitUpper;
                }

                // Calculate signal quality
                float sigQ = 100 - (((vitErrQ - vitLower) / (vitUpper - vitLower)) * 100);

                // Update UI
                Program.MainWindow.SignalLock = locked;
                Program.MainWindow.SignalQuality = (int)sigQ;
                Program.MainWindow.ViterbiErrors = vitErr;
                Program.MainWindow.RSErrors = rsErr;

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
