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
        public Plot()
        {
            InitializeComponent();

            comboPeriod.SelectedIndex = 1;
        }
    }
}
