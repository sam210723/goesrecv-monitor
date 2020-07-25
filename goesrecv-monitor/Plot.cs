using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goesrecv_monitor
{
    public partial class Plot : Form
    {
        long last;
        bool ready = false;
        bool force = false;
        int[] durations = { 60, 120, 600, 1800, 3600, 21600, 43200, 86400 };

        public Plot()
        {
            InitializeComponent();

            // Default to 1 minute period
            Period = 0;
        }

        /// <summary>
        /// Get first set of data when form loads
        /// </summary>
        private void Plot_Load(object sender, EventArgs e)
        {
            force = true;
        }

        /// <summary>
        /// Updates plot with new data
        /// </summary>
        public void Update(int viterbi, int rs)
        {
            DateTime now = DateTime.Now;

            if (chartStats.InvokeRequired)
            {
                chartStats.Invoke((MethodInvoker)(() => {
                    // Remove oldest points
                    if (chartStats.Series[0].Points.Count > Points)
                    {
                        chartStats.Series[0].Points.RemoveAt(0);
                        chartStats.Series[1].Points.RemoveAt(0);
                        
                    }

                    // Add new points
                    chartStats.Series[0].Points.AddXY(now, viterbi);
                    chartStats.Series[1].Points.AddXY(now, rs);

                    // Update X axis start and end
                    chartStats.ChartAreas[0].AxisX.Minimum = now.AddSeconds(-durations[Period]).ToOADate();
                    chartStats.ChartAreas[0].AxisX.Maximum = now.ToOADate();
                }));
            }
            else
            {
                // Remove oldest points
                if (chartStats.Series[0].Points.Count > Points)
                {
                    chartStats.Series[0].Points.RemoveAt(0);
                    chartStats.Series[1].Points.RemoveAt(0);
                }

                // Add new points
                chartStats.Series[0].Points.AddXY(now, viterbi);
                chartStats.Series[1].Points.AddXY(now, rs);

                // Update X axis start and end
                chartStats.ChartAreas[0].AxisX.Minimum = now.AddSeconds(-durations[Period]).ToOADate();
                chartStats.ChartAreas[0].AxisX.Maximum = now.ToOADate();
            }

            // Update last data time
            last = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            // Clear new data force flag
            force = false;
        }
        
        /// <summary>
        /// Clear chart when period changes
        /// </summary>
        private void comboPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartStats.Series[0].Points.Clear();
            chartStats.Series[1].Points.Clear();

            // Set new data force flag
            force = true;
        }

        #region Properties
        /// <summary>
        /// Number of points to plot on 
        /// </summary>
        public int Points { get; } = 1000;

        /// <summary>
        /// Plot duration
        /// </summary>
        public int Period
        {
            get { return comboPeriod.SelectedIndex; }
            set { comboPeriod.SelectedIndex = value; }
        }

        /// <summary>
        /// Sampling interval in milliseconds
        /// </summary>
        public int Interval
        {
            get { return (durations[Period] * 1000) / Points; }
        }

        /// <summary>
        /// Indicates plot is ready for new data
        /// </summary>
        public bool ReadyForData
        {
            get
            {
                try
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)(() => {
                            ready = ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - last) > Interval);
                        }));
                    }
                    else
                    {
                        ready = ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - last) > Interval);
                    }
                    return ready || force;
                }
                catch (ObjectDisposedException)
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
