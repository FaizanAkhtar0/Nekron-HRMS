using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron
{
    public partial class ReportPnl : Form
    {
        public ReportPnl()
        {
            InitializeComponent();
        }

        private void bunifuShadowPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            s_report s = new s_report();
            s.ShowDialog();
            t_report t = new t_report();
            t.ShowDialog();
             
        }
    }
}
