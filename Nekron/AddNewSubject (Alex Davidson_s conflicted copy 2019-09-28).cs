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
            if (this.bunifuiOSSwitch1.Value == true) {
                this.affectedClass.Enabled = true;
                this.bunifuiOSSwitch2.Value = false;
                this.affectedClasses.Enabled = false;
            }
            else {
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
            else {
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
            e.Graphics.DrawString("Course Code : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 100));
            e.Graphics.DrawString("Course Description : " + bunifuMaterialTextbox1.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 130));
            e.Graphics.DrawString("Assigned Teacher: " + comboBox1.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 160));
            e.Graphics.DrawString("Affected Class : " + affectedClass.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 190));
            e.Graphics.DrawString("--------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 400));

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
