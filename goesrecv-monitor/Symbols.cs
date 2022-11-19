﻿using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

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

            byte[] res = new byte[8];
            try
            {
                // Connect socket
                s.Connect(IP, SymbolPort);
                Program.Log(logsrc, string.Format("Connected to {0}:{1}", IP, SymbolPort.ToString()));

                // Send nanomsg init message
                s.Send(nninit);

                // Check nanomsg response
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
            byte[] buffer = new byte[65536];
            int num, remainingBytesToWrite, startReadingAt, totalBytes = 0, bytesBeforeHeader = 0;
            while (true)
            {
                // Receive message content
                num = s.Receive(buffer);

                // Kill thread if no data received
                if (num == 0)
                {
                    Program.Log(logsrc, "Connection lost/no data, killing thread");

                    // Reset UI and alert user
                    Program.MainWindow.ResetUI();
                    if (Program.BigWindow.Visible) { Program.BigWindow.ResetUI(); }
                    System.Windows.Forms.MessageBox.Show("Lost connection to goesrecv", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    // Stop all threads
                    Stop();
                    return;
                }

                //Parse nanomsg response to find headers and remove them
                remainingBytesToWrite = num;
                startReadingAt = 0;
                while (remainingBytesToWrite > bytesBeforeHeader)
                {
                    //Write Information before header
                    if (bytesBeforeHeader > 0)
                    {
                        Buffer.BlockCopy(buffer, startReadingAt, dres, totalBytes, bytesBeforeHeader);
                        totalBytes += bytesBeforeHeader;
                    }

                    //Get next nanomsg packet length
                    Array.Copy(buffer, bytesBeforeHeader + startReadingAt, res, 0, 8);
                    if (BitConverter.IsLittleEndian) Array.Reverse(res);
                    startReadingAt += bytesBeforeHeader + 8;
                    remainingBytesToWrite = num - startReadingAt;
                    bytesBeforeHeader = (int)BitConverter.ToUInt64(res, 0);
                }

                //No more headers in bytes we have; write the rest of the bytes
                Buffer.BlockCopy(buffer, startReadingAt, dres, totalBytes, remainingBytesToWrite);
                bytesBeforeHeader -= remainingBytesToWrite;

                // Update UI
                Program.MainWindow.DrawSymbols(dres);

                totalBytes = 0;
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
