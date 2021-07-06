using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron
{
    public partial class SchedulingPnl : Form
    {
        public SchedulingPnl()
        {
            InitializeComponent();
        }

        private void plan_disc_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuShadowPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV Files(*.csv)|*.csv|XLXS Files(*.XLSX)|*.XLSX|All Files(*.*)|*.*";
            open.Title = "Browse a The Plan File";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string picpath = open.FileName.ToString();
                picPath.Text = "";
                picPath.Text = picpath;
              
                LoadImgChecked.Visible = true;
                LoadImgChecked.BringToFront();
            }
        }

        private void submit_plan_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
            cmd.Connection = con;
            con.Open();

            FileStream file = new FileStream(picPath.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(file);
            FileInfo info = new FileInfo(picPath.Text);
            byte[] imgData = br.ReadBytes((int)file.Length);

            cmd.CommandText = "insert into TEST.schedule values(:date_of_plan, :plan_file, :schedule_discription)";
            cmd.Parameters.Add(":date_of_plan", DateTime.Now.ToString("dd-MMM-yyyy"));
            cmd.Parameters.Add(":plan_file", imgData);
            cmd.Parameters.Add(":schedule_discription", (Object)imgData);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
