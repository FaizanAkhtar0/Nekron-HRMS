using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron
{
    public partial class DataBaseBackUp : Form
    {

        private string StorePath;
        public DataBaseBackUp()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.StorePath = fbd.SelectedPath;
                this.back_path.Text = fbd.SelectedPath;
                LoadImgChecked.Visible = true;
                LoadImgChecked.BringToFront();
            }
        }

        private void LoadImgChecked_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {
            bunifuCheckbox1.Checked = true;
        }
    }
}
