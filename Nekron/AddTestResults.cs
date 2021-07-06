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

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
            cmd.Connection = con;
            con.Open();

            DataTable dt1 = new DataTable();
            cmd.CommandText = "select c_id, username from TEST.students where section_name = '" + section_name.Text + "'";
            int c_id = 0;

            dt1.Load(cmd.ExecuteReader());
            foreach (DataRow row in dt1.Rows)
            {
                c_id = Convert.ToInt32(row[0]);
            }

            DataTable dt = new DataTable();
            cmd.CommandText = "select s_id, username from TEST.students where section_name = '" + section_name.Text + "'";
            int[] s_id = null;
            string[] name = null;

            dt.Load(cmd.ExecuteReader());
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                s_id[i] = Convert.ToInt32(row[0]);
                name[i] = Convert.ToString(row[1]);
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
            cmd.Connection = con;
            con.Open();

            DataTable dt1 = new DataTable();
            cmd.CommandText = "select c_id, username from TEST.students where section_name = '" + section_name.Text + "'";
            int c_id = 0;

            dt1.Load(cmd.ExecuteReader());
            foreach (DataRow row in dt1.Rows)
            {
                c_id = Convert.ToInt32(row[0]);
            }

            DataTable dt = new DataTable();
            cmd.CommandText = "select s_id, username from TEST.students where section_name = '" + section_name.Text + "'";
            int[] s_id = null;
            string[] name = null;

            dt.Load(cmd.ExecuteReader());
            int i = 0;
            foreach(DataRow row in dt.Rows) {
                s_id[i] = Convert.ToInt32(row[0]);
                name[i] = Convert.ToString(row[1]);

                cmd.CommandText = "insert into TEST.marks values(:max_marks, :min_marks, :s_id, :c_id, :username, :exam_name, :exam_date)";

                cmd.Parameters.Add(":max_marks", Convert.ToInt32(max_marks.Text));
                cmd.Parameters.Add(":min_marks", Convert.ToInt32(min_marks.Text));
                cmd.Parameters.Add(":s_id", s_id[i]);
                cmd.Parameters.Add(":c_id", c_id);
                cmd.Parameters.Add(":username", name[i]);
                cmd.Parameters.Add(":exam_name", exam_name.Text);
                cmd.Parameters.Add(":exam_date", DateTime.Now.ToString("dd-MMM-yyyy"));

                i++;
            }
            con.Close();
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
            e.Graphics.DrawString("Section Name : " + section_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Course Code : " + course_code.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Course name : " + course_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString("Exam Name : " + exam_name.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 290));
            e.Graphics.DrawString("Max Marks : " + max_marks.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 320));
            e.Graphics.DrawString("Min Marks : " + min_marks.Text, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 350));
            e.Graphics.DrawString("-----------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 380));
            e.Graphics.DrawString("@All Rights Reserved For Nekron, Contact NO: +090078601", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 410));
            e.Graphics.DrawString("Address: Comsats University Islamabad Sahiwal Campus", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.Black, new Point(25, 470));

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            this.section_name.Text = "";
            this.course_code.Text = "";
            this.course_name.Text = "";
            this.exam_name.Text = "";
            this.max_marks.Text = "";
            this.min_marks.Text = "";
        }

        private void update_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
            cmd.Connection = con;
            con.Open();

            DataTable dt = new DataTable();
            cmd.CommandText = "select * from TEST.marks";

            dt.Load(cmd.ExecuteReader());
            result_view.DataSource = dt.DefaultView;


            OracleDataAdapter oda = new OracleDataAdapter();
            oda.Fill(dt);
            OracleCommandBuilder ocb = new OracleCommandBuilder(oda);
            oda.Update(dt);
            result_view.DataSource = dt.DefaultView;
        }
    }
}
