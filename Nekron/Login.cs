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

namespace Nekron
{
    public partial class Login : Form
    {

        private string username = null, password = null; 
        public Login()
        {
            InitializeComponent();
            adminPnl.Visible = false;
            studentPnl.Visible = false;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            indicator.Height = ((Control)sender).Height - 1;
            parentPnl.Visible = true;
            adminPnl.Visible = false;
            studentPnl.Visible = false;
            parentPnl.BringToFront();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            indicator.Height = ((Control)sender).Height - 1;
            parentPnl.Visible = true;
            adminPnl.Visible = true;
            studentPnl.Visible = false;
            adminPnl.BringToFront();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            indicator.Height = ((Control)sender).Height - 1;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            indicator.Height = ((Control)sender).Height - 1;
            parentPnl.Visible = true;
            adminPnl.Visible = true;
            studentPnl.Visible = true;
            studentPnl.BringToFront();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            indicator.Top = ((Control)sender).Top;
            indicator.Height = ((Control)sender).Height - 1;
        }

        private void emp_Login_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void parentPnl_hover_MouseHover(object sender, EventArgs e)
        {
            this.parentPnl_usr_pass.isPassword = false;
        }

        private void Login_MouseHover(object sender, EventArgs e)
        {
            this.parentPnl_usr_pass.isPassword = true;
            this.adminPnl_usr_pass.isPassword = true;
        }

        private void adminPnl_hover_MouseHover(object sender, EventArgs e)
        {
            this.adminPnl_usr_pass.isPassword = false;
        }

        private void studentPnl_login_Click(object sender, EventArgs e)
        {
            //Student s=new Student();
            //s.Show();
        }

        private void adminPnl_login_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
            cmd.Connection = con;
            con.Open();
            DataTable dt = new DataTable();
            cmd.CommandText = "select username, a_password from TEST.admins where username = '" + adminPnl_usr_name.Text + "'";
            dt.Load(cmd.ExecuteReader());
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                username = Convert.ToString(row[0]);
                password = Convert.ToString(row[1]);
                MessageBox.Show(username + password);
                ++count;
            }

            if (count == 1 && adminPnl_usr_name.Text.Equals(username) && adminPnl_usr_pass.Text.Equals(password))
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else {
                MessageBox.Show("Enter valid Data");
            }
        }
    }
}
