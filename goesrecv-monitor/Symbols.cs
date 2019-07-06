using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Soft-symbol connection and data handler
/// </summary>
namespace goesrecv_monitor
{
    class Symbols
    {
        static Thread SymbolThread;
        static byte[] nninit = { 0x00, 0x53, 0x50, 0x00, 0x00, 0x21, 0x00, 0x00 };
        static byte[] nnires = { 0x00, 0x53, 0x50, 0x00, 0x00, 0x20, 0x00, 0x00 };

        /// <summary>
        /// Starts symbol processing thread
        /// </summary>
        public static void Start()
        {
            SymbolThread = new Thread(new ThreadStart(SymbolLoop));
            SymbolThread.Start();
        }

        /// <summary>
        /// Stops symbol processing thead
        /// </summary>
        public static void Stop()
        {
            if (SymbolThread != null)
            {
                SymbolThread.Abort();
            }
        }


        /// <summary>
        /// Symbol processing thread method
        /// </summary>
        static void SymbolLoop()
        {
            Console.WriteLine("[SYMBOL] Started");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(IP, SymbolPort);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            while (true)
            {
                Thread.Sleep(100);
            }
        }


        // Properties
        public static string IP { get; set; }
        static readonly int SymbolPort = 5001;

        /// <summary>
        /// Indicates if symbol processing thread is running
        /// </summary>
        public static bool Running
        {
            get
            {
                if (SymbolThread == null)
                {
                    return false;
                }
                else
                {
                    if (SymbolThread.ThreadState == ThreadState.Running || SymbolThread.ThreadState == ThreadState.WaitSleepJoin)
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
