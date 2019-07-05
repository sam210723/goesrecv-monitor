using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    static class Program
    {
        private static void Initialise()
        {
            Console.WriteLine("Initialising...");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Initialise();

            Application.Run(new Main());
        }
    }
}
