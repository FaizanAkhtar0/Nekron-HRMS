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
using System.Net;
using System.IO;

namespace Nekron
{
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            BatchcomboBox2.Items.Clear();
            for (int i = 0; i < 1980; i++)
            {
                BatchcomboBox2.Items.Add(DateTime.Now.AddYears(-i).Year.ToString());
                if (BatchcomboBox2.Items.Contains("1996"))
                {
                    break;
                }
            }

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
                        OracleCommand cmd1 = new OracleCommand("select full_name,s_id from HRM.students where class_name='" + ClasscomboBox3.SelectedItem.ToString() + "' and SUBSTR(Admission_date,-4,4)='" + BatchcomboBox2.SelectedItem.ToString() + "' ", con);


                        // cmd1.CommandText = "select full_name from HRM.students where class_name='" + ClasscomboBox3.SelectedValue.ToString() + "' ";
                        //cmd1.CommandType = System.Data.CommandType.Text;
                        OracleDataAdapter da = new OracleDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        st_sid_clm.DataPropertyName = dt.Columns["s_id"].ToString();
                        st_name_clm.DataPropertyName = dt.Columns["full_name"].ToString();

                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                con.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        int x;
        private void ClasscomboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            students();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection("Data Source=XE;User ID=system;password=123;");
            if (BatchcomboBox2.SelectedIndex != -1 || ClasscomboBox3.SelectedIndex != -1)
            {
                try
                {

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        var cmd = "insert into HRM.attendance(S_ROLL_NO,ATTENDANCE,DATE_OF_ATTENDANCE,ST_NAME) values('" + dataGridView1.Rows[i].Cells["st_sid_clm"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["st_attendance_clm"].Value.ToString() + "','" + Date.Value.ToString() + "','" + dataGridView1.Rows[i].Cells["st_name_clm"].Value.ToString() + "')";

                        using (con)
                        {
                            using (OracleCommand command = new OracleCommand(cmd, con))
                            {
                                //OracleParameter gradeParam = new OracleParameter("@sr", SqlDbType.Int);
                                //gradeParam.Value = dataGridView1.Rows[i].Cells["st_sid_clm"].Value.ToString();
                                //command.Parameters.Add(gradeParam);
                                //OracleParameter gradePara = new OracleParameter("@att", SqlDbType.VarChar);
                                //gradePara.Value = dataGridView1.Rows[i].Cells["st_attendance_clm"].Value.ToString();
                                //command.Parameters.Add(gradePara);
                                //OracleParameter gradePar = new OracleParameter("@dt", SqlDbType.VarChar);
                                //gradePar.Value = Date.Value.ToString();
                                //command.Parameters.Add(gradePar);
                                //OracleParameter gradePa = new OracleParameter("@sn", SqlDbType.VarChar);
                                //gradePa.Value = dataGridView1.Rows[i].Cells["st_name_clm"].Value.ToString();
                                //command.Parameters.Add(gradePa);




                                con.Open();
                                x = command.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    if (x > 0)
                    {
                        MessageBox.Show("Attendance Updated");
                    }

                    try
                    {
                        OracleCommand cmd = new OracleCommand();

                        OracleConnection con1 = new OracleConnection("Data Source=XE;User ID=system;password=123;Unicode=true");
                        cmd.Connection = con1;
                        con1.Open();
                        DataTable dt = new DataTable();
                        cmd.CommandText = "select s_name from HRM.attandance where attandance = A";

                        dt.Load(cmd.ExecuteReader());
                        string[] names = null;
                        int i = 0;
                        foreach(DataRow row in dt.Rows) {
                            names[i] = Convert.ToString(row[0]);
                            i++;
                        }
                        con1.Close();

                        con1.Open();
                        int ii = 0;
                        DataTable dt_contacts = new DataTable();
                        foreach(string s in names) {
                            
                            cmd.CommandText = "select guardian_contact_no from HRM.students where username = '" + names[ii] + "'";
                            ii++;

                            dt.Load(cmd.ExecuteReader());

                            string contactNo = null;

                            foreach(DataRow row1 in dt_contacts.Rows) {
                                contactNo = Convert.ToString(row1[0]);
                            }



                            String result;
                            string apiKey = "axd34560me7";
                            string message = "You child is abscent today!";
                            string xender = "Nekron INC.";

                            String url = "https://api.txtlocal.com/send/?apikey=" + apiKey + "&numbers=" + contactNo + "&message=" + message + "&sender=" + xender;
                            //refer to parameters to complete correct url string

                            StreamWriter myWriter = null;
                            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                            objRequest.Method = "POST";
                            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                            objRequest.ContentType = "application/x-www-form-urlencoded";
                            try
                            {
                                myWriter = new StreamWriter(objRequest.GetRequestStream());
                                myWriter.Write(url);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                myWriter.Close();
                            }

                            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                            {
                                result = sr.ReadToEnd();
                                // Close and clean up the StreamReader
                                sr.Close();
                            }



                        }
                    } catch (Exception ex) {

                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Attendance j = new Attendance();
            j.Hide();

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
