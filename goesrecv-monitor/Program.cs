using System;
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
        ///  Gracefully exits the application
        /// </summary>
        public static void GracefulExit()
        {
            Console.WriteLine("Exiting...");

            Console.WriteLine("  - Stopping STATS thread");
            Stats.Stop();

            Console.WriteLine("  - Stopping SYMBOLS thread");
            Symbols.Stop();

            if (logf != null && logging)
            {
                Console.WriteLine("  - Closing log file");
                logf.Close();
            }

            Console.WriteLine("Process terminated");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Setup logging
            logging = Properties.Settings.Default.logging;
            if (logging)
            {
                // Create log file
                logf = File.CreateText("log.txt");

                // Redirect console output to file
                Console.SetOut(logf);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow = new Main();
            Application.Run(MainWindow);
        }
    }
}
