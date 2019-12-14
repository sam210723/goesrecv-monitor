using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    static class Program
    {
        public static Main MainWindow;

        public static StreamWriter logf;
        public static bool logging;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetupLog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow = new Main();
            Application.Run(MainWindow);
        }


        static void SetupLog()
        {
            // Get logging flag from settings
            logging = Properties.Settings.Default.logging;

            // If logging is enabled
            if (logging)
            {
                // Create log file
                try
                {
                    logf = File.CreateText("log.txt");
                }
                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show("Insufficient permission to create log file", "goesrecv monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                // Redirect console output to file
                Console.SetOut(logf);
            }

            // Write application info to log
            Log("PROGRAM", "APPLICATION STARTED");
        }


        /// <summary>
        /// Write to log file
        /// </summary>
        /// <param name="message">String to write to log</param>
        public static void Log(string src, string msg)
        {
            if (logf != null && logging)
            {
                // Get timestamp
                string time;
                if (src != "")
                {
                    string now = DateTime.Now.ToString("dd/MM/yyyy][HH:mm:ss.fff");
                    time = string.Format("[{0}]", now);
                }
                else
                {
                    time = "".PadRight(26);
                }

                // Format src string
                if (src != "")
                {
                    src = string.Format("[{0}]", src).PadRight(10);
                }
                else
                {
                    src = "".PadRight(10);
                }

                // Build log string
                string s = string.Format("{0}{1}{2}", time, src, msg);

                Console.WriteLine(s);
                logf.Flush();
            }
        }


        /// <summary>
        ///  Gracefully exits the application
        /// </summary>
        public static void GracefulExit()
        {
            Log("PROGRAM", "Stopping STATS threads");
            Stats.Stop();

            Log("PROGRAM", "Stopping SYMBOL thread");
            Symbols.Stop();

            if (logf != null && logging)
            {
                Log("PROGRAM", "Closing log file");
                logf.Close();
            }
        }


        /// <summary>
        /// Returns the current assembly version
        /// </summary>
        static string GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
