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
    public partial class UserRegistration : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        string usr_name_changed;
        public UserRegistration()
        {
            InitializeComponent();
            unavaliblePic.Visible = false;
            avaliblePic.Visible = false;
            usr_name_validity_pic.BringToFront();
            timer1.Start();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!(this.Opacity == 1)) {
                this.Opacity += 0.10;
            }
            else {
                timer1.Stop();
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {

            if (usr_name.Text.Equals(""))
            {
                MessageBox.Show("Please enter and validate a username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usr_name.HintText = "Enter a username here...";
                usr_name.Focus();
                return;
            }
            else if (usr_type.Text.Equals("-Select-"))
            {
                MessageBox.Show("Please select a user type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usr_type.Focus();
                return;
            }
            else if (usr_gender.Text.Equals("-Select-"))
            {
                MessageBox.Show("Please select an appropriate gender!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usr_gender.Focus();
                return;
            }
            else if (usr_status.Text.Equals("-Select-"))
            {
                MessageBox.Show("Please select a status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usr_status.Focus();
                return;
            }
            else if (imgPath.Text.Equals(""))
            {
                MessageBox.Show("Please upload a profile picture!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.loadImg.Focus();
                return;
            }
            else if (usr_pass.Text.Equals("") || usr_confirm_pass.Text.Equals(""))
            {
                MessageBox.Show("Please set a password to make your profile secure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.usr_pass.Focus();
                return;
            }
            else if (!(usr_pass.Text.Equals(usr_confirm_pass.Text)))
            {
                MessageBox.Show("Passwords did not match, try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.usr_pass.Focus();
                return;
            }
            else
            {
                if (UserNameValidityCheck())
                {
                    usr_name_validity_pic.Visible = false;
                    unavaliblePic.Visible = false;
                    avaliblePic.Visible = true;
                    avaliblePic.BringToFront();

                    if (usr_type.Text.Equals("Admin")){
                        string con_str = "insert into HRM.admins values(:a_id, :username, :a_password, :full_name, :email, :contact_no, :gender, :status, :registration_date, :image)";
                        OracleCommand cmd = new OracleCommand();
                        OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                        cmd.Connection = con;
                        cmd.CommandText = con_str;

                        FileStream file = new FileStream(imgPath.Text, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(file);
                        FileInfo info = new FileInfo(imgPath.Text);
                        byte[] imgData = br.ReadBytes((int)file.Length);

                        file.Close();
                        br.Close();

                        int genderVal, StatusVal;

                        if (usr_gender.Text.Equals("Male")) {
                            genderVal = 1;
                        }
                        else if (usr_gender.Text.Equals("Others")) {
                            genderVal = 2;
                        }else {
                            genderVal = 0;
                        }

                        if (usr_status.Text.Equals("Enabled")) {
                            StatusVal = 1;
                        }
                        else {
                            StatusVal = 0;
                        }

                        DateTime dt = DateTime.Now;
                        string dated = dt.ToString("dd-MMM-yyyy");

                        cmd.Parameters.Add("a_id", GenerateNewID());
                        cmd.Parameters.Add("username", usr_name.Text);
                        cmd.Parameters.Add("a_password", usr_pass.Text);
                        cmd.Parameters.Add("full_name", usr_fullname.Text);
                        cmd.Parameters.Add("email", usr_email.Text);
                        cmd.Parameters.Add("contact_no", usr_contactNo.Text);
                        cmd.Parameters.Add("gender", genderVal);
                        cmd.Parameters.Add("status", StatusVal);
                        cmd.Parameters.Add("registration_date", dated);
                        cmd.Parameters.Add("image", (Object)imgData);
                        


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("An administrative account has been added!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (usr_type.Text.Equals("Teacher")) {
                        AddEmployeeTeacherDetails addEmployee = new AddEmployeeTeacherDetails();
                        addEmployee.setValues(usr_type.Text, usr_name.Text, usr_fullname.Text, usr_email.Text, usr_contactNo.Text, usr_gender.Text, usr_status.Text, usr_pass.Text, imgPath.Text);
                        addEmployee.Show();

                        this.Hide();
                    }

                    if (usr_type.Text.Equals("Student"))
                    {
                        AddStudentDetails addStudent = new AddStudentDetails();
                        addStudent.setValues(usr_type.Text, usr_name.Text, usr_fullname.Text, usr_email.Text, usr_contactNo.Text, usr_gender.Text, usr_status.Text, usr_pass.Text, imgPath.Text);
                        addStudent.Show();

                        this.Hide();
                    }
                }
                else
                {
                    unavaliblePic.Visible = true;
                    avaliblePic.Visible = false;
                    usr_name_validity_pic.Visible = false;
                    unavaliblePic.BringToFront();
                    MessageBox.Show("Database already contains a username \"" + usr_name.Text + "\" of type, \"" + usr_type.Text + "\"\nPlease try another!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    unavaliblePic.Visible = false;
                    usr_name_validity_pic.Visible = true;
                    usr_name_validity_pic.BringToFront();
                    this.usr_name.Text = "";
                    this.usr_name.HintText = "Enter a different username here...";
                    this.usr_name.Focus();
                    return;
                }

               
            }
        }

        private void loadImg_Click(object sender, EventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            open.Title = "Browse a Profile Picture";

            if (open.ShowDialog() == DialogResult.OK) {
                string picPath = open.FileName.ToString();
                imgPath.Text = "";
                imgPath.Text = picPath;
                loadImg.Visible = false;
                LoadImgChecked.Visible = true;
                LoadImgChecked.BringToFront();
            }
        }

        private bool UserNameValidityCheck() {
            try {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                bool flag = true;

                cmd.Connection = con;
                if (usr_name.Text.Equals("")) {
                    MessageBox.Show("Please enter and validate a username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_name.HintText = "Enter a username here...";
                    usr_name.Focus();
                }
                if (usr_type.Text.Equals("Admin")){
                    cmd.CommandText = "select username from HRM.admins";
                }
                else if (usr_type.Text.Equals("Teacher")){
                    cmd.CommandText = "select username from HRM.teachers";
                }
                else if (usr_type.Text.Equals("Student")){
                    cmd.CommandText = "select username from HRM.students";
                }
                else if (usr_type.Text.Equals("Parent")){
                    cmd.CommandText = "select username from HRM.parents";
                }

                con.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach(DataRow row in dt.Rows) {
                    string username = Convert.ToString(row[0]);
                    if (usr_name.Text.Equals(username)) {
                        return false;
                    }
                }

                return flag;

                
            }catch (Exception ex) {
                MessageBox.Show("Unable to connect to database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (UserNameValidityCheck()){
                usr_name_validity_pic.Visible = false;
                unavaliblePic.Visible = false;
                avaliblePic.Visible = true;
                avaliblePic.BringToFront();
                usr_name_changed = usr_name.Text;
                timer2.Start();
            }
            else{
                unavaliblePic.Visible = true;
                avaliblePic.Visible = false;
                usr_name_validity_pic.Visible = false;
                unavaliblePic.BringToFront();
                MessageBox.Show("Database already contains a username \"" + usr_name.Text + "\" of such type, \"" + usr_type.Text + "\"\nPlease try another!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                unavaliblePic.Visible = false;
                usr_name_validity_pic.Visible = true;
                usr_name_validity_pic.BringToFront();
                this.usr_name.Text = "";
                this.usr_name.HintText = "Enter a different username here...";
                this.usr_name.Focus();
              
            }
        }

        private int GenerateNewID() {
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

            cmd.Connection = con;

            cmd.CommandText = "select count(a_id) from HRM.admins";

            con.Open();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            int count = Convert.ToInt32(dt.Rows[0][0]);

            return count + 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!(usr_name.Text.Equals(usr_name_changed))) {
                usr_name_validity_pic.Visible = true;
                avaliblePic.Visible = false;
                unavaliblePic.Visible = false;
                usr_name_validity_pic.BringToFront();
                timer2.Stop();
            }
        }

        private void bunifuCustomLabel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset() {
            usr_type.Text = "-Select-";
            usr_name.Text = "";
            usr_fullname.Text = "";
            usr_email.Text = "";
            usr_contactNo.Text = "";
            usr_gender.Text = "-Select-";
            usr_status.Text = "-Select-";
            imgPath.Text = "";
            usr_RegDate.Value = DateTime.Now;
            usr_pass.Text = "";
            usr_confirm_pass.Text = "";
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete() {
            try
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                if (usr_name.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_name.HintText = "Enter a username here...";
                    usr_name.Focus();
                    return;
                }
                if (usr_type.Text.Equals("Admin"))
                {
                    cmd.CommandText = "delete from HRM.admins where username = " + usr_name.Text;
                }
                else if (usr_type.Text.Equals("Teacher"))
                {
                    cmd.CommandText = "delete from HRM.teachers where username = " + usr_name.Text;
                }
                else if (usr_type.Text.Equals("Student"))
                {
                    cmd.CommandText = "delete from HRM.students where username = " + usr_name.Text;
                }
                else if (usr_type.Text.Equals("Parent"))
                {
                    cmd.CommandText = "delete from HRM.parents where username = " + usr_name.Text;
                }

                cmd.Connection = con;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Unable to perform the delete operation!\nException: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void UpdateInfo() {
            try
            {
                FileStream file = new FileStream(imgPath.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(file);
                FileInfo info = new FileInfo(imgPath.Text);
                byte[] imgData = br.ReadBytes((int)file.Length);

                OracleCommand cmd = new OracleCommand();
                OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");

                cmd.Connection = con;
                if (usr_name.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_name.HintText = "Enter a username here...";
                    usr_name.Focus();
                    return;
                }else if (usr_pass.Text.Equals(usr_confirm_pass.Text)) {
                    MessageBox.Show("Password did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usr_pass.Focus();
                    return;
                }
                if (usr_type.Text.Equals("Admin"))
                {
                    cmd.CommandText = "update HRM.admins set full_name = '" + usr_fullname.Text + "', email = '" + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + usr_gender.Text + "', status = '" + usr_status.Text + "', a_password = '" + usr_pass.Text + "', image = '" + imgData + "' where username = '" + usr_name.Text + "'"; 
                }
                else if (usr_type.Text.Equals("Teacher"))
                {
                    cmd.CommandText = "update HRM.teachers set full_name = '" + usr_fullname.Text + "', email = '" + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + usr_gender.Text + "', status = '" + usr_status.Text + "', a_password = '" + usr_pass.Text + "', image = '" + imgData + "' where username = '" + usr_name.Text + "'";
                }
                else if (usr_type.Text.Equals("Student"))
                {
                    cmd.CommandText = "update HRM.students set full_name = '" + usr_fullname.Text + "', email = '" + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + usr_gender.Text + "', status = '" + usr_status.Text + "', a_password = '" + usr_pass.Text + "', image = '" + imgData + "' where username = '" + usr_name.Text + "'";
                }
                else if (usr_type.Text.Equals("Parent"))
                {
                    cmd.CommandText = "update HRM.parents set full_name = '" + usr_fullname.Text + "', email = '" + usr_email.Text + "', contact_no = '" + usr_contactNo.Text + "', gender = '" + usr_gender.Text + "', status = '" + usr_status.Text + "', a_password = '" + usr_pass.Text + "', image = '" + imgData + "' where username = '" + usr_name.Text + "'";
                }

                cmd.Connection = con;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to perform the delete operation!\nException: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewData_Click(object sender, EventArgs e)
        {

        }
    }
}
