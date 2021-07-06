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
using Microsoft.Reporting.WinForms;


namespace Nekron
{
    public partial class AddStudentDetails : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string usertype, username, fullname, email, contactno, gender, status, password, imgpath = "";
        private static double tutionFee, labCharges, securityCharges, healthServices, TotalFee, avalibleConcession, finalFee, fine;
        private static string dueDate;

        private void bunifuCustomLabel2_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (!(usr_name.Text.Equals(""))) {
                if (UserNameValidityCheck()) {
                    if (MessageBox.Show("Do you want to load previously saved profile of \"" + usr_name.Text + "\".\nPress 'yes' to autofill and 'no' to update!", "Autofill feature?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        AutoFillForm();
                    } else {
                        updateinfo();
                    }
                }
            }
        }

        private void updateinfo()
        {
            try {
                usr_admissionNo.Enabled = true;
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                int genderVal, StatusVal, preVal, primaryVal, midVal, secondaryVal, higherSecondaryVal = 0;

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
                if (usr_pre_chkbx.Checked == true) {
                    preVal = 1;
                } else {
                    preVal = 0;
                }

                if (usr_primary_chkbx.Checked == true) {
                    primaryVal = 1;
                } else {
                    primaryVal = 0;
                }

                if (usr_middle_chkbx.Checked == true) {
                    midVal = 1;
                } else {
                    midVal = 0;
                }

                if (usr_seconday_chkbx.Checked == true) {
                    secondaryVal = 1;
                } else {
                    secondaryVal = 0;
                }

                if (usr_highSecondary_chkbx.Checked == true) {
                    higherSecondaryVal = 1;
                } else {
                    higherSecondaryVal = 0;
                }

                FileStream file = new FileStream(imgpath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(file);
                FileInfo info = new FileInfo(imgpath);
                byte[] imgData = br.ReadBytes((int)file.Length);

                
                cmd.Connection = con;
                cmd.CommandText = "update TEST.students set full_name = '" + usr_fullname.Text + "', email = '"
                    + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + genderVal
                    + "', status = '" + StatusVal + "', admission_date = '" + usr_admissionDate.Value.Date.ToString("dd-MMM-yyyy")
                    + "', father_name = '" + usr_fathername.Text + "', mother_name = '" + usr_mothername.Text
                    + "', date_of_birth = '" + usr_DOB.Value.Date.ToString("dd-MMM-yyyy") + "', nationality = '" + usr_nationality.Text
                    + "', religion = '" + usr_religion.Text + "', class_name = '" + usr_Class.Text + "', section_name = '"
                    + usr_assignedSection.Text + "', s_roll_no = '" + usr_admissionNo.Text + "', guardian_full_name = '"
                    + usr_GuardianName.Text + "', guardian_contact_no = '" + usr_GuardianContactNo.Text + "', guardian_address = '"
                    + usr_GuardianFullAddress.Text + "', pre_school = '" + preVal + "', primary_school = '" + primaryVal
                    + "', middle = '" + midVal + "', middle_yearofpass = '" + usr_middle_yearOfPass.Text
                    + "', middle_institution = '" + usr_middle_insititution.Text + "', high_school_ssc = '" + secondaryVal
                    + "', ssc_yearofpass = '" + usr_ssc_yearOfPass.Text + "', ssc_institution = '" + usr_secondary_insititution.Text
                    + "', higher_secondary_hssc = '" + higherSecondaryVal + "', hssc_yearofpass = '" + usr_hssc_yearOfPass.Text
                    + "', hssc_institution = '" + usr_HighSecondary_institution.Text + "', image = '" + (Object)imgData
                    + "', pre_institution = '" + usr_pre_insititution.Text + "', primary_institution = '" + usr_primary_insititution.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program couldn't Update " + usr_name.Text + "'s Profile into databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UserNameValidityCheck()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                bool flag = false;

                cmd.Connection = con;
                if (usr_name.Text.Equals(""))
                {
                    MessageBox.Show("Please enter and validate a username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_name.HintText = "Enter a username here...";
                    usr_name.Focus();
                }
                if (usr_type.Text.Equals("Admin"))
                {
                    cmd.CommandText = "select username from TEST.admins";
                }
                else if (usr_type.Text.Equals("Teacher"))
                {
                    cmd.CommandText = "select username from TEST.teachers";
                }
                else if (usr_type.Text.Equals("Student"))
                {
                    cmd.CommandText = "select username from TEST.students";
                }
                else if (usr_type.Text.Equals("Parent"))
                {
                    cmd.CommandText = "select username from TEST.parents";
                }

                con.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    string username = Convert.ToString(row[0]);
                    if (usr_name.Text.Equals(username))
                    {
                        return true;
                    }
                }

                return flag;
                MessageBox.Show("No such " + usr_name.Text + " of type " + usr_type.Text +" exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void AutoFillForm() {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "select * from TEST.students where username = '" + usr_name.Text + "'";

            DataTable dt = new DataTable();

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            byte[] imgData = null;

            foreach (DataRow row in dt.Rows) {
                imgData = (byte[])row[32];
            }

            MemoryStream ms = new MemoryStream();
            ms.Write(imgData, 0, imgData.Length);
            Bitmap bm = new Bitmap(ms, false);
            usr_img.Image = bm;
            ms.Dispose();

            con.Open();
            OracleDataReader odr = cmd.ExecuteReader();

            while (odr.Read())
            {
                usr_fullname.Text = Convert.ToString(odr["full_name"]);
                usr_email.Text = Convert.ToString(odr["email"]);
                usr_contactNo.Text = Convert.ToString(odr["contact_no"]);
                int genderVal = Convert.ToInt32(odr["gender"]);
                int statusVal = Convert.ToInt32(odr["status"]);
                usr_admissionDate.Text = Convert.ToString(odr["admission_date"]);
                usr_admissionNo.Text = Convert.ToString(odr["admission_no"]);
                usr_fathername.Text = Convert.ToString(odr["father_name"]);
                usr_mothername.Text = Convert.ToString(odr["mother_name"]);
                usr_DOB.Value = Convert.ToDateTime(odr["date_of_birth"]);
                usr_nationality.Text = Convert.ToString(odr["nationality"]);
                usr_religion.Text = Convert.ToString(odr["religion"]);
                usr_Class.Text = Convert.ToString(odr["class_name"]);
                usr_assignedSection.Text = Convert.ToString(odr["section_name"]);
                usr_GuardianName.Text = Convert.ToString(odr["guardian_full_name"]);
                usr_GuardianContactNo.Text = Convert.ToString(odr["guardian_contact_no"]);
                usr_GuardianFullAddress.Text = Convert.ToString(odr["guardian_address"]);
                int preVal = Convert.ToInt32(odr["pre_school"]);
                int primaryVal = Convert.ToInt32(odr["primary_school"]);
                int midVal = Convert.ToInt32(odr["middle"]);
                usr_middle_yearOfPass.Text = Convert.ToString(odr["middle_yearofpass"]);
                usr_middle_insititution.Text = Convert.ToString(odr["middle_institution"]);
                int secondaryVal = Convert.ToInt32(odr["high_school_ssc"]);
                usr_ssc_yearOfPass.Text = Convert.ToString(odr["ssc_yearofpass"]);
                usr_secondary_insititution.Text = Convert.ToString(odr["ssc_institution"]);
                int highSecondaryVal = Convert.ToInt32(odr["higher_secondary_hssc"]);
                usr_hssc_yearOfPass.Text = Convert.ToString(odr["hssc_yearofpass"]);
                usr_HighSecondary_institution.Text = Convert.ToString(odr["hssc_institution"]);
                usr_pre_insititution.Text = Convert.ToString(odr["pre_institution"]);
                usr_primary_insititution.Text = Convert.ToString(odr["primary_institution"]);

                if (genderVal == 1)
                {
                    usr_gender.Text = "Male";
                }
                else if (genderVal == 0)
                {
                    usr_gender.Text = "Female";
                }else {
                    usr_gender.Text = "Others";
                }

                if (statusVal == 1)
                {
                    usr_status.Text = "Enabled";
                }
                else
                {
                    usr_status.Text = "Disabled";
                }

                if (preVal == 1)
                {
                    usr_pre_chkbx.Checked = true;
                }
                else
                {
                    usr_pre_chkbx.Checked = false;
                }

                if (primaryVal == 1)
                {
                    usr_primary_chkbx.Checked = true;
                }
                else
                {
                    usr_primary_chkbx.Checked = false;
                }

                if (midVal == 1)
                {
                    usr_middle_chkbx.Checked = true;
                }
                else
                {
                    usr_middle_chkbx.Checked = false;
                }

                if (secondaryVal == 1)
                {
                    usr_seconday_chkbx.Checked = true;
                }
                else
                {
                    usr_seconday_chkbx.Checked = false;
                }

                if (highSecondaryVal == 1)
                {
                    usr_highSecondary_chkbx.Checked = true;
                }
                else
                {
                    usr_highSecondary_chkbx.Checked = false;
                }

                MessageBox.Show("" + genderVal + statusVal + preVal + primaryVal + midVal + secondaryVal + highSecondaryVal + usr_DOB.Value);
            }

            con.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset() {
            usr_fullname.Text = "";
            usr_email.Text = "";
            usr_contactNo.Text = "";
            usr_admissionDate.Text = "";
            usr_admissionNo.Text = "";
            usr_fathername.Text = "";
            usr_mothername.Text = "";
            usr_DOB.Value = DateTime.Now;
            usr_nationality.Text = "";
            usr_religion.Text = "";
            usr_gender.Text = "-Select-";
            usr_status.Text = "-Select-";
            usr_Class.Text = "-Select-";
            usr_assignedSection.Text = "-Select-";
            usr_GuardianName.Text = "";
            usr_GuardianContactNo.Text = "";
            usr_GuardianFullAddress.Text = "";
            usr_middle_yearOfPass.Text = "";
            usr_middle_insititution.Text = "";
            usr_ssc_yearOfPass.Text = "";
            usr_secondary_insititution.Text = "";
            usr_hssc_yearOfPass.Text = "";
            usr_HighSecondary_institution.Text = "";
            usr_pre_insititution.Text = "";
            usr_primary_insititution.Text = "";
            usr_pre_chkbx.Checked = true;
            usr_primary_chkbx.Checked = true;
            usr_middle_chkbx.Checked = false;
            usr_seconday_chkbx.Checked = false;
            usr_highSecondary_chkbx.Checked = false;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (usr_name.Text.Equals("")) {
                MessageBox.Show("Username must be provided for delete operation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_name.HintText = "Enter a valid username here!";
                usr_name.Focus();
            } else if (usr_type.Text.Equals("-Select-")) {
                MessageBox.Show("You must select a usertype to continue!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr_type.Focus();
            } else {
                if (UserNameValidityCheck()) {
                    Delete();
                }
            }
        }

        private void Delete()
        {
            DeleteFromFee();
            DeleteFromStudents();
        }

        private void DeleteFromFee() {
            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "delete from TEST.fee where username = '" + usr_name.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program couldn't delete salary info from databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteFromStudents() {
            try {
                OracleCommand cmd = new OracleCommand();

                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                cmd.CommandText = "delete from TEST.students where username = '" + usr_name.Text + "'";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            } catch (Exception ex) {
                MessageBox.Show("Program couldn't delete Teacher info from databse due to following Exception: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void usr_fullname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.icons8_dragon_100;
            Image img = bmp;
            e.Graphics.DrawImage(img, 300, 25, img.Width, img.Height);
            e.Graphics.DrawString("UserName : " + usr_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("FullName : " + usr_fullname.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Email : " + usr_email.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("ContactNo : " + usr_contactNo.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25,290));
            e.Graphics.DrawString("Gender : " + usr_gender.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("AdmissionDate : " + DateTime.Now, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("FatherName : " + usr_fathername.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("MotherName : " + usr_mothername.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 410));
            var datestring = usr_DOB.Value.ToShortDateString();
            e.Graphics.DrawString("Date Of Birth : " + datestring, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString("Nationality : " + usr_nationality.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 470));
            e.Graphics.DrawString("Religion: " + usr_religion.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 500));
            e.Graphics.DrawString("Class : " + usr_Class.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 530));
            e.Graphics.DrawString("Section : " + usr_assignedSection.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 560));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 560));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 590));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 620));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 650));
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

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

        public static void getFeeDetails()
        {
            tutionFee = Feeinfo.tutionFee;
            labCharges = Feeinfo.labCharges;
            securityCharges = Feeinfo.securityCharges;
            healthServices = Feeinfo.healthServices;
            TotalFee = Feeinfo.TotalFee;
            avalibleConcession = Feeinfo.avalibleConcession;
            finalFee = Feeinfo.finalFee;
            dueDate = Feeinfo.dueDate;
            fine = Feeinfo.fine;
            MessageBox.Show("" + tutionFee + labCharges + securityCharges + healthServices + TotalFee + avalibleConcession + finalFee + dueDate + fine);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            open.Title = "Browse Profile Picture";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string picPath = open.FileName.ToString();
                this.imgpath = picPath;
                usr_img.ImageLocation = picPath;
            }
        }

        public AddStudentDetails()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuCustomLabel12_Click(object sender, EventArgs e)
        {

        }

        private void AddStudent_Load(object sender, EventArgs e)
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
            usr_admissionNo.Text = Convert.ToString(GenerateNewID());
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

            foreach (DataRow row in dt.Rows)
            {
                usr_assignedSection.Items.Add((string)row[0]);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox19_OnValueChanged(object sender, EventArgs e)
        {

        }

        private int GenerateNewID()
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;

            cmd.CommandText = "select count(s_id) from TEST.students";

            con.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            int count = Convert.ToInt32(dt.Rows[0][0]);

            return count + 1;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            int genderVal, StatusVal, preVal, primaryVal, midVal, secondaryVal, higherSecondaryVal = 0;

            if (usr_gender.Text.Equals("Male"))
            {
                genderVal = 1;
            }
            else if (usr_gender.Text.Equals("Others"))
            {
                genderVal = 2;
            }
            else
            {
                genderVal = 0;
            }
            if (usr_status.Text.Equals("Enabled"))
            {
                StatusVal = 1;
            }
            else
            {
                StatusVal = 0;
            }
            if (usr_pre_chkbx.Checked == true)
            {
                preVal = 1;
            }
            else
            {
                preVal = 0;
            }

            if (usr_primary_chkbx.Checked == true)
            {
                primaryVal = 1;
            }
            else
            {
                primaryVal = 0;
            }

            if (usr_middle_chkbx.Checked == true)
            {
                midVal = 1;
            }
            else
            {
                midVal = 0;
            }

            if (usr_seconday_chkbx.Checked == true)
            {
                secondaryVal = 1;
            }
            else
            {
                secondaryVal = 0;
            }

            if (usr_highSecondary_chkbx.Checked == true)
            {
                higherSecondaryVal = 1;
            }
            else
            {
                higherSecondaryVal = 0;
            }

            DateTime dt = DateTime.Now;
            string dated = dt.ToString("dd-MMM-yyyy");

            FileStream file = new FileStream(imgpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(file);
            FileInfo info = new FileInfo(imgpath);
            byte[] imgData = br.ReadBytes((int)file.Length);

         /*   cmd.Connection = con;
            cmd.CommandText = "insert into TEST.students(s_id, username, full_name, email, contact_no, gender, status" +
                ", admission_date, admission_no, father_name, mother_name, date_of_birth, nationality, religion, class_name, section_name" +
                ", s_roll_no, password, guardian_full_name, guardian_contact_no, guardian_address, pre_school, primary_school, middle" +
                ", middle_yearofpass, middle_institution, high_school_ssc, ssc_yearofpass, ssc_institution, higher_secondary_hssc" +
                ", hssc_yearofpass, hssc_institution) values('"+ GenerateNewID() + "','"+ usr_name.Text + "','" + usr_fullname.Text + "','" + usr_email.Text
                + "','" + usr_contactNo.Text + "','" + genderVal + "','" + StatusVal + "','" + dated + "','" + GenerateNewID() + "','" + usr_fathername.Text + "','"
                + usr_mothername.Text + "','" + usr_DOB.Value.ToString("dd-MMM-yyyy") + "','" + usr_nationality.Text + "','" + usr_religion.Text + "','" + usr_Class.Text + "','" + usr_assignedSection.Text
                + "','" + GenerateNewID() + "','" + password + "','" + usr_GuardianName.Text + "','" + usr_GuardianContactNo.Text + "','" + usr_GuardianFullAddress.Text
                + "','" + preVal + "','" + primaryVal + "','" + midVal + "','" + usr_middle_yearOfPass.Text + "','" + usr_middle_insititution.Text + "','" + secondaryVal
                + "','" + usr_ssc_yearOfPass.Text + "','" + usr_secondary_insititution.Text + "','" + higherSecondaryVal + "','" + usr_hssc_yearOfPass.Text + "','" + usr_HighSecondary_institution.Text
                + "','" + (Object)imgData + "')";
                */

                        // Student Registration...
                        cmd.Connection = con;
                        cmd.CommandText = "insert into TEST.students values(:s_id, :username, :full_name, :email, :contact_no, :gender, :status" +
                            ", :admission_date, :admission_no, :father_name, :mother_name, :date_of_birth, :nationality, :religion, :class_name, :section_name" +
                            ", :s_roll_no, :password, :guardian_full_name, :guardian_contact_no, :guardian_address, :pre_school, :primary_school, :middle" +
                            ", :middle_yearofpass, :middle_institution, :high_school_ssc, :ssc_yearofpass, :ssc_institution, :higher_secondary_hssc" +
                            ", :hssc_yearofpass, :hssc_institution, :image, :pre_institution, :primary_institution)";

                        string DOB = usr_DOB.Value.Date.ToString("dd-MMM-yyyy");

                        int studentID = GenerateNewID();
                        cmd.Parameters.Add(":s_id", GenerateNewID());
                        cmd.Parameters.Add(":username", usr_name.Text);
                        cmd.Parameters.Add(":full_name", usr_fullname.Text);
                        cmd.Parameters.Add(":email", usr_email.Text);
                        cmd.Parameters.Add(":contact_no", usr_contactNo.Text);
                        cmd.Parameters.Add(":gender", genderVal);
                        cmd.Parameters.Add(":status", StatusVal);
                        cmd.Parameters.Add(":admission_date", dated);
                        cmd.Parameters.Add(":admission_no", GenerateNewID());
                        cmd.Parameters.Add(":father_name", usr_fathername.Text);
                        cmd.Parameters.Add(":mother_name", usr_mothername.Text);
                        cmd.Parameters.Add(":date_of_birth", DOB);
                        cmd.Parameters.Add(":nationality", usr_nationality.Text);
                        cmd.Parameters.Add(":religion", usr_religion.Text);
                        cmd.Parameters.Add(":class_name", usr_Class.Text);
                        cmd.Parameters.Add(":section_name", usr_assignedSection.Text);
                        cmd.Parameters.Add(":s_roll_no", GenerateNewID());
                        cmd.Parameters.Add(":password", password);
                        cmd.Parameters.Add(":guardian_full_name", usr_GuardianName.Text);
                        cmd.Parameters.Add(":guardian_contact_no", usr_GuardianContactNo.Text);
                        cmd.Parameters.Add(":guardian_address", usr_GuardianFullAddress.Text);
                        cmd.Parameters.Add(":pre_school", preVal);
                        cmd.Parameters.Add(":primary_school", primaryVal);
                        cmd.Parameters.Add(":middle", midVal);
                        cmd.Parameters.Add(":middle_yearofpass", usr_middle_yearOfPass.Text);
                        cmd.Parameters.Add(":middle_institution", usr_middle_insititution.Text);
                        cmd.Parameters.Add(":high_school_ssc", secondaryVal);
                        cmd.Parameters.Add(":ssc_yearofpass", usr_ssc_yearOfPass.Text);
                        cmd.Parameters.Add(":ssc_institution", usr_secondary_insititution.Text);
                        cmd.Parameters.Add(":higher_secondary_hssc", higherSecondaryVal);
                        cmd.Parameters.Add(":hssc_yearofpass", usr_hssc_yearOfPass.Text);
                        cmd.Parameters.Add(":hssc_institution", usr_HighSecondary_institution.Text);
                        cmd.Parameters.Add(":image", (Object)imgData);
                        cmd.Parameters.Add(":pre_institution", usr_pre_insititution.Text);
                        cmd.Parameters.Add("primary_institution", usr_primary_insititution.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
            InsertFeeDetails(studentID);
            MessageBox.Show("Student " + usr_fullname.Text + " has been registered along with the fee structure!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InsertFeeDetails(int studentID) {

            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;
            cmd.CommandText = "insert into TEST.fee values(:s_id, :s_username, :tution_fee, :lab_charges, :security_charges, :health_services, :total_fee, :avalible_concession, :final_fee, :due_date, :fine)";

            cmd.Parameters.Add(":s_id", studentID);
            cmd.Parameters.Add(":s_username", usr_name.Text);
            cmd.Parameters.Add(":tution_fee", tutionFee);
            cmd.Parameters.Add(":lab_charges", labCharges);
            cmd.Parameters.Add(":security_charges", securityCharges);
            cmd.Parameters.Add(":health_services", healthServices);
            cmd.Parameters.Add(":total_fee", tutionFee);
            cmd.Parameters.Add(":avalible_concession", avalibleConcession);
            cmd.Parameters.Add(":final_fee", finalFee);
            cmd.Parameters.Add(":due_date", dueDate);
            cmd.Parameters.Add(":fine", fine);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            FeeDetails feeDetails = new FeeDetails();
            feeDetails.Show();
        }
    }
}
