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
    public partial class s_report : Form
    {
        public s_report()
        {
            InitializeComponent();
        }

       public void s_report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet2.STUDENTS' table. You can move, or remove it, as needed.
            this.STUDENTSTableAdapter.Fill(this.DataSet2.STUDENTS);

            this.reportViewer1.RefreshReport();
        }

        public void reportViewer1_Load(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
