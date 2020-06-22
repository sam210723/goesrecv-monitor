using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    class ConstellationPanel : Panel
    {
        // Globals
        private byte[] Symbols;
        private Brush SymbolBrush;
        private Point Center;
        private Pen LinePen;

        // Properties
        public Color SymbolColor { get; set; } = Color.FromArgb(128, Color.Yellow);
        public float SymbolScale { get; set; } = 1.75f;
        public int SymbolSize { get; set; } = 5;
        public Color LineColor { get; set; } = Color.DarkSlateGray;
        public int Order { get; set; } = 2;

        /// <summary>
        /// Custom Panel control for drawing BPSK constellation plots
        /// </summary>
        public ConstellationPanel()
        {
            // Stop render flicker by double buffering
            DoubleBuffered = true;

            // Setup brushes and pens
            SymbolBrush = new SolidBrush(SymbolColor);
            LinePen = new Pen(new SolidBrush(LineColor), 1);
        }

        /// <summary>
        /// Draws symbol points when the control is re-drawn
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Get graphics object
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Get panel center
            Center = new Point(Width / 2, Height / 2);

            // Draw symbol dividing lines
            DrawLines(g);

            // Skip symbols if none provided
            if (Symbols == null) { return; }

            for (int i = 8; i < 2048; i = i + 2)
            {
                sbyte symI = (sbyte) Symbols[i];
                sbyte symQ = (sbyte) Symbols[i + 1];

                // Ignore null values
                if (symI != '\0' && symQ != '\0')
                {
                    float sX = Center.X + (symI * SymbolScale) - (SymbolSize / 2);
                    float sY = Center.Y + (symQ * SymbolScale) - (SymbolSize / 2);
                    g.FillEllipse(SymbolBrush, sX, sY, SymbolSize, SymbolSize);
                }
            }
        }

        /// <summary>
        /// Draws symbols on the constellation plot
        /// </summary>
        /// <param name="s">List of Point objects representing symbols</param>
        public void DrawSymbols(byte[] s)
        {
            // Set global symbol list
            Symbols = s;

            // Invalidate control, causing it to be re-drawn with OnPaint()
            Invalidate();
        }
        
        /// <summary>
        /// Draw constellation lines
        /// </summary>
        protected void DrawLines(Graphics g) {
            // Draw BPSK line
            g.DrawLine(
                LinePen,
                new Point(Center.X + 1, 0),
                new Point(Center.X + 1, Height)
            );

            // Skip QPSK line if order is not 4
            if (Order != 4) { return; }

            // Draw QPSK line
            g.DrawLine(
                LinePen,
                new Point(0, Center.X),
                new Point(Width, Center.X)
            );
        }
    }
}
