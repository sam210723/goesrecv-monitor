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
        private List<Point> Symbols;
        private Brush SymbolBrush;
        private Point Center;
        private Pen LinePen;
        private Point LineStart;
        private Point LineEnd;

        // Properties
        public Color SymbolColor { get; set; } = Color.FromArgb(128, Color.Yellow);
        public float SymbolScale { get; set; } = 1.75f;
        public int SymbolSize { get; set; } = 5;
        public Color LineColor { get; set; } = Color.DarkSlateGray;

        /// <summary>
        /// Custom Panel control for drawing BPSK constellation plots
        /// </summary>
        public ConstellationPanel()
        {
            // Stop render flicker by double buffering
            DoubleBuffered = true;

            SymbolBrush = new SolidBrush(SymbolColor);
        }

        /// <summary>
        /// Draws symbol points when the control is re-drawn
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Get graphics object
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw divider line
            g.DrawLine(LinePen, LineStart, LineEnd);

            // Skip symbols if none provided
            if (Symbols == null) { return; }

            foreach (Point sym in Symbols)
            {
                // Draw point
                float sX = Center.X + (sym.X * SymbolScale) - (SymbolSize / 2);
                float sY = Center.Y + (sym.Y * SymbolScale) - (SymbolSize / 2);
                g.FillEllipse(SymbolBrush, sX, sY, SymbolSize, SymbolSize);
            }
        }

        /// <summary>
        /// Draws symbols on the constellation plot
        /// </summary>
        /// <param name="s">List of Point objects representing symbols</param>
        public void DrawSymbols(List<Point> s)
        {
            // Set global symbol list
            Symbols = s;

            // Invalidate control, causing it to be re-drawn with OnPaint()
            Invalidate();
        }

        /// <summary>
        /// Calculates panel center on Resize event
        /// </summary>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            // Get panel center point
            Center = new Point(Width / 2, Height / 2);

            // Setup center line variables
            LineStart = new Point(Center.X + 1, 0);
            LineEnd = new Point(Center.X + 1, Height);
            LinePen = new Pen(new SolidBrush(LineColor), 1);
        }
    }
}
