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
    public partial class AddTestResults : Form
    {
        public AddTestResults()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox6_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("Section Name : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Course Code : " + bunifuMaterialTextbox6.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Course name : " + bunifuMaterialTextbox5.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("Exam Name : " + bunifuMaterialTextbox2.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("Max Marks : " + bunifuMaterialTextbox3.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("Min Marks : " + bunifuMaterialTextbox4.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 410));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 470));

        }

        private void usr_name_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
