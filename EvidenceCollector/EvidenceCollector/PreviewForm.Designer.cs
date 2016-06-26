namespace EvidenceCollector
{
    partial class PreviewForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.dockButton = new System.Windows.Forms.Button();
            this.previewFormNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.previousButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nextButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dockButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(712, 46);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Tag = "";
            // 
            // previousButton
            // 
            this.previousButton.AutoSize = true;
            this.previousButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.previousButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previousButton.Location = new System.Drawing.Point(74, 3);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(136, 40);
            this.previousButton.TabIndex = 2;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.AutoSize = true;
            this.nextButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Location = new System.Drawing.Point(572, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(137, 40);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            // 
            // dockButton
            // 
            this.dockButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockButton.Location = new System.Drawing.Point(3, 3);
            this.dockButton.Name = "dockButton";
            this.dockButton.Size = new System.Drawing.Size(65, 40);
            this.dockButton.TabIndex = 4;
            this.dockButton.Text = "Dock";
            this.dockButton.UseVisualStyleBackColor = true;
            this.dockButton.Click += new System.EventHandler(this.dockButton_Click);
            // 
            // previewFormNotifyIcon
            // 
            this.previewFormNotifyIcon.Text = "PreviewFormNotifyIcon";
            this.previewFormNotifyIcon.Visible = true;
            this.previewFormNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.previewFormNotifyIcon_MouseClick);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 46);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "PreviewForm";
            this.ShowIcon = false;
            this.Text = "remove this";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button dockButton;
        private System.Windows.Forms.NotifyIcon previewFormNotifyIcon;
    }
}