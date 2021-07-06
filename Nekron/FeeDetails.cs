﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nekron.respository;

namespace Nekron
{
    public partial class FeeDetails : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private double tutionFee, labCharges, securityCharges, healthServices, TotalFee, avalibleConcession, finalFee, fine;
        private string dueDate;

        private void FeeDetails_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Description", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Charges", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(300,230 ));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("TuitionFee :                                   " + usr_tutionFee.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("LabCharges :                                   " + usr_labCharges.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("Security Charges:                              " + usr_securityCharges.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("health Services :                              " + usr_healthServices.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("Total Fee :                                    " + usr_totalFee.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 410));
            e.Graphics.DrawString("Available Concession :                         " + usr_avalibleConcession.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString("FinalFee :                                     " + usr_finalFee.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 470));
            var datestring = usr_DueDate.Value.ToShortDateString();
            e.Graphics.DrawString("DueDate :                                      " + datestring, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 500));
            e.Graphics.DrawString("Fine After Date :                              " + usr_fine.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 530));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 560));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 590));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 620));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 650));
        }

        private void usr_DueDate_onValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            try {
                avalibleConcession = Convert.ToDouble(usr_avalibleConcession.Text);
                double final = Convert.ToDouble(usr_totalFee.Text) - avalibleConcession;
                this.finalFee = final;
                usr_finalFee.Text = Convert.ToString(final);
            }catch (Exception ex) {
                MessageBox.Show("The above fields must not be empty and can only contain numberical characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_avalibleConcession.Text = "";
                usr_finalFee.Text = "";
                usr_tutionFee.Focus();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                this.tutionFee = Convert.ToDouble(usr_tutionFee.Text);
                this.labCharges = Convert.ToDouble(usr_labCharges.Text);
                this.securityCharges = Convert.ToDouble(usr_securityCharges.Text);
                this.healthServices = Convert.ToDouble(usr_healthServices.Text);


                double total = tutionFee + labCharges + securityCharges + healthServices;
                this.TotalFee = total;
                this.usr_totalFee.Text = Convert.ToString(total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The above fields must not be empty and can only contain numberical characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_tutionFee.Text = "";
                usr_labCharges.Text = "";
                usr_securityCharges.Text = "";
                usr_healthServices.Text = "";
                usr_totalFee.Text = "";
                usr_avalibleConcession.Text = "";
                usr_finalFee.Text = "";
                usr_tutionFee.Focus();
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            SendFeeDetails();
            



        }

        public void SendFeeDetails() {
            try {
                fine = Convert.ToDouble(usr_fine.Text);
                dueDate = usr_DueDate.Value.Date.ToString("dd-MMM-yyyy");
            }catch (Exception ex) {
                MessageBox.Show("Fine field must not be empty and can only contain numberical characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Feeinfo info = new Feeinfo();
            info.recieveInfo(this.tutionFee, this.labCharges, this.securityCharges, this.healthServices, this.TotalFee, this.avalibleConcession, this.finalFee, this.dueDate, this.fine);
            AddStudentDetails.getFeeDetails();
            MessageBox.Show("Fee Details Saved...", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        public FeeDetails()
        {

            InitializeComponent();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
