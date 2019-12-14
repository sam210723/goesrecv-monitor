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
        static string logsrc = "PROGRAM";


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetupLog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log(logsrc, "Creating instance of Main()");
            MainWindow = new Main();

            Log(logsrc, "Running Main()");
            Application.Run(MainWindow);
        }


        /// <summary>
        /// Setup log file
        /// </summary>
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
            Log(logsrc, string.Format("goesrecv monitor v{0}", GetVersion()));

            string arch = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            Log(null, string.Format("{0} {1}" , Environment.OSVersion.ToString(), arch));
            
            Log(null, AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);

            Log(null, string.Format("{0} CPUs, {1} process", Environment.ProcessorCount, Environment.Is64BitProcess ? "64-bit" : "32-bit"));
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
                if (src != null)
                {
                    string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
                    time = string.Format("[{0}]", now);
                }
                else
                {
                    time = "".PadRight(25);
                }

                // Format src string
                if (src != null)
                {
                    src = string.Format("[{0}]", src).PadRight(10);
                }
                else
                {
                    src = "".PadRight(10);
                }

                // Build log string
                string s = string.Format("{0} {1}  {2}", time, src, msg);

                Console.WriteLine(s);
                logf.Flush();
            }
        }


        /// <summary>
        ///  Gracefully exits the application
        /// </summary>
        public static void GracefulExit()
        {
            Log(logsrc, "Graceful exit triggered");

            Log(logsrc, "Stopping STATS threads");
            Stats.Stop();

            Log(logsrc, "Stopping SYMBOL thread");
            Symbols.Stop();

            if (logf != null && logging)
            {
                Log(null, "------------------------------------------\n");
                logf.Close();
            }
        }


        /// <summary>
        /// Returns the current assembly version
        /// </summary>
        public static string GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
