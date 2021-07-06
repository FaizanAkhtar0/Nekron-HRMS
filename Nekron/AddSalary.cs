using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nekron.respository;

namespace Nekron
{
    public partial class AddSalary : Form
    {
        private string username = null;
        private int ID = 0;

        public void setVal(int id, string UserName) {
            this.username = UserName;
            this.ID = id;
        }

        public AddSalary()
        {
            InitializeComponent();
        }

        private void role_id_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel9_Click(object sender, EventArgs e)
        {

        }

        private void AddSalary_Load(object sender, EventArgs e)
        {
            usr_name.Text = this.username;
            usr_id.Text = Convert.ToString(ID);
            if(this.username == null) {
                PopulateEmployeeUserName();
                this.usr_name.Enabled = true;
                this.usr_id.Enabled = true;
            } else {
                this.usr_name.Enabled = false;
                this.usr_id.Enabled = false;
            }
        }

        private void PopulateEmployeeUserName()
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "select username from TEST.teachers";
            DataTable dt = new DataTable();
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows) {
                usr_name.Items.Add((string)row[0]);
            }
        }

        private void usr_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usr_name.Text.Equals("")) {
                MessageBox.Show("Username can not be empty.\nPlease select a valid username to continue...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (usr_name.Text.Equals("-Select-")) {
                MessageBox.Show("Please select a valid username to continue...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "select t_id from TEST.teachers where username = '" + usr_name.Text + "'";

                DataTable dt = new DataTable();
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                foreach (DataRow row in dt.Rows) {
                    usr_name.Items.Add((string)row[0]);
                }
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            int id = 0;
            double salary = 0.0d, totalSalary = 0.0d, deduction = 0.0d;
            try {
                id = Convert.ToInt32(usr_id.Text);
                salary = Convert.ToDouble(usr_basic_salary.Text);
                deduction = Convert.ToDouble(usr_deduction.Text);
                totalSalary = Convert.ToDouble(usr_total_payment.Text);
            }catch(Exception ex) {
                MessageBox.Show("BasicSalary, userID, Deducation and TotalSalary must contain numerical values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Reset();
                return;
            }
            Salaryinfo.recieveSalaryInfo(usr_name.Text, usr_payment_date.Value.Date.ToString("dd-MMM-yyyy"), usr_mode_of_payment.Text, usr_payment_mode_details.Text, deduction, totalSalary, id, salary);
            MessageBox.Show("Information has been saved!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset() {
            usr_name.Text = "-Select-";
            usr_id.Text = "-Select-";
            usr_basic_salary.Text = "";
            usr_payment_date.Value = DateTime.Now;
            usr_mode_of_payment.Text = "";
            usr_payment_mode_details.Text = "";
            usr_deduction.Text = "";
            usr_total_payment.Text = "";
        }

        private void update_Click(object sender, EventArgs e)
        {

            int id = 0;
            double salary = 0.0d, totalSalary = 0.0d, deduction = 0.0d;
            try {
                id = Convert.ToInt32(usr_id.Text);
                salary = Convert.ToDouble(usr_basic_salary.Text);
                deduction = Convert.ToDouble(usr_deduction.Text);
                totalSalary = Convert.ToDouble(usr_total_payment.Text);
            } catch (Exception ex) {
                MessageBox.Show("BasicSalary, userID, Deducation and TotalSalary must contain numerical values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Reset();
            }

            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "update TEST.teachers set salary_amount = '" + salary + "', payment_date = '" + usr_payment_date.Value.Date.ToString("dd-MMM-yyyy") + "', mode_of_payment = '" + usr_mode_of_payment.Text + "', payment_mode_details = '" + usr_payment_mode_details.Text + "', deduction_amount = '" + deduction + "', total_pay = '" + totalSalary + "' where username = '" + usr_name.Text + "' & usr_id = '" + usr_id.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }catch (Exception ex) {
                MessageBox.Show("Couldn't connect to database, program was unable to perform the update task!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void delete_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "delete from TEST.teachers where username = '" + usr_name.Text + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void generateSlip_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "select * from TEST.salary where username = '" + usr_name.Text + "'";
            DataTable dt = new DataTable();

            con.Open();
            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(dt);
            con.Close();
            salary_view.DataSource = dt;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
          
            double basicSalary = 0.0d, deduction = 0.0d;
            try {
                deduction = Convert.ToDouble(usr_deduction.Text);
                basicSalary = Convert.ToDouble(usr_basic_salary.Text);
                usr_total_payment.Text = Convert.ToString(basicSalary - deduction);
            } catch (Exception ex) {
                MessageBox.Show("BasicSalary, userID, Deducation and TotalSalary must contain numerical values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Reset();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("User Name : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 100));
            e.Graphics.DrawString("Employee ID : " + usr_id.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 130));
            e.Graphics.DrawString("Basic Salary: " + usr_basic_salary.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 160));
            e.Graphics.DrawString("Payment Date : " + usr_payment_date.ToString(), new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 190));
            e.Graphics.DrawString("--------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 400));

        }
    }
}
