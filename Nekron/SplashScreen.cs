using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron {
    public partial class SplashScreen : Form  {

        private List<Color> colors = new List<Color>();

        public SplashScreen() {
            colors.Add(Color.FromArgb(0, 156, 71));
            colors.Add(Color.FromArgb(112, 191, 83));
            colors.Add(Color.FromArgb(216, 155, 40));
            colors.Add(Color.FromArgb(217, 102, 41));
            colors.Add(Color.FromArgb(217, 102, 41));
            colors.Add(Color.FromArgb(235, 83, 104));
            colors.Add(Color.FromArgb(223, 128, 255));
            colors.Add(Color.FromArgb(112, 48, 160));
            colors.Add(Color.FromArgb(102, 112, 187));
            colors.Add(Color.FromArgb(95, 136, 176));
            colors.Add(Color.FromArgb(70, 175, 227));
            colors.Add(Color.FromArgb(0, 156, 71));

            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e) {
            timer1.Start();
            bunifuCustomLabel1.ForeColor = Color.White;
        }

        private int cur_color = 0;
        private int loop = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!(bunifuProgressBar1.Value == bunifuProgressBar1.MaximumValue))
            {
                bunifuProgressBar1.Value = bunifuProgressBar1.Value + 1;
                if (bunifuProgressBar1.Value == 1)
                {
                    bunifuProgressBar1.ProgressColor = Color.DeepSkyBlue;
                }
                else if (bunifuProgressBar1.Value == 70)
                {
                    bunifuCustomLabel1.Text = "Loading.";
                }
                else if (bunifuProgressBar1.Value == 150)
                {
                    bunifuCustomLabel1.Text = "Loading..";
                }
                else if (bunifuProgressBar1.Value == 200)
                {
                    bunifuCustomLabel1.Text = "Loading...";
                }
                else if (bunifuProgressBar1.Value == 250)
                {
                    bunifuCustomLabel1.Text = "Loading....";
                }
                else if (bunifuProgressBar1.Value == 300)
                {
                    bunifuCustomLabel1.Text = "Loading.";
                    bunifuProgressBar1.ProgressColor = Color.DodgerBlue;
                }
                else if (bunifuProgressBar1.Value == 320)
                {
                    bunifuCustomLabel1.Text = "Loading..";
                }
                else if (bunifuProgressBar1.Value == 325)
                {
                    bunifuCustomLabel1.Text = "Loading Components... Please wait.";
                }
                else if (bunifuProgressBar1.Value == 330)
                {
                    bunifuCustomLabel1.Text = "Loading Components... Please wait.";
                }
                else if (bunifuProgressBar1.Value == 350)
                {
                    bunifuCustomLabel1.Text = "Loading...";
                }
                else if (bunifuProgressBar1.Value == 390)
                {
                    bunifuCustomLabel1.Text = "Loading....";
                }
                else if (bunifuProgressBar1.Value == 400)
                {
                    bunifuCustomLabel1.Text = "Loading.";
                    bunifuProgressBar1.ProgressColor = Color.Blue;
                }
                else if (bunifuProgressBar1.Value == 415)
                {
                    bunifuCustomLabel1.Text = "Loading Components... Please wait.";
                }
                else if (bunifuProgressBar1.Value == 430)
                {
                    bunifuCustomLabel1.Text = "Loading..";
                }
                else if (bunifuProgressBar1.Value == 460)
                {
                    bunifuCustomLabel1.Text = "Loading...";
                }
                else if (bunifuProgressBar1.Value == 465)
                {
                    bunifuCustomLabel1.Text = "Loading....";
                }
                else if (bunifuProgressBar1.Value == 470)
                {
                    bunifuCustomLabel1.Text = "Loading.";
                }
                else if (bunifuProgressBar1.Value == 480)
                {
                    bunifuCustomLabel1.Text = "Loading..";
                }
                else if (bunifuProgressBar1.Value == 485)
                {
                    bunifuCustomLabel1.Text = "Loading...";
                }
                else if (bunifuProgressBar1.Value == 490)
                {
                    bunifuCustomLabel1.Text = "Loading....";
                }
                else if (bunifuProgressBar1.Value == 495)
                {
                    bunifuCustomLabel1.Text = "Loading....";
                }
                else if (bunifuProgressBar1.Value == 500)
                {
                    bunifuCustomLabel1.Text = "Starting up Nekron...";
                    bunifuProgressBar1.ProgressColor = Color.Navy;
                    bunifuCustomLabel1.ForeColor = Color.Aqua;
                }
                else if (bunifuProgressBar1.Value == 700)
                {
                    bunifuCustomLabel1.Text = "Setting up the interface...";
                    bunifuCustomLabel1.ForeColor = Color.Indigo;
                }
                else if (bunifuProgressBar1.Value == 800)
                {
                    bunifuCustomLabel1.Text = "Done...";
                    bunifuCustomLabel1.ForeColor = Color.Purple;
                    Login login = new Login();
                    login.Visible = true;
                    login.Show();
                    this.Hide();
                }
            }
            timer1.Enabled = false;
            if (cur_color < colors.Count - 1)
            {
                this.BackColor = Bunifu.Core.Drawing.BunifuColorTransition.GetColorScale(loop, colors[cur_color], colors[cur_color + 1]);
                if (loop < 100)
                {
                    loop++;
                }
                else
                {
                    loop = 0;
                    cur_color++;
                }
                timer1.Enabled = true;
            }
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
