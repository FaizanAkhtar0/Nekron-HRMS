using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace Nekron
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }
        public void students()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                cmd.CommandText = "select * from HRM.students where status=1";

                con.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    try
                    {
                        OracleCommand cmd1 = new OracleCommand("select m.MAX_MARKS,m.OBTAINED_MARKS,s.S_ID,s.FULL_NAME,s.CLASS_NAME from HRM.Marks m,HRM.students s where (m.USERNAME=s.USERNAME AND m.USERNAME='" + usrname + "')", con);


                        // cmd1.CommandText = "select full_name from HRM.students where class_name='" + ClasscomboBox3.SelectedValue.ToString() + "' ";
                        //cmd1.CommandType = System.Data.CommandType.Text;
                        OracleDataAdapter da = new OracleDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        st_sid_clm.DataPropertyName = dt.Columns["s.S_ID"].ToString();
                        st_name_clm.DataPropertyName = dt.Columns["s.FULL_NAME"].ToString();
                        st_class_clm.DataPropertyName = dt.Columns["s.CLASS_NAME"].ToString();
                        st_omarks_clm.DataPropertyName = dt.Columns["m.OBTAINED_MARKS"].ToString();
                        st_maxmarks_clm.DataPropertyName = dt.Columns["m.OBTAINED_MARKS"].ToString();












                        dataGridView1.DataSource = dt;
                    }

                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                con.Close();










            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Student_Load(object sender, EventArgs e)
        {

        }

        private void view_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    students();
                }
                catch (Exception NullReferenceException) { MessageBox.Show("Please enter the Username again "); }
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
