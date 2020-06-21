using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace goesrecv_monitor
{
    /// <summary>
    /// Soft-symbol connection and data handler
    /// </summary>
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
                Program.Log("SYMBOL", "Stopping");
                SymbolThread.Abort();
            }
        }


        /// <summary>
        /// Symbol processing thread method
        /// </summary>
        static void SymbolLoop()
        {
            string logsrc = "SYMBOL";
            Program.Log(logsrc, "START");

            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Program.Log(logsrc, "Socket created");

            try
            {
                // Connect socket
                s.Connect(IP, SymbolPort);
                Program.Log(logsrc, string.Format("Connected to {0}:{1}", IP, SymbolPort.ToString()));

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
                Stop();
                return;
            }

            byte[] dres = new byte[65536];
            while (true)
            {
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");
                    Stop();
                    return;
                }

                // Update UI
                Program.MainWindow.DrawSymbols(dres);

                Thread.Sleep(10);
            }
        }


        // Properties
        public static string IP { get; set; }
        
        // 5001 = Quantisation output   (I only)
        // 5002 = Clock Recovery output (I and Q)
        static readonly int SymbolPort = 5002;

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
