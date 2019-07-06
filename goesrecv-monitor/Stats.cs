using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            while (true)
            {
                Thread.Sleep(10);
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
        public static IPAddress IP { get; set; }
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
