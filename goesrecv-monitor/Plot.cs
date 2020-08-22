using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace goesrecv_monitor
{
    public partial class Plot : Form
    {
        long last;
        bool ready = false;
        bool force = false;
        int[] durations = { 60, 120, 600, 1800, 3600, 21600, 43200, 86400 };
        int periodIdx;

        public Plot()
        {
            InitializeComponent();

            // Get default period from settings
            Period = Properties.Settings.Default.period;
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
            // Enable CSV export and clear plot buttons when data is added
            if (btnExportCSV.InvokeRequired)
            {
                btnExportCSV.Invoke((MethodInvoker)(() => {
                    btnExportCSV.Enabled = true;
                    btnClearPlot.Enabled = true;
                }));
            }
            else
            {
                btnExportCSV.Enabled = true;
                btnClearPlot.Enabled = true;
            }

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
        /// Exports current plot data to CSV file
        /// </summary>
        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            // Get chart data points
            List<DataPoint> vit = chartStats.Series[0].Points.ToList<DataPoint>();
            List<DataPoint> rs  = chartStats.Series[1].Points.ToList<DataPoint>();

            // CSV string and header
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Timestamp,Viterbi,Reed-Solomon");

            // Loop through points
            for (int i = 0; i < vit.Count; i++)
            {
                // Get timestamp for row
                string timestamp = DateTime.FromOADate(vit[i].XValue).ToString("dd/MM/yy HH:mm:ss.fff");

                // Add timestamp and values to row
                csv.AppendFormat("{0},{1},{2}\n", timestamp, vit[i].YValues[0], rs[i].YValues[0]);
            }

            // Configure save dialog
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save CSV...";
            sfd.FileName = "data.csv";
            sfd.Filter = "CSV Files | *.csv";
            sfd.DefaultExt = "csv";
            sfd.InitialDirectory = Environment.CurrentDirectory;

            // Save file
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, csv.ToString());
                //MessageBox.Show(string.Format("Plot data saved to \"{0}\"", sfd.FileName), "Export Plot Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Update period of chart X axis
        /// </summary>
        private void comboPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            periodIdx = comboPeriod.SelectedIndex;

            // Set new data force flag
            force = true;

            // Save period setting
            Properties.Settings.Default.period = Period;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Clear data from plot
        /// </summary>
        private void btnClearPlot_Click(object sender, EventArgs e)
        {
            chartStats.Series[0].Points.Clear();
            chartStats.Series[1].Points.Clear();
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
            get { return periodIdx; }
            set {
                comboPeriod.SelectedIndex = value;
                periodIdx = value;
            }
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
