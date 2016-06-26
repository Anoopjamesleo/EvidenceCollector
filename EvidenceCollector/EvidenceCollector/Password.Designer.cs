namespace EvidenceCollector
{
    partial class Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LblQcPath = new System.Windows.Forms.Label();
            this.configTextBox1 = new CustomControl.ConfigTextBox();
            this.lblQCDomain = new System.Windows.Forms.Label();
            this.Project = new System.Windows.Forms.Label();
            this.QCProject = new System.Windows.Forms.ComboBox();
            this.QCDomain = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Your QC Password:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(97, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(205, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(308, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 38);
            this.button1.TabIndex = 2;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.0231F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.9769F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Controls.Add(this.LblQcPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.configTextBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblQCDomain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Project, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.QCProject, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.QCDomain, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(358, 127);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // LblQcPath
            // 
            this.LblQcPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LblQcPath.AutoSize = true;
            this.LblQcPath.Location = new System.Drawing.Point(3, 6);
            this.LblQcPath.Name = "LblQcPath";
            this.LblQcPath.Size = new System.Drawing.Size(88, 13);
            this.LblQcPath.TabIndex = 3;
            this.LblQcPath.Text = "QC Path";
            // 
            // configTextBox1
            // 
            this.configTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.configTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.configTextBox1.Location = new System.Drawing.Point(97, 3);
            this.configTextBox1.Name = "configTextBox1";
            this.configTextBox1.Size = new System.Drawing.Size(205, 20);
            this.configTextBox1.TabIndex = 8;
            // 
            // lblQCDomain
            // 
            this.lblQCDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQCDomain.AutoSize = true;
            this.lblQCDomain.Location = new System.Drawing.Point(3, 33);
            this.lblQCDomain.Name = "lblQCDomain";
            this.lblQCDomain.Size = new System.Drawing.Size(88, 13);
            this.lblQCDomain.TabIndex = 7;
            this.lblQCDomain.Text = "Domain";
            // 
            // Project
            // 
            this.Project.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Project.AutoSize = true;
            this.Project.Location = new System.Drawing.Point(3, 60);
            this.Project.Name = "Project";
            this.Project.Size = new System.Drawing.Size(88, 13);
            this.Project.TabIndex = 6;
            this.Project.Text = "Project";
            // 
            // QCProject
            // 
            this.QCProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.QCProject.FormattingEnabled = true;
            this.QCProject.Items.AddRange(new object[] {
            "TD_VALIDATION_TESTS",
            "TD_FEATURE"});
            this.QCProject.Location = new System.Drawing.Point(97, 56);
            this.QCProject.Name = "QCProject";
            this.QCProject.Size = new System.Drawing.Size(205, 21);
            this.QCProject.TabIndex = 4;
            // 
            // QCDomain
            // 
            this.QCDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.QCDomain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.QCDomain.FormattingEnabled = true;
            this.QCDomain.Items.AddRange(new object[] {
            "IP"});
            this.QCDomain.Location = new System.Drawing.Point(97, 29);
            this.QCDomain.Name = "QCDomain";
            this.QCDomain.Size = new System.Drawing.Size(205, 21);
            this.QCDomain.TabIndex = 5;
            // 
            // Password
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 127);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LblQcPath;
        private CustomControl.ConfigTextBox configTextBox1;
        private System.Windows.Forms.Label lblQCDomain;
        private System.Windows.Forms.Label Project;
        private System.Windows.Forms.ComboBox QCProject;
        private System.Windows.Forms.ComboBox QCDomain;
    }
}