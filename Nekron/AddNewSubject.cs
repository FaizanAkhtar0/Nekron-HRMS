using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nekron.respository;


namespace Nekron
{
    public partial class AddNewSubject : Form
    {
        public AddNewSubject()
        {
            InitializeComponent();
        }

        private void bunifuShadowPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {
            if (this.bunifuiOSSwitch1.Value == true)
            {
                this.affectedClass.Enabled = true;
                this.bunifuiOSSwitch2.Value = false;
                this.affectedClasses.Enabled = false;
            }
            else
            {
                this.affectedClass.Enabled = false;
                this.bunifuiOSSwitch2.Value = true;
                this.affectedClasses.Enabled = true;
            }
        }

        private void bunifuiOSSwitch2_OnValueChange(object sender, EventArgs e)
        {
            if (this.bunifuiOSSwitch2.Value == true)
            {
                this.affectedClass.Enabled = false;
                this.bunifuiOSSwitch1.Value = false;
                this.affectedClasses.Enabled = true;
            }
            else
            {
                this.bunifuiOSSwitch2.Value = false;
                this.bunifuiOSSwitch1.Value = true;
                this.affectedClass.Enabled = true;
            }
        }

        private void AddNewSubject_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("Course Code : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Course Description : " + bunifuMaterialTextbox1.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Assigned Teacher: " + comboBox1.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("Affected Class : " + affectedClass.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 560));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 590));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 620));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 650));

        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                cmd.CommandText = "update TEST.COURSES  set COURSE_CODE = '" + usr_name.Text + "', C_DISCRIPTION = '"
                    + bunifuMaterialTextbox1.Text + "', T_ID = '" + comboBox1 + "', AFFECTED_SECTIONS = '" + affectedClass
                    +"'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Program couldn't Update " + usr_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                cmd.CommandText = " delete from TEST.COURSES where COURSE_CODE = '" + usr_name.Text + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Program couldn't Update " + usr_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
