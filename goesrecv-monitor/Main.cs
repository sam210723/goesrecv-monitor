using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            SetVersionLabel();
            ConfigConstellation();
        }

        /// <summary>
        /// Configures constellation display
        /// </summary>
        void ConfigConstellation()
        {
            // Set background colour
            pboxConstellation.BackColor = Color.FromArgb(255, 15, 15, 15);

            // Create base bitmap
            Bitmap bmp = new Bitmap(400, 400);
            Graphics g = Graphics.FromImage(bmp);

            // Draw divider line
            Pen p = new Pen(Brushes.DarkSlateGray, 1);
            g.DrawLine(p, 0, 200, 400, 200);

            // Show bitmap in picture box
            pboxConstellation.Image = bmp;
        }

        /// <summary>
        /// Sets the version label to assembly version
        /// </summary>
        void SetVersionLabel()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            labelVersion.Text = "v" + fvi.FileVersion;
        }
    }
}
