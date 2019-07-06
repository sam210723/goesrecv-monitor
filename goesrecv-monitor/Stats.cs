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
            DemodThread.Abort();
            DecoderThread.Abort();
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
                s.Connect(IP.ToString(), DemodPort);
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
            byte[] dres = new byte[1024];
            while (true)
            {
                s.Receive(dres);

                // Convert to string and trim
                string data = Encoding.ASCII.GetString(dres);
                data = data.Substring(8, data.IndexOf('\n'));
                data = data.TrimEnd('\0').TrimEnd('\n');

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Decoder statistics thread method
        /// </summary>
        static void DecoderLoop()
        {
            Console.WriteLine("[DECODER] Started");

            while (true)
            {
                Thread.Sleep(10);
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
