using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron
{
    public partial class AddSections : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private bool flag;

        public AddSections()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            string con_str = "insert into TEST.sections values(:section_name, :assigned_room_no, :t_id, :affected_classes, :date_of_addition)";
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = con_str;

            DateTime dt = DateTime.Now;
            string dated = dt.ToString("dd-MMM-yyyy");

            cmd.Parameters.Add("section_name", section_name.Text);
            cmd.Parameters.Add("assigned_room_no", assigned_roomNo.Text);
            cmd.Parameters.Add("t_id", assigned_teacher.Text);
            if (flag) {
                cmd.Parameters.Add("affected_classes", affectedClass.Text);
            }else {
                cmd.Parameters.Add("affected_classes", affectedClasses.Text);
            }
            cmd.Parameters.Add("date_of_addition", dated);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void bunifuiOSSwitch2_Click(object sender, EventArgs e)
        {
            if (!(bunifuiOSSwitch2.Value == true))
            {
                bunifuiOSSwitch2.Value = true;
                flag = false;
            }
            affectedClasses.Enabled = true;
            bunifuiOSSwitch1.Value = false;
            affectedClass.Enabled = false;
        }

        private void bunifuiOSSwitch1_Click(object sender, EventArgs e)
        {
            if(!(bunifuiOSSwitch1.Value == true)) {
                bunifuiOSSwitch1.Value = true;
                flag = true;
            }
            affectedClass.Enabled = true;
            bunifuiOSSwitch2.Value = false;
            affectedClasses.Enabled = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Section Name : " + section_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("AssignedRoom : " + assigned_roomNo.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("Assigned Teacher: " + assigned_teacher.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("Affected Class : " + affectedClass.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 610));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 640));
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                cmd.CommandText = "update TEST.SECTIONS  set SECTION_NAME = '" + section_name.Text + "', ASSIGNED_ROOM_NO = '"
                    + assigned_roomNo.Text + "', T_ID = '" + assigned_teacher + "', AFFECTED_CLASSES = '" + affectedClass
                    + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Program couldn't Update " + section_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void section_name_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                cmd.CommandText = " delete from TEST.COURSES where SECTION_NAME = '" + section_name.Text + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Program couldn't Update " + section_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
