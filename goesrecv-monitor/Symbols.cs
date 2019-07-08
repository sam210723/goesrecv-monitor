using System;
using System.Collections.Generic;
using System.Drawing;
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
                Console.WriteLine("[SYMBOL] Stopping");
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
                // Connect socket
                s.Connect(IP, SymbolPort);
                Console.WriteLine("[SYMBOL] Connected to {0}:{1}", IP, SymbolPort.ToString());

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
                byte[] res = new byte[8];
                int bytesRec = s.Receive(res);
                if (res.SequenceEqual(nnires))
                {
                    Console.WriteLine("[SYMBOL] Nanomsg OK");
                }
                else
                {
                    string resHex = BitConverter.ToString(res);
                    Console.WriteLine("[SYMBOL] Nanomsg error: {0} (Expected: {1})", resHex, BitConverter.ToString(nnires));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[SYMBOL] Failed to connect");
                Stop();
                return;
            }

            while (true)
            {
                byte[] dres = new byte[65536];
                int numbytes = s.Receive(dres);

                // Kill thread if no data received
                if (numbytes == 0)
                {
                    Console.WriteLine("[SYMBOL] No data");
                    Stop();
                    return;
                }

                // Loop through bytes 2 at a time, skipping first 8
                List<Point> points = new List<Point>();
                for (int i = 8; i < 2048; i = i + 2)
                {
                    sbyte symI = (sbyte) dres[i];
                    sbyte symQ = (sbyte) dres[i + 1];

                    // Ignore null values
                    if (symI != '\0' && symQ != '\0')
                    {
                        points.Add(new Point(symI, symQ));
                    }
                }

                // Update UI
                Program.MainWindow.DrawSymbols(points);

                Thread.Sleep(10);
            }
        }


        // Properties
        public static string IP { get; set; }
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
