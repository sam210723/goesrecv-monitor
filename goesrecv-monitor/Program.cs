using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    static class Program
    {
        public static Main MainWindow;
        
        static bool Init()
        {
            Console.WriteLine("Initialising...");

            return true;
        }

        /// <summary>
        ///  Gracefully exits the application
        /// </summary>
        public static void GracefulExit()
        {
            Console.WriteLine("Exiting...");
            Stats.Stop();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Init();

            MainWindow = new Main();
            Application.Run(MainWindow);
        }
    }
}
