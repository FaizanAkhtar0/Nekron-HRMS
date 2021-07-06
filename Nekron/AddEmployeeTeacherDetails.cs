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
    public partial class AddEmployeeTeacherDetails : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private int ID;
        private string usertype, username, fullname, email, contactno, gender, status, password, imgpath;
        private string paymentDate, paymentMode, paymentModeDetails;
        private double basicSalary, totalPayment, deduction;

        public void setValues(string usertype, string username, string fullname, string email, string contactno, string gender, string status, string password, string imgpath)
        {
            this.usertype = usertype;
            this.username = username;
            this.fullname = fullname;
            this.email = email;
            this.contactno = contactno;
            this.gender = gender;
            this.status = status;
            this.password = password;
            this.imgpath = imgpath;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            open.Title = "Browse Profile Picture";
            if (open.ShowDialog() == DialogResult.OK) {
                string picPath = open.FileName.ToString();
                this.imgpath = picPath;
                usr_img.ImageLocation = picPath;
            }
        }

        public AddEmployeeTeacherDetails()
        {
            InitializeComponent();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            AddSalary salary = new AddSalary();
            this.ID = GenerateNewID();
            salary.setVal(ID, usr_name.Text);
            salary.Show();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

        }

        private bool UserNameValidityCheck()
        {
            try {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                bool flag = false;

                cmd.Connection = con;
                if (usr_name.Text.Equals("")) {
                    MessageBox.Show("Please enter and validate a username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_name.HintText = "Enter a username here...";
                    usr_name.Focus();
                }
                if (usr_type.Text.Equals("Admin")) {
                    cmd.CommandText = "select username from TEST.admins";
                } else if (usr_type.Text.Equals("Teacher")) {
                    cmd.CommandText = "select username from TEST.teachers";
                } else if (usr_type.Text.Equals("Student")) {
                    cmd.CommandText = "select username from TEST.students";
                } else if (usr_type.Text.Equals("Parent")) {
                    cmd.CommandText = "select username from TEST.parents";
                }

                con.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows) {
                    string username = Convert.ToString(row[0]);
                    if (usr_name.Text.Equals(username)) {
                        return true;
                    }
                }

                return flag;
                MessageBox.Show("No such " + usr_name.Text + " of type " + usr_type.Text + " exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show("Unable to connect to database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (usr_name.Text.Equals("")) {
                MessageBox.Show("Username must be provided for delete operation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_name.HintText = "Enter a valid username here!";
                usr_name.Focus();
            }else if (usr_type.Text.Equals("-Select-")) {
                MessageBox.Show("You must select a usertype to continue!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_type.Focus();
            } else {
                if (UserNameValidityCheck()) {
                    Delete();
                }
            }
        }

        private void Delete() {
            DeleteFromSalary();
            DeleteFromTeachers();
        }

        private void DeleteFromSalary() {
            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "delete from TEST.salary where username = '" + usr_name.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch(Exception ex) {
                MessageBox.Show("Program couldn't delete salary info from databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteFromTeachers() {
            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "delete from TEST.teachers where username = '" + usr_name.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program couldn't delete Teacher info from databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (usr_name.Text.Equals("")) {
                MessageBox.Show("Username must be provided for Updation operation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_name.HintText = "Enter a valid username here!";
                usr_name.Focus();
            } else if (usr_type.Text.Equals("-Select-")) {
                MessageBox.Show("You must select a usertype to continue!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_type.Focus();
            } else {
                if (UserNameValidityCheck()) {
                    if (MessageBox.Show("Do you want to load previously saved profile of \"" + usr_name.Text + "\".\nPress 'yes' to autofill and 'no' to update!", "Autofill feature?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        AutoFillForm();
                    } else {
                        updateinfo();
                    }
                }
            }
        }

        private void updateinfo() {
            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                int genderVal, StatusVal = 0;

                if (usr_gender.Text.Equals("Male")) {
                    genderVal = 1;
                } else if (usr_gender.Text.Equals("Others")) {
                    genderVal = 2;
                } else {
                    genderVal = 0;
                }
                if (usr_status.Text.Equals("Enabled")) {
                    StatusVal = 1;
                } else {
                    StatusVal = 0;
                }

                FileStream file = new FileStream(imgpath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(file);
                FileInfo info = new FileInfo(imgpath);
                byte[] imgData = br.ReadBytes((int)file.Length);

                cmd.Connection = con;
                cmd.CommandText = "update TEST.teachers set full_name = '" + usr_fullname.Text + "', email = '"
                    + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + genderVal
                    + "', status = '" + StatusVal + "', date_of_joining = '"
                    + usr_DOJ.Value.Date.ToString("dd-MMM-yyyy") + "', father_name = '"
                    + usr_fathername.Text + "', permanent_address = '" + usr_permanentAddress.Text
                    + "', date_of_birth = '" + usr_DOB.Value.Date.ToString("dd-MMM-yyyy")
                    + "', years_of_experience = '" + usr_yearsOfExperience.Text + "', cnic = '"
                    + usr_cnic.Text + "', password = '" + password + "', image = '" + imgData + "', basic_income = '"
                    + usr_income.Text + "', designation = '" + usr_designation.Text + "', income_tax = '"
                    + usr_income.Text + "', group_insurance = '" + usr_groupInsurance.Text
                    + "', family_benefit_fund = '" + usr_family_benefitFund.Text + "', loan = '" + usr_loans.Text
                    + "', deduction = '" + usr_deduction.Text + "', section_name = '" + usr_assigned_section.Text + "'";  

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program couldn't Update " + usr_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoFillForm()
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "select * from TEST.teachers where username = '" + usr_name.Text + "'";

            DataTable dt = new DataTable();

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            byte[] imgData = (byte[])dt.Rows[0][14];

            MemoryStream ms = new MemoryStream();
            ms.Write(imgData, 0, imgData.Length);
            Bitmap bm = new Bitmap(ms, false);
            usr_img.Image = bm;
            ms.Dispose();

            con.Open();
            OracleDataReader odr = cmd.ExecuteReader();

            while (odr.Read()) {
                usr_fullname.Text = Convert.ToString(odr["full_name"]);
                usr_email.Text = Convert.ToString(odr["email"]);
                usr_contactNo.Text = Convert.ToString(odr["contact_no"]);
                int genderVal = Convert.ToInt32(odr["gender"]);
                int statusVal = Convert.ToInt32(odr["status"]);
                usr_DOJ.Value = Convert.ToDateTime(odr["date_of_joining"]);
                usr_fathername.Text = Convert.ToString(odr["father_name"]);
                usr_permanentAddress.Text = Convert.ToString(odr["permanent_address"]);
                usr_DOB.Value = Convert.ToDateTime(odr["date_of_birth"]);
                usr_yearsOfExperience.Text = Convert.ToString(odr["years_of_experience"]);
                usr_cnic.Text = Convert.ToString(odr["cnic"]);
                usr_income.Text = Convert.ToString(odr["basic_income"]);
                usr_designation.Text = Convert.ToString(odr["designation"]);
                usr_incomeTax.Text = Convert.ToString(odr["income_tax"]);
                usr_groupInsurance.Text = Convert.ToString(odr["group_insurance"]);
                usr_family_benefitFund.Text = Convert.ToString(odr["family_benefit_fund"]);
                usr_loans.Text = Convert.ToString(odr["loan"]);
                usr_deduction.Text = Convert.ToString(odr["deduction"]);
                usr_assigned_section.Text = Convert.ToString(odr["section_name"]);

                if (genderVal == 1) {
                    usr_gender.Text = "Male";
                } else if (genderVal == 0) {
                    usr_gender.Text = "Female";
                } else {
                    usr_gender.Text = "Others";
                }

                if (statusVal == 1) {
                    usr_status.Text = "Enabled";
                } else {
                    usr_status.Text = "Disabled";
                }
            }

            con.Close();
        }

        private void view_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("UserName : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("FullName : " + usr_fullname.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Email : " + usr_email.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("ContactNo : " + usr_contactNo.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("Gender : " + usr_gender.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("AdmissionDate : " + DateTime.Now, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("FatherName : " + usr_fathername.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("ParmanentAddress : " + usr_permanentAddress.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25,410));
            e.Graphics.DrawString("Date Of Birth : " + usr_DOB.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString("YearOfExperience : " + usr_yearsOfExperience.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 470));
            e.Graphics.DrawString("CNIC: " + usr_cnic.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 500));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 530));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 560));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 590));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 620));

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            int genderVal, StatusVal = 0;

            if (usr_gender.Text.Equals("Male")) {
                genderVal = 1;
            } else if (usr_gender.Text.Equals("Others")) {
                genderVal = 2;
            } else {
                genderVal = 0;
            }
            if (usr_status.Text.Equals("Enabled")) {
                StatusVal = 1;
            } else {
                StatusVal = 0;
            }
            

            DateTime dt = DateTime.Now;
            string dated = dt.ToString("dd-MMM-yyyy");

            FileStream file = new FileStream(imgpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(file);
            FileInfo info = new FileInfo(imgpath);
            byte[] imgData = br.ReadBytes((int)file.Length);

            cmd.Connection = con;
            cmd.CommandText = "insert into TEST.teachers values(:t_id, :username, :full_name, :email, :contact_no, :gender, :status" +
                ", :date_of_joining, :father_name, :permanent_address, :date_of_birth, :years_of_experience, :cnic, :password, :image, :basic_income" +
                ", :designation, :income_tax, :group_insurance, :family_benefit_fund, :loan, :deduction, :section_name)";

            string DOB = usr_DOB.Value.Date.ToString("dd-MMM-yyyy");

            int teacherID = GenerateNewID();
            cmd.Parameters.Add(":t_id", GenerateNewID());
            cmd.Parameters.Add(":username", usr_name.Text);
            cmd.Parameters.Add(":full_name", usr_fullname.Text);
            cmd.Parameters.Add(":email", usr_email.Text);
            cmd.Parameters.Add(":contact_no", usr_contactNo.Text);
            cmd.Parameters.Add(":gender", genderVal);
            cmd.Parameters.Add(":status", StatusVal);
            cmd.Parameters.Add(":date_of_joining", dated);
            cmd.Parameters.Add(":father_name", usr_fathername.Text);
            cmd.Parameters.Add(":permanent_address", usr_permanentAddress.Text);
            cmd.Parameters.Add(":date_of_birth", DOB);
            cmd.Parameters.Add(":years_of_experience", usr_yearsOfExperience.Text);
            cmd.Parameters.Add(":cnic", usr_cnic.Text);
            cmd.Parameters.Add("password", password);
            cmd.Parameters.Add(":image", (Object)imgData);
            cmd.Parameters.Add(":basic_income", usr_income.Text);
            cmd.Parameters.Add(":designation", usr_designation.Text);
            cmd.Parameters.Add(":income_tax", usr_incomeTax.Text);
            cmd.Parameters.Add(":group_insurance", usr_groupInsurance.Text);
            cmd.Parameters.Add(":family_benefit_fund", usr_family_benefitFund.Text);
            cmd.Parameters.Add(":loan", usr_loans.Text);
            cmd.Parameters.Add(":deduction", usr_deduction.Text);
            cmd.Parameters.Add(":section_name", usr_assigned_section.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            AddSalary addSalary = new AddSalary();
            addSalary.setVal(teacherID, usr_name.Text);

            AddSalaryDetails(teacherID);
            MessageBox.Show("Employee \"" + usr_name.Text + "\" has been added along with the salary structure.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddSalaryDetails(int ID) {
            this.paymentDate = Salaryinfo.paymentDate;
            this.paymentMode = Salaryinfo.paymentMode;
            this.paymentModeDetails = Salaryinfo.paymentModeDetails;
            this.basicSalary = Salaryinfo.basicSalary;
            this.totalPayment = Salaryinfo.totalPayment;
            this.deduction = Salaryinfo.deduction;

            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "insert into TEST.salary values(:usr_id, :username, :salary_amount, :payment_date, :mode_of_payment, :payment_mode_details, :deducation_amount, :total_pay)";

                cmd.Parameters.Add("usr_id", ID);
                cmd.Parameters.Add("username", usr_name.Text);
                cmd.Parameters.Add("salary_amount", basicSalary);
                cmd.Parameters.Add("payment_date", paymentDate);
                cmd.Parameters.Add("mode_of_payment", paymentMode);
                cmd.Parameters.Add("payment_mode_details", paymentModeDetails);
                cmd.Parameters.Add("deducation_amount", deduction);
                cmd.Parameters.Add("total_pay", totalPayment);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program was unable to store Salary into database due to following Exception: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GenerateNewID() {
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;

            cmd.CommandText = "select count(t_id) from TEST.teachers";

            con.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            int count = Convert.ToInt32(dt.Rows[0][0]);

            return count + 1;
        }

        private void usr_role_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void role_id_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuGradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuMaterialTextbox4_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset() {
            usr_fullname.Text = "";
            usr_fathername.Text = "";
            usr_email.Text = "";
            usr_contactNo.Text = "";
            usr_gender.Text = "-Select-";
            usr_status.Text = "-Select-";
            usr_DOJ.Value = DateTime.Now;
            usr_fathername.Text = "";
            usr_DOB.Value = DateTime.Now;
            usr_yearsOfExperience.Text = "";
            usr_cnic.Text = "";
            usr_assigned_section.Text = "-Select-";
            usr_income.Text = "";
            usr_designation.Text = "";
            usr_incomeTax.Text = "";
            usr_groupInsurance.Text = "";
            usr_family_benefitFund.Text = "";
            usr_loans.Text = "";
            usr_deduction.Text = "";
        }

        private void AddEmployeeTeacherDetails_Load(object sender, EventArgs e)
        {
            usr_type.Text = this.usertype;
            usr_name.Text = this.username;
            usr_fullname.Text = this.fullname;
            usr_email.Text = this.email;
            usr_contactNo.Text = this.contactno;
            usr_gender.Text = this.gender;
            usr_status.Text = this.status;
            usr_img.ImageLocation = imgpath;
            PopulateSections();
        }



        private void PopulateSections()
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "select section_name from TEST.sections";
            DataTable dt = new DataTable();
            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows) {
                usr_assigned_section.Items.Add((string)row[0]);
            }
        }
    }
}
