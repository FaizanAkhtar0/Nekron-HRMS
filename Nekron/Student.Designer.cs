namespace Nekron
{
    partial class Student
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Student));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.st_sid_clm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.st_name_clm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.st_class_clm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.st_omarks_clm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.st_maxmarks_clm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bunifuCustomLabel11 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.usrname = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuImageButton4 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.view = new Bunifu.Framework.UI.BunifuThinButton2();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 5;
            this.bunifuElipse2.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(98)))), ((int)(((byte)(212)))));
            this.panel1.Controls.Add(this.bunifuImageButton4);
            this.panel1.Controls.Add(this.bunifuImageButton2);
            this.panel1.Controls.Add(this.bunifuCustomLabel2);
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Location = new System.Drawing.Point(-2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 31);
            this.panel1.TabIndex = 306;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(37, 6);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(179, 19);
            this.bunifuCustomLabel2.TabIndex = 3;
            this.bunifuCustomLabel2.Text = "Nekron - Student View";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.st_sid_clm,
            this.st_name_clm,
            this.st_class_clm,
            this.st_omarks_clm,
            this.st_maxmarks_clm});
            this.dataGridView1.Location = new System.Drawing.Point(11, 185);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1025, 313);
            this.dataGridView1.TabIndex = 310;
            // 
            // st_sid_clm
            // 
            this.st_sid_clm.HeaderText = "Student ID";
            this.st_sid_clm.Name = "st_sid_clm";
            this.st_sid_clm.ReadOnly = true;
            // 
            // st_name_clm
            // 
            this.st_name_clm.HeaderText = "Name";
            this.st_name_clm.Name = "st_name_clm";
            this.st_name_clm.ReadOnly = true;
            // 
            // st_class_clm
            // 
            this.st_class_clm.HeaderText = "Class";
            this.st_class_clm.Name = "st_class_clm";
            this.st_class_clm.ReadOnly = true;
            // 
            // st_omarks_clm
            // 
            this.st_omarks_clm.HeaderText = "Obtained Marks";
            this.st_omarks_clm.Name = "st_omarks_clm";
            this.st_omarks_clm.ReadOnly = true;
            // 
            // st_maxmarks_clm
            // 
            this.st_maxmarks_clm.HeaderText = "Maximum Marks";
            this.st_maxmarks_clm.Name = "st_maxmarks_clm";
            this.st_maxmarks_clm.ReadOnly = true;
            // 
            // bunifuCustomLabel11
            // 
            this.bunifuCustomLabel11.AutoSize = true;
            this.bunifuCustomLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel11.ForeColor = System.Drawing.Color.DarkViolet;
            this.bunifuCustomLabel11.Location = new System.Drawing.Point(21, 65);
            this.bunifuCustomLabel11.Name = "bunifuCustomLabel11";
            this.bunifuCustomLabel11.Size = new System.Drawing.Size(74, 16);
            this.bunifuCustomLabel11.TabIndex = 309;
            this.bunifuCustomLabel11.Text = "Username:";
            // 
            // usrname
            // 
            this.usrname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.usrname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.usrname.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.usrname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usrname.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrname.ForeColor = System.Drawing.Color.Black;
            this.usrname.HintForeColor = System.Drawing.Color.Empty;
            this.usrname.HintText = "";
            this.usrname.isPassword = false;
            this.usrname.LineFocusedColor = System.Drawing.Color.Blue;
            this.usrname.LineIdleColor = System.Drawing.Color.Magenta;
            this.usrname.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.usrname.LineThickness = 3;
            this.usrname.Location = new System.Drawing.Point(96, 50);
            this.usrname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.usrname.MaxLength = 32767;
            this.usrname.Name = "usrname";
            this.usrname.Size = new System.Drawing.Size(215, 31);
            this.usrname.TabIndex = 308;
            this.usrname.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuImageButton4
            // 
            this.bunifuImageButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuImageButton4.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton4.Image")));
            this.bunifuImageButton4.ImageActive = null;
            this.bunifuImageButton4.Location = new System.Drawing.Point(3, 1);
            this.bunifuImageButton4.Name = "bunifuImageButton4";
            this.bunifuImageButton4.Size = new System.Drawing.Size(30, 28);
            this.bunifuImageButton4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton4.TabIndex = 7;
            this.bunifuImageButton4.TabStop = false;
            this.bunifuImageButton4.Zoom = 10;
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(978, 1);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(30, 28);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 4;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            this.bunifuImageButton2.Click += new System.EventHandler(this.bunifuImageButton2_Click);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.BackgroundImage")));
            this.bunifuImageButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(1011, 1);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(27, 28);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 2;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // view
            // 
            this.view.ActiveBorderThickness = 1;
            this.view.ActiveCornerRadius = 30;
            this.view.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(98)))), ((int)(((byte)(212)))));
            this.view.ActiveForecolor = System.Drawing.Color.White;
            this.view.ActiveLineColor = System.Drawing.Color.MediumTurquoise;
            this.view.BackColor = System.Drawing.SystemColors.Control;
            this.view.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("view.BackgroundImage")));
            this.view.ButtonText = "Student Marks";
            this.view.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view.ForeColor = System.Drawing.Color.SeaGreen;
            this.view.IdleBorderThickness = 1;
            this.view.IdleCornerRadius = 30;
            this.view.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.view.IdleForecolor = System.Drawing.Color.White;
            this.view.IdleLineColor = System.Drawing.Color.Aqua;
            this.view.Location = new System.Drawing.Point(142, 103);
            this.view.Margin = new System.Windows.Forms.Padding(4);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(135, 47);
            this.view.TabIndex = 307;
            this.view.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.view.Click += new System.EventHandler(this.view_Click);
            // 
            // Student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 504);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bunifuCustomLabel11);
            this.Controls.Add(this.usrname);
            this.Controls.Add(this.view);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Student";
            this.Text = "Student";
            this.Load += new System.EventHandler(this.Student_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton4;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn st_sid_clm;
        private System.Windows.Forms.DataGridViewTextBoxColumn st_name_clm;
        private System.Windows.Forms.DataGridViewTextBoxColumn st_class_clm;
        private System.Windows.Forms.DataGridViewTextBoxColumn st_omarks_clm;
        private System.Windows.Forms.DataGridViewTextBoxColumn st_maxmarks_clm;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel11;
        private Bunifu.Framework.UI.BunifuMaterialTextbox usrname;
        private Bunifu.Framework.UI.BunifuThinButton2 view;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
    }
}