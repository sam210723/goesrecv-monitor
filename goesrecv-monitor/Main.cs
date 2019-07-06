﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            // UI initialisation methods
            SetVersionLabel();
            InitConstellation();

            // Set control colours
            btnConnct.BackColor = Color.FromArgb(255, 30, 30, 30);
            textIP.BackColor = Color.FromArgb(255, 30, 30, 30);
        }

        /// <summary>
        /// Configures constellation display
        /// </summary>
        void InitConstellation()
        {
            // Set background colour
            pboxConstellation.BackColor = Color.FromArgb(255, 15, 15, 15);

            // Create base bitmap
            Bitmap bmp = new Bitmap(400, 400);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw divider line
            Pen p = new Pen(Brushes.DarkSlateGray, 1);
            Point p1 = new Point(0, pboxConstellation.Height / 2);
            Point p2 = new Point(pboxConstellation.Width, pboxConstellation.Height / 2);
            g.DrawLine(p, p1, p2);

            // Draw test points
            Brush symBrush = Brushes.Yellow;
            int symWidth = 4;
            Point sym0 = new Point((pboxConstellation.Width / 2) - (symWidth / 2), (pboxConstellation.Height / 4) - (symWidth / 2));
            Point sym1 = new Point((pboxConstellation.Width / 2) - (symWidth / 2), ((pboxConstellation.Height / 4) * 3) - (symWidth / 2));
            g.FillEllipse(symBrush, sym0.X, sym0.Y, symWidth, symWidth);
            g.FillEllipse(symBrush, sym1.X, sym1.Y, symWidth, symWidth);

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
            labelVersion.BackColor = Color.FromArgb(255, 15, 15, 15);
        }

        /// <summary>
        /// Connect button click
        /// </summary>
        private void btnConnct_Click(object sender, EventArgs e)
        {
            if (Stats.Running)
            {
                Stats.Stop();
                btnConnct.Text = "Connect";
                btnConnct.ForeColor = Color.White;
            }
            else
            {
                Stats.IP = textIP.Text;
                Stats.Start();

                btnConnct.Text = "STOP";
                btnConnct.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Triggers graceful exit
        /// </summary>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.GracefulExit();
        }
    }
}
