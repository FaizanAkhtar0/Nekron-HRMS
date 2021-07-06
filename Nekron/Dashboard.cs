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

namespace Nekron
{
    public partial class Dashboard : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private int adminPresentVal = 0, adminLeftVal = 0, adminBeginningVal = 0, empPresentVal = 0, empLeftVal = 0, empBeginningVal = 0, stuPresentVal = 0, stuLeftVal = 0, stuBeginningVal = 0;
        private double Total_PayRoll = 0, Total_Income = 0, Benefit_Income = 0;
        public Dashboard()
        {
            InitializeComponent();
            try
            {
                admin_panel.Visible = false;
                payroll_panel.Visible = false;
                /*            report_panel.Visible = false;
                            plan_panel.Visible = false;
                            absence_panel.Visible = false;
                            */



                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                DateTime[] Date_list = new DateTime[100];
                cmd.Connection = con;
                Bunifu.DataViz.WinForms.Canvas data = new Bunifu.DataViz.WinForms.Canvas();
                Bunifu.DataViz.WinForms.DataPoint point1 = new Bunifu.DataViz.WinForms.DataPoint(Bunifu.DataViz.WinForms.BunifuDataViz._type.Bunifu_spline);

                cmd.CommandText = "select distinct(registration_date) from TEST.admins ORDER BY registration_date";
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Date_list[i] = (Convert.ToDateTime(row[0]));

                    cmd.CommandText = "select count(a_id) from TEST.admins where registration_date = '" + Date_list[i].ToString("dd-MMM-yyyy") + "'";

                    DataTable dt1 = new DataTable();
                    dt1.Load(cmd.ExecuteReader());
                    foreach (DataRow row1 in dt1.Rows)
                    {
                        string number = Convert.ToString(row1[0]);

                        point1.addLabely(Date_list[i].ToString("dd-MMM-yyyy"), number);
                        data.addData(point1);
                    }
                    ++i;
                }


                Bunifu.DataViz.WinForms.DataPoint point2 = new Bunifu.DataViz.WinForms.DataPoint(Bunifu.DataViz.WinForms.BunifuDataViz._type.Bunifu_spline);

                cmd.CommandText = "select distinct(admission_date) from TEST.students ORDER BY admission_date";
                DataTable dt2 = new DataTable();
                dt2.Load(cmd.ExecuteReader());
                int ii = 0;
                foreach (DataRow row in dt2.Rows)
                {
                    Date_list[i] = (Convert.ToDateTime(row[0]));

                    cmd.CommandText = "select count(s_id) from TEST.students where admission_date = '" + Date_list[i].ToString("dd-MMM-yyyy") + "'";

                    DataTable dt3 = new DataTable();
                    dt3.Load(cmd.ExecuteReader());
                    foreach (DataRow row1 in dt3.Rows)
                    {
                        string number = Convert.ToString(row1[0]);

                        point2.addLabely(Date_list[i].ToString("dd-MMM-yyyy"), number);
                        data.addData(point2);
                    }
                    ++ii;
                }


                Bunifu.DataViz.WinForms.DataPoint point3 = new Bunifu.DataViz.WinForms.DataPoint(Bunifu.DataViz.WinForms.BunifuDataViz._type.Bunifu_spline);

                cmd.CommandText = "select distinct(date_of_joining) from TEST.teachers ORDER BY date_of_joining";
                DataTable dt4 = new DataTable();
                dt4.Load(cmd.ExecuteReader());
                int iii = 0;
                foreach (DataRow row in dt4.Rows)
                {
                    Date_list[i] = (Convert.ToDateTime(row[0]));

                    cmd.CommandText = "select count(t_id) from TEST.teachers where date_of_joining = '" + Date_list[i].ToString("dd-MMM-yyyy") + "'";

                    DataTable dt5 = new DataTable();
                    dt5.Load(cmd.ExecuteReader());
                    foreach (DataRow row1 in dt5.Rows)
                    {
                        string number = Convert.ToString(row1[0]);

                        point3.addLabely(Date_list[i].ToString("dd-MMM-yyyy"), number);
                        data.addData(point3);
                    }
                    ++iii;
                }

                bunifuCharts1.Render(data);
                con.Close();

                try
                {
                    CalculateAdminRate();
                    CalculateEmployeeRate();
                    CalculateStudentRate();
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Unable to load data from the database.", "Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry)
                    {
                        CalculateAdminRate();
                        CalculateEmployeeRate();
                        CalculateStudentRate();
                    }
                    else
                    {
                        MessageBox.Show("All fields will be initilized with default values", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                try
                {
                    fillVacancies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't calculate vacancies...", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } }

        private void fillVacancies()
        {try
            {
                admin_Vacancy.Text = Convert.ToString((adminLeftVal * 100) / adminBeginningVal) + "% - left: " + adminLeftVal;
                admin_vacancy_progress.Value = Convert.ToInt32((adminLeftVal * 100) / adminBeginningVal);
                employee_Vacancy.Text = Convert.ToString((empLeftVal * 100) / empBeginningVal) + "% - left: " + empLeftVal;
                employee_vacancy_progress.Value = Convert.ToInt32((empLeftVal * 100) / empBeginningVal);
                student_vacancy.Text = Convert.ToString((stuLeftVal * 100) / stuBeginningVal) + "% - left: " + stuLeftVal;
                student_vacancy_progress.Value = Convert.ToInt32((stuLeftVal * 100) / stuBeginningVal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateStudentRate()
        {try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                //Beginning of Student Rate Calculation
                DataTable dt_student_left = new DataTable();
                cmd.CommandText = "select count(s_id) from TEST.students where status = 0 AND admission_date >= (select trunc(max(admission_date),'MM') from TEST.students)";
                dt_student_left.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_student_left.Rows)
                {
                    stu_left_Val.Text = Convert.ToString(row[0]);
                    stuLeftVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_student_end = new DataTable();
                cmd.CommandText = "select count(s_id) from TEST.students where status = 1 AND admission_date  <= (select max(admission_date) from TEST.students)";
                dt_student_end.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_student_end.Rows)
                {
                    stu_end_Val.Text = Convert.ToString(row[0]);
                    stuPresentVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_student_begin = new DataTable();
                cmd.CommandText = "select count(s_id) from TEST.students where admission_date <= (select max(admission_date) from TEST.students)";
                dt_student_begin.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_student_begin.Rows)
                {
                    stu_beg_Val.Text = Convert.ToString(row[0]);
                    stuBeginningVal = Convert.ToInt32(row[0]);
                }

                try
                {
                    double stuRate = (this.stuLeftVal * 100) / this.stuBeginningVal;
                    studentRateProgress.Value = (int)(Math.Ceiling(stuRate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't convert Values");
                }

                DataTable dt_lastCheckStu = new DataTable();
                cmd.CommandText = "select max(admission_date) from TEST.students";
                dt_lastCheckStu.Load(cmd.ExecuteReader());


                foreach (DataRow row in dt_lastCheckStu.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(row[0]);
                    stu_status.Text = "Rating Status: Monthly (" + dateTime.Date.ToString("dd-MMM-yyyy") + ")";
                }
                //End of Student rate calculation

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateEmployeeRate()
        {
            try
            {

                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                //Beginning of Employee/Teacher Rate Calculation
                DataTable dt_employee_left = new DataTable();
                cmd.CommandText = "select count(t_id) from TEST.teachers where status = 0 AND date_of_joining >= (select trunc(max(date_of_joining),'MM') from TEST.teachers)";
                dt_employee_left.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_employee_left.Rows)
                {
                    emp_left_rr_val.Text = Convert.ToString(row[0]);
                    empLeftVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_employee_end = new DataTable();
                cmd.CommandText = "select count(t_id) from TEST.teachers where status = 1 AND date_of_joining  <= (select max(date_of_joining) from TEST.teachers)";
                dt_employee_end.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_employee_end.Rows)
                {
                    end_emp_rr_val.Text = Convert.ToString(row[0]);
                    empPresentVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_employee_begin = new DataTable();
                cmd.CommandText = "select count(t_id) from TEST.teachers where date_of_joining <= (select max(date_of_joining) from TEST.teachers)";
                dt_employee_begin.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_employee_begin.Rows)
                {
                    beg_emp_rr_val.Text = Convert.ToString(row[0]);
                    empBeginningVal = Convert.ToInt32(row[0]);
                }

                try
                {
                    double empRate = (this.empLeftVal * 100) / this.empBeginningVal;
                    employeeRatePrgress.Value = (int)(Math.Ceiling(empRate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't convert Values");
                }

                DataTable dt_lastCheckEmp = new DataTable();
                cmd.CommandText = "select max(date_of_joining) from TEST.teachers";
                dt_lastCheckEmp.Load(cmd.ExecuteReader());


                foreach (DataRow row in dt_lastCheckEmp.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(row[0]);
                    emp_status.Text = "Rating Status: Monthly (" + dateTime.Date.ToString("dd-MMM-yyyy") + ")";
                }
                //End of Employee rate calculation
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateAdminRate()
        {
            try
            {

                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                //Beginning of Admin Rate Calculation
                DataTable dt_admin_left = new DataTable();
                cmd.CommandText = "select count(a_id) from TEST.admins where status = 0 AND registration_date >= (select trunc(max(registration_date),'MM') from TEST.admins)";
                dt_admin_left.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_admin_left.Rows)
                {
                    admin_left_val.Text = Convert.ToString(row[0]);
                    adminLeftVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_admin_end = new DataTable();
                cmd.CommandText = "select count(a_id) from TEST.admins where status = 1 AND registration_date  <= (select max(registration_date) from TEST.admins)";
                dt_admin_end.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_admin_end.Rows)
                {
                    end_admin_val.Text = Convert.ToString(row[0]);
                    adminPresentVal = Convert.ToInt32(row[0]);
                }

                DataTable dt_admin_begin = new DataTable();
                cmd.CommandText = "select count(a_id) from TEST.admins where registration_date <= (select max(registration_date) from TEST.admins)";
                dt_admin_begin.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt_admin_begin.Rows)
                {
                    beg_admin_val.Text = Convert.ToString(row[0]);
                    adminBeginningVal = Convert.ToInt32(row[0]);
                }

                try
                {
                    double adminRate = (this.adminLeftVal * 100) / this.adminBeginningVal;
                    adminRateProgress.Value = (int)(Math.Ceiling(adminRate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't convert Values");
                }

                DataTable dt_lastCheck = new DataTable();
                cmd.CommandText = "select max(registration_date) from TEST.admins";
                dt_lastCheck.Load(cmd.ExecuteReader());


                foreach (DataRow row in dt_lastCheck.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(row[0]);
                    admin_status.Text = "Rating Status: Monthly (" + dateTime.Date.ToString("dd-MMM-yyyy") + ")";
                }
                //End of Admin rate calculation
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                admin_panel.Visible = false;
                  payroll_panel.Visible = false;
                  report_panel.Visible = false;
                  plan_panel.Visible = false;
                absence_panel.Visible = false;
                Sum_Pnl.BringToFront();
                bunifuGradientPanel2.BringToFront();
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                admin_panel.Visible = true;
                payroll_panel.Visible = false;
                report_panel.Visible = false;
                plan_panel.Visible = false;
                absence_panel.Visible = false; 
                admin_panel.BringToFront();
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                admin_panel.Visible = true;
                payroll_panel.Visible = true;
                    report_panel.Visible = false;
                     plan_panel.Visible = false;
                absence_panel.Visible = false;
                payroll_panel.BringToFront(); 
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                 admin_panel.Visible = true;
                   payroll_panel.Visible = true;
                   report_panel.Visible = true;
                   plan_panel.Visible = false;
                  absence_panel.Visible = false; 
                report_panel.BringToFront(); 
                bunifuGradientPanel2.BringToFront();
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                  admin_panel.Visible = true;
                  payroll_panel.Visible = true;
                  report_panel.Visible = true;
               plan_panel.Visible = true;
                absence_panel.Visible = false; 
                plan_panel.BringToFront(); 
                bunifuGradientPanel2.BringToFront();
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
                Sum_Pnl.Visible = true;
                   admin_panel.Visible = true;
                   payroll_panel.Visible = true;
                   report_panel.Visible = true;
                   plan_panel.Visible = true;
                   absence_panel.Visible = true;
                   absence_panel.BringToFront(); 
                bunifuGradientPanel2.BringToFront();
                panel1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addStudentDetails_Click(object sender, EventArgs e)
        {try
            {
                AddStudentDetails addStudentDetails = new AddStudentDetails();
                addStudentDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addSalaryDetails_Click(object sender, EventArgs e)
        {
            try
            {
                AddSalary addSalary = new AddSalary();
                addSalary.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addSections_Click(object sender, EventArgs e)
        {try
            {
                AddSections addSections = new AddSections();
                addSections.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addFeeDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FeeDetails AddFeeDetails = new FeeDetails();
                addFeeDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddPortals_Click(object sender, EventArgs e)
        {

        }

        private void RestoreDatabase_Click(object sender, EventArgs e)
        {

        }

        private void AddCourses_Click(object sender, EventArgs e)
        {try
            {
                AddNewSubject addNewSubject = new AddNewSubject();
                addNewSubject.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageResults_Click(object sender, EventArgs e)
        {
            try
            {
                AddTestResults addTestResults = new AddTestResults();
                addTestResults.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void adminPnlrefresh_btn_Click(object sender, EventArgs e)
        {try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                if (!(usr_type_admin_pnl.Text.Equals("-Select-")))
                {
                    if (usr_type_admin_pnl.Text.Equals("Admin"))
                    {
                        DataTable dt_admin = new DataTable();

                        if (usr_name_adminPnl.Text.Equals("all"))
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.admins where status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                        else
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.admins where username = '" + usr_name_adminPnl.Text + "' AND status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                    }

                    if (usr_type_admin_pnl.Text.Equals("Teacher"))
                    {
                        DataTable dt_admin = new DataTable();

                        if (usr_name_adminPnl.Text.Equals("all"))
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.teachers where status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                        else
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.teachers where username = '" + usr_name_adminPnl.Text + "' AND status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                    }

                    if (usr_type_admin_pnl.Text.Equals("Student"))
                    {
                        DataTable dt_admin = new DataTable();

                        if (usr_name_adminPnl.Text.Equals("all"))
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.students where status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                        else
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.students where username = '" + usr_name_adminPnl.Text + "' AND status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                    }

                    if (usr_type_admin_pnl.Text.Equals("Parent"))
                    {
                        DataTable dt_admin = new DataTable();

                        if (usr_name_adminPnl.Text.Equals("all"))
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.parents where status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                        else
                        {
                            if (!(usr_status.Text.Equals("-Select-")))
                            {
                                int statusVal = -1;
                                if (usr_status.Text.Equals("Enabled"))
                                {
                                    statusVal = 1;
                                }
                                else if (usr_status.Text.Equals("Disabled"))
                                {
                                    statusVal = 0;
                                }

                                cmd.CommandText = "select * from TEST.parents where username = '" + usr_name_adminPnl.Text + "' AND status = '" + statusVal + "'";
                                dt_admin.Load(cmd.ExecuteReader());
                                adminPnl_gridView.DataSource = dt_admin.DefaultView;
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackUpDatabase_Click(object sender, EventArgs e)
        {

        }

        private void refresh_btn_payrollPnl_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                if (!(usr_type_payrollPnl.Text.Equals("-Select-")))
                {
                    if (usr_type_payrollPnl.Text.Equals("Teacher"))
                    {
                        DataTable dt_Teacher = new DataTable();

                        if (usr_name_payrollPnl.Text.Equals("all"))
                        {
                            cmd.CommandText = "select t.t_id, t.username, t.full_name, t.contact_no, t.email, s.salary_amount, s.payment_date, s.mode_of_payment, s.payment_mode_details, s.deduction_amount, s.total_pay  from TEST.teachers t, TEST.salary s where t.t_id = s.usr_id";
                            dt_Teacher.Load(cmd.ExecuteReader());
                            payrollPnl_View.DataSource = dt_Teacher.DefaultView;
                        }
                        else
                        {
                            cmd.CommandText = "select t.username, t.full_name, t.contact_no, email, s.salary_amount, s.payment_date, s.mode_of_payment, s.payment_mode_details, s.deduction_amount, s.total_pay  from TEST.teachers t, TEST.salary s where t.username = '" + usr_name_payrollPnl.Text + "' AND s.username = '" + usr_name_payrollPnl.Text + "' AND t.t_id = s.usr_id";
                            dt_Teacher.Load(cmd.ExecuteReader());
                            payrollPnl_View.DataSource = dt_Teacher.DefaultView;
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                AddSalary addSalary = new AddSalary();
                addSalary.Show();  //PayrollPnl
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            try
            {
                AddEmployeeTeacherDetails teacherDetails = new AddEmployeeTeacherDetails();
                teacherDetails.Show(); //PayrollPnl
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            try
            {
                FeeDetails feeDetails = new FeeDetails();
                feeDetails.Show(); //PayrollPnl
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void start_distribution_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (report_usr_type.Text.Equals("Student")) {
                s_report report = new s_report();
                report.ShowDialog();
            }else if (report_usr_type.Text.Equals("Teacher")) {
                // Tecaher report genetator
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {

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

        private void submit_absence_Click(object sender, EventArgs e)
        {
            //Attendance attendance = new Attendance();
            //attendance.Show();
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();
                DataTable dt = new DataTable();
                cmd.CommandText = "select * from TEST.attandance where t_username = '" + abscencePnl_usr_name.Text + "'";
                dt.Load(cmd.ExecuteReader());

                abscense_View.DataSource = dt.DefaultView;

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();

                    OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                    cmd.Connection = con;
                    con.Open();

                    DataTable dt = new DataTable();
                    if (report_usr_type.Text.Equals("Student")) {
                        cmd.CommandText = "select * from TEST.students where username = '" + report_usr_name.Text + "'";
                        dt.Load(cmd.ExecuteReader());
                    }else if (report_usr_type.Text.Equals("Teacher")) {
                        cmd.CommandText = "select * from TEST.teachers where username = '" + report_usr_name.Text + "'";
                        dt.Load(cmd.ExecuteReader());
                    }

                    report_view.DataSource = dt.DefaultView;
                    
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void refreshBtn_Calculate_PayRoll_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                cmd.Connection = con;
                con.Open();

                DataTable dt_PayRoll_Cal = new DataTable();

                cmd.CommandText = "select username, salary_amount, deduction_amount, total_pay from TEST.salary";
                dt_PayRoll_Cal.Load(cmd.ExecuteReader());
                payrollPnl_Cal_View.DataSource = dt_PayRoll_Cal.DefaultView;
                con.Close();

                cmd.CommandText = "select total_pay from TEST.salary";
                con.Open();

                DataTable dt_Final_PayRoll = new DataTable();
                dt_Final_PayRoll.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt_Final_PayRoll.Rows)
                {
                    this.Total_PayRoll += Convert.ToDouble(row[0]);
                }

                this.payRollPnlTotalPay.Text = this.Total_PayRoll.ToString();
                con.Close();

                cmd.CommandText = "select final_fee from TEST.fee";
                con.Open();
                DataTable dt_Income = new DataTable();
                dt_Income.Load(cmd.ExecuteReader());
                foreach (DataRow row in dt_Income.Rows)
                {
                    this.Total_Income += Convert.ToDouble(row[0]);
                }

                this.payRollPnlIncome.Text = this.Total_Income.ToString();

                this.payRollPnlBenefitIncome.Text = (this.Total_Income - this.Total_PayRoll).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            try
            {
                indicator.Top = ((Control)sender).Top;
                indicator.Height = ((Control)sender).Height - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void add_employee_Click(object sender, EventArgs e)
        {

        }

        private void add_student_Click(object sender, EventArgs e)
        {

        }

        private void add_parent_Click(object sender, EventArgs e)
        {

        }

        private void add_system_users_Click(object sender, EventArgs e)
        {
            try
            {
                UserRegistration userRegistration = new UserRegistration();
                userRegistration.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addEmployeeDetails_Click(object sender, EventArgs e)
        {try
            {
                AddEmployeeTeacherDetails teacherDetails = new AddEmployeeTeacherDetails();
                teacherDetails.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
