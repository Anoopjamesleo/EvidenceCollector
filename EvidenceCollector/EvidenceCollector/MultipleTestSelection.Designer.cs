namespace EvidenceCollector
{
    partial class MultipleTestSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleTestSelection));
            this.MTSGrid = new System.Windows.Forms.DataGridView();
            this.TestPlanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestPlanPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MTSGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MTSGrid
            // 
            this.MTSGrid.AllowUserToAddRows = false;
            this.MTSGrid.AllowUserToDeleteRows = false;
            this.MTSGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MTSGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TestPlanName,
            this.TestPlanPath});
            this.MTSGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MTSGrid.Location = new System.Drawing.Point(0, 0);
            this.MTSGrid.Name = "MTSGrid";
            this.MTSGrid.ReadOnly = true;
            this.MTSGrid.Size = new System.Drawing.Size(594, 137);
            this.MTSGrid.TabIndex = 0;
            this.MTSGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MTSGrid_CellDoubleClick);
            // 
            // TestPlanName
            // 
            this.TestPlanName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TestPlanName.DefaultCellStyle = dataGridViewCellStyle1;
            this.TestPlanName.HeaderText = "TestPlanName";
            this.TestPlanName.Name = "TestPlanName";
            this.TestPlanName.ReadOnly = true;
            // 
            // TestPlanPath
            // 
            this.TestPlanPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TestPlanPath.DefaultCellStyle = dataGridViewCellStyle2;
            this.TestPlanPath.HeaderText = "TestPlanPath";
            this.TestPlanPath.Name = "TestPlanPath";
            this.TestPlanPath.ReadOnly = true;
            // 
            // MultipleTestSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(594, 137);
            this.Controls.Add(this.MTSGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultipleTestSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MultipleTestSelection";
            ((System.ComponentModel.ISupportInitialize)(this.MTSGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView MTSGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestPlanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestPlanPath;
    }
}