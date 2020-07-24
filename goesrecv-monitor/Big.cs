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
    public partial class Big : Form
    {
        public Big()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Resets UI elements after disconnect
        /// </summary>
        public void ResetUI()
        {
            if (labelQuality.InvokeRequired)
            {
                labelQuality.Invoke((MethodInvoker)(() => {
                    this.BackColor = Color.Black;
                    labelQuality.Text = "----";
                    labelVitErr.Text = "----";
                    labelRsErr.Text = "----";
                }));
            }
            else
            {
                this.BackColor = Color.Black;
                labelQuality.Text = "----";
                labelVitErr.Text = "----";
                labelRsErr.Text = "----";
            }
        }


        #region Properties
        public bool SignalLock
        {
            set
            {
                if (value)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            this.BackColor = Color.Green;
                        }));
                    }
                    else
                    {
                        this.BackColor = Color.Green;
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            this.BackColor = Color.Red;
                        }));
                    }
                    else
                    {
                        this.BackColor = Color.Red;
                    }
                }
            }
        }

        public int SignalQuality
        {
            set
            {
                if (labelQuality.InvokeRequired)
                {
                    labelQuality.Invoke((MethodInvoker)(() =>
                    {
                        labelQuality.Text = value.ToString().PadLeft(3, ' ') + "%";
                    }));
                }
                else
                {
                    labelQuality.Text = value.ToString().PadLeft(3, ' ') + "%";
                }

            }
        }

        public int ViterbiErrors
        {
            set
            {
                if (labelVitErr.InvokeRequired)
                {
                    labelVitErr.Invoke((MethodInvoker)(() =>
                    {
                        labelVitErr.Text = value.ToString().PadLeft(4, ' ');
                    }));
                }
                else
                {
                    labelVitErr.Text = value.ToString().PadLeft(4, ' ');
                }

            }
        }

        public int RSErrors
        {
            set
            {
                if (labelRsErr.InvokeRequired)
                {
                    labelRsErr.Invoke((MethodInvoker)(() =>
                    {
                        labelRsErr.Text = value.ToString().PadLeft(4, ' ');
                    }));
                }
                else
                {
                    labelRsErr.Text = value.ToString().PadLeft(4, ' ');
                }

            }
        }
        #endregion
    }
}
