namespace EvidenceCollector
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.browseTemplateFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseTargetFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.browseTestPlanDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btPreviewDocumentButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.processButton = new System.Windows.Forms.Button();
            this.doneCapturingButton = new System.Windows.Forms.Button();
            this.associateIDTextBox = new CustomControl.ConfigTextBox();
            this.associateIDLabel = new System.Windows.Forms.Label();
            this.solutionTextBox = new CustomControl.ConfigTextBox();
            this.operatingSystemTextBox = new CustomControl.ConfigTextBox();
            this.environmentTextBox = new CustomControl.ConfigTextBox();
            this.domainTextBox = new CustomControl.ConfigTextBox();
            this.solutionsLabel = new System.Windows.Forms.Label();
            this.operatingSystemLabel = new System.Windows.Forms.Label();
            this.environmentLabel = new System.Windows.Forms.Label();
            this.statusHintLabel = new System.Windows.Forms.Label();
            this.evidenceCaptureStatusLabel = new System.Windows.Forms.Label();
            this.domainLabel = new System.Windows.Forms.Label();
            this.imgPreviewBox = new System.Windows.Forms.PictureBox();
            this.browseTargetFolderButton = new System.Windows.Forms.Button();
            this.testPlanFilePathBox = new CustomControl.ConfigTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.browseTemplateFileButton = new System.Windows.Forms.Button();
            this.templateFilePathBox = new CustomControl.ConfigTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browseTestPlanButton = new System.Windows.Forms.Button();
            this.targetFolderPathBox = new CustomControl.ConfigTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.previewDataGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.preRequisiteBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.QCCheck = new System.Windows.Forms.CheckBox();
            this.cbPrerequisiteEvidencesCheckbox = new System.Windows.Forms.CheckBox();
            this.cbSecondaryScreenCheckbox = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreviewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseTemplateFileDialog
            // 
            this.browseTemplateFileDialog.InitialDirectory = "C:\\Users\\AV045929";
            // 
            // browseTestPlanDialog
            // 
            this.browseTestPlanDialog.InitialDirectory = "C:\\Users\\AV045929";
            // 
            // btPreviewDocumentButton
            // 
            this.btPreviewDocumentButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btPreviewDocumentButton.Location = new System.Drawing.Point(483, 84);
            this.btPreviewDocumentButton.Name = "btPreviewDocumentButton";
            this.btPreviewDocumentButton.Size = new System.Drawing.Size(54, 44);
            this.btPreviewDocumentButton.TabIndex = 6;
            this.btPreviewDocumentButton.Text = "Preview";
            this.toolTip1.SetToolTip(this.btPreviewDocumentButton, "Preview target document");
            this.btPreviewDocumentButton.UseVisualStyleBackColor = true;
            this.btPreviewDocumentButton.Click += new System.EventHandler(this.btPreviewDocumentButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.processButton);
            this.panel2.Controls.Add(this.doneCapturingButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(543, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(54, 44);
            this.panel2.TabIndex = 39;
            // 
            // processButton
            // 
            this.processButton.AutoSize = true;
            this.processButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processButton.Location = new System.Drawing.Point(0, 0);
            this.processButton.Margin = new System.Windows.Forms.Padding(2);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(54, 44);
            this.processButton.TabIndex = 1;
            this.processButton.Text = "Load";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Visible = false;
            this.processButton.Click += new System.EventHandler(this.processButtonEvent);
            // 
            // doneCapturingButton
            // 
            this.doneCapturingButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doneCapturingButton.Location = new System.Drawing.Point(0, 0);
            this.doneCapturingButton.Name = "doneCapturingButton";
            this.doneCapturingButton.Size = new System.Drawing.Size(54, 44);
            this.doneCapturingButton.TabIndex = 31;
            this.doneCapturingButton.Text = "Done";
            this.doneCapturingButton.UseVisualStyleBackColor = true;
            this.doneCapturingButton.Click += new System.EventHandler(this.doneCapturing_Click);
            // 
            // associateIDTextBox
            // 
            this.associateIDTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.associateIDTextBox, 2);
            this.associateIDTextBox.Location = new System.Drawing.Point(99, 238);
            this.associateIDTextBox.Name = "associateIDTextBox";
            this.associateIDTextBox.Size = new System.Drawing.Size(438, 20);
            this.associateIDTextBox.TabIndex = 11;
            // 
            // associateIDLabel
            // 
            this.associateIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.associateIDLabel.AutoSize = true;
            this.associateIDLabel.Location = new System.Drawing.Point(3, 241);
            this.associateIDLabel.Name = "associateIDLabel";
            this.associateIDLabel.Size = new System.Drawing.Size(90, 13);
            this.associateIDLabel.TabIndex = 48;
            this.associateIDLabel.Text = "Associate ID";
            // 
            // solutionTextBox
            // 
            this.solutionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.solutionTextBox, 2);
            this.solutionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionTextBox.Location = new System.Drawing.Point(99, 212);
            this.solutionTextBox.Name = "solutionTextBox";
            this.solutionTextBox.Size = new System.Drawing.Size(438, 20);
            this.solutionTextBox.TabIndex = 10;
            // 
            // operatingSystemTextBox
            // 
            this.operatingSystemTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.operatingSystemTextBox, 2);
            this.operatingSystemTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatingSystemTextBox.Location = new System.Drawing.Point(99, 186);
            this.operatingSystemTextBox.Name = "operatingSystemTextBox";
            this.operatingSystemTextBox.Size = new System.Drawing.Size(438, 20);
            this.operatingSystemTextBox.TabIndex = 9;
            // 
            // environmentTextBox
            // 
            this.environmentTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.environmentTextBox, 2);
            this.environmentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.environmentTextBox.Location = new System.Drawing.Point(99, 160);
            this.environmentTextBox.Name = "environmentTextBox";
            this.environmentTextBox.Size = new System.Drawing.Size(438, 20);
            this.environmentTextBox.TabIndex = 8;
            // 
            // domainTextBox
            // 
            this.domainTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.domainTextBox, 2);
            this.domainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.domainTextBox.Location = new System.Drawing.Point(99, 134);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(438, 20);
            this.domainTextBox.TabIndex = 7;
            // 
            // solutionsLabel
            // 
            this.solutionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionsLabel.AutoSize = true;
            this.solutionsLabel.Location = new System.Drawing.Point(3, 215);
            this.solutionsLabel.Name = "solutionsLabel";
            this.solutionsLabel.Size = new System.Drawing.Size(90, 13);
            this.solutionsLabel.TabIndex = 44;
            this.solutionsLabel.Text = "Solutions";
            // 
            // operatingSystemLabel
            // 
            this.operatingSystemLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.operatingSystemLabel.AutoSize = true;
            this.operatingSystemLabel.Location = new System.Drawing.Point(3, 189);
            this.operatingSystemLabel.Name = "operatingSystemLabel";
            this.operatingSystemLabel.Size = new System.Drawing.Size(90, 13);
            this.operatingSystemLabel.TabIndex = 43;
            this.operatingSystemLabel.Text = "Operating System";
            // 
            // environmentLabel
            // 
            this.environmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.environmentLabel.AutoSize = true;
            this.environmentLabel.Location = new System.Drawing.Point(3, 163);
            this.environmentLabel.Name = "environmentLabel";
            this.environmentLabel.Size = new System.Drawing.Size(90, 13);
            this.environmentLabel.TabIndex = 42;
            this.environmentLabel.Text = "Environment";
            // 
            // statusHintLabel
            // 
            this.statusHintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.statusHintLabel.AutoSize = true;
            this.statusHintLabel.Location = new System.Drawing.Point(2, 99);
            this.statusHintLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusHintLabel.Name = "statusHintLabel";
            this.statusHintLabel.Size = new System.Drawing.Size(92, 13);
            this.statusHintLabel.TabIndex = 19;
            this.statusHintLabel.Text = "Status: ";
            // 
            // evidenceCaptureStatusLabel
            // 
            this.evidenceCaptureStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.evidenceCaptureStatusLabel.AutoSize = true;
            this.evidenceCaptureStatusLabel.Location = new System.Drawing.Point(98, 99);
            this.evidenceCaptureStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.evidenceCaptureStatusLabel.Name = "evidenceCaptureStatusLabel";
            this.evidenceCaptureStatusLabel.Size = new System.Drawing.Size(71, 13);
            this.evidenceCaptureStatusLabel.TabIndex = 18;
            this.evidenceCaptureStatusLabel.Text = "current status";
            // 
            // domainLabel
            // 
            this.domainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(3, 137);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(90, 13);
            this.domainLabel.TabIndex = 40;
            this.domainLabel.Text = "Domain";
            // 
            // imgPreviewBox
            // 
            this.imgPreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgPreviewBox.Location = new System.Drawing.Point(0, 0);
            this.imgPreviewBox.Name = "imgPreviewBox";
            this.imgPreviewBox.Size = new System.Drawing.Size(601, 222);
            this.imgPreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPreviewBox.TabIndex = 29;
            this.imgPreviewBox.TabStop = false;
            // 
            // browseTargetFolderButton
            // 
            this.browseTargetFolderButton.AutoSize = true;
            this.browseTargetFolderButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseTargetFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseTargetFolderButton.Location = new System.Drawing.Point(542, 56);
            this.browseTargetFolderButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseTargetFolderButton.Name = "browseTargetFolderButton";
            this.browseTargetFolderButton.Size = new System.Drawing.Size(56, 23);
            this.browseTargetFolderButton.TabIndex = 5;
            this.browseTargetFolderButton.Text = "...";
            this.browseTargetFolderButton.UseVisualStyleBackColor = true;
            this.browseTargetFolderButton.Click += new System.EventHandler(this.browseTargetFolder);
            // 
            // testPlanFilePathBox
            // 
            this.testPlanFilePathBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.testPlanFilePathBox, 2);
            this.testPlanFilePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testPlanFilePathBox.Location = new System.Drawing.Point(99, 3);
            this.testPlanFilePathBox.Name = "testPlanFilePathBox";
            this.testPlanFilePathBox.Size = new System.Drawing.Size(438, 20);
            this.testPlanFilePathBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Template";
            // 
            // browseTemplateFileButton
            // 
            this.browseTemplateFileButton.AutoSize = true;
            this.browseTemplateFileButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseTemplateFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseTemplateFileButton.Location = new System.Drawing.Point(542, 29);
            this.browseTemplateFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseTemplateFileButton.Name = "browseTemplateFileButton";
            this.browseTemplateFileButton.Size = new System.Drawing.Size(56, 23);
            this.browseTemplateFileButton.TabIndex = 3;
            this.browseTemplateFileButton.Text = "...";
            this.browseTemplateFileButton.UseVisualStyleBackColor = true;
            this.browseTemplateFileButton.Click += new System.EventHandler(this.browseTemplateFile);
            // 
            // templateFilePathBox
            // 
            this.templateFilePathBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.templateFilePathBox, 2);
            this.templateFilePathBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateFilePathBox.Location = new System.Drawing.Point(99, 30);
            this.templateFilePathBox.Name = "templateFilePathBox";
            this.templateFilePathBox.Size = new System.Drawing.Size(438, 20);
            this.templateFilePathBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Target folder";
            // 
            // browseTestPlanButton
            // 
            this.browseTestPlanButton.AutoSize = true;
            this.browseTestPlanButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseTestPlanButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseTestPlanButton.Location = new System.Drawing.Point(542, 2);
            this.browseTestPlanButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseTestPlanButton.Name = "browseTestPlanButton";
            this.browseTestPlanButton.Size = new System.Drawing.Size(56, 23);
            this.browseTestPlanButton.TabIndex = 1;
            this.browseTestPlanButton.Text = "...";
            this.browseTestPlanButton.UseVisualStyleBackColor = true;
            this.browseTestPlanButton.Click += new System.EventHandler(this.browseTestPlan);
            // 
            // targetFolderPathBox
            // 
            this.targetFolderPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.targetFolderPathBox.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.targetFolderPathBox, 2);
            this.targetFolderPathBox.Location = new System.Drawing.Point(99, 57);
            this.targetFolderPathBox.Name = "targetFolderPathBox";
            this.targetFolderPathBox.Size = new System.Drawing.Size(438, 20);
            this.targetFolderPathBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Test Plan";
            // 
            // previewDataGrid
            // 
            this.previewDataGrid.AllowUserToAddRows = false;
            this.previewDataGrid.AllowUserToDeleteRows = false;
            this.previewDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.previewDataGrid.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.previewDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.previewDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.previewDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewDataGrid.Location = new System.Drawing.Point(0, 0);
            this.previewDataGrid.Name = "previewDataGrid";
            this.previewDataGrid.Size = new System.Drawing.Size(573, 222);
            this.previewDataGrid.TabIndex = 30;
            this.previewDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.previewDataGrid_CellContentClick);
            this.previewDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.previewDataGrid_CellEndEdit);
            this.previewDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.previewDataGrid_CellMouseClick);
            this.previewDataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.previewDataGrid_CellDoubleClick);
            this.previewDataGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.previewDataGrid_RowEnter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.96677F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.8791F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.15414F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetFolderPathBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.browseTestPlanButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.templateFilePathBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.browseTemplateFileButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.testPlanFilePathBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.browseTargetFolderButton, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.preRequisiteBox, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.domainLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.evidenceCaptureStatusLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.statusHintLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.environmentLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.operatingSystemLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.solutionsLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.domainTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.environmentTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.operatingSystemTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.solutionTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.associateIDTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btPreviewDocumentButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.associateIDLabel, 0, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1178, 331);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // preRequisiteBox
            // 
            this.preRequisiteBox.BackColor = System.Drawing.SystemColors.HighlightText;
            this.preRequisiteBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.preRequisiteBox, 2);
            this.preRequisiteBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preRequisiteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preRequisiteBox.Location = new System.Drawing.Point(603, 3);
            this.preRequisiteBox.Name = "preRequisiteBox";
            this.preRequisiteBox.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.preRequisiteBox, 10);
            this.preRequisiteBox.Size = new System.Drawing.Size(572, 325);
            this.preRequisiteBox.TabIndex = 32;
            this.preRequisiteBox.Text = "";
            this.preRequisiteBox.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.QCCheck);
            this.panel1.Controls.Add(this.cbPrerequisiteEvidencesCheckbox);
            this.panel1.Controls.Add(this.cbSecondaryScreenCheckbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(99, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 64);
            this.panel1.TabIndex = 38;
            // 
            // QCCheck
            // 
            this.QCCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QCCheck.AutoSize = true;
            this.QCCheck.Location = new System.Drawing.Point(180, 0);
            this.QCCheck.Name = "QCCheck";
            this.QCCheck.Size = new System.Drawing.Size(87, 17);
            this.QCCheck.TabIndex = 2;
            this.QCCheck.Text = "Pull From QC";
            this.QCCheck.UseVisualStyleBackColor = true;
            this.QCCheck.CheckedChanged += new System.EventHandler(this.QCCheck_CheckedChanged);
            // 
            // cbPrerequisiteEvidencesCheckbox
            // 
            this.cbPrerequisiteEvidencesCheckbox.AutoSize = true;
            this.cbPrerequisiteEvidencesCheckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPrerequisiteEvidencesCheckbox.Location = new System.Drawing.Point(0, 17);
            this.cbPrerequisiteEvidencesCheckbox.Name = "cbPrerequisiteEvidencesCheckbox";
            this.cbPrerequisiteEvidencesCheckbox.Size = new System.Drawing.Size(378, 17);
            this.cbPrerequisiteEvidencesCheckbox.TabIndex = 1;
            this.cbPrerequisiteEvidencesCheckbox.Text = "Prerequisite Evidences";
            this.cbPrerequisiteEvidencesCheckbox.UseVisualStyleBackColor = true;
            // 
            // cbSecondaryScreenCheckbox
            // 
            this.cbSecondaryScreenCheckbox.AutoSize = true;
            this.cbSecondaryScreenCheckbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSecondaryScreenCheckbox.Location = new System.Drawing.Point(0, 0);
            this.cbSecondaryScreenCheckbox.Name = "cbSecondaryScreenCheckbox";
            this.cbSecondaryScreenCheckbox.Size = new System.Drawing.Size(378, 17);
            this.cbSecondaryScreenCheckbox.TabIndex = 0;
            this.cbSecondaryScreenCheckbox.Text = "Use Secondary Screen";
            this.cbSecondaryScreenCheckbox.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.imgPreviewBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.previewDataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 222);
            this.splitContainer1.SplitterDistance = 601;
            this.splitContainer1.TabIndex = 50;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(20, 20);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1178, 557);
            this.splitContainer2.SplitterDistance = 331;
            this.splitContainer2.TabIndex = 29;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1218, 597);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Evidence Collector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreviewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.OpenFileDialog browseTemplateFileDialog;
        private System.Windows.Forms.FolderBrowserDialog browseTargetFolderDialog;
        private System.Windows.Forms.OpenFileDialog browseTestPlanDialog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btPreviewDocumentButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.Button doneCapturingButton;
        private CustomControl.ConfigTextBox associateIDTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private CustomControl.ConfigTextBox targetFolderPathBox;
        private System.Windows.Forms.Button browseTestPlanButton;
        private System.Windows.Forms.Label label3;
        private CustomControl.ConfigTextBox templateFilePathBox;
        private System.Windows.Forms.Button browseTemplateFileButton;
        private System.Windows.Forms.Label label2;
        private CustomControl.ConfigTextBox testPlanFilePathBox;
        private System.Windows.Forms.Button browseTargetFolderButton;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.Label statusHintLabel;
        private System.Windows.Forms.Label environmentLabel;
        private System.Windows.Forms.Label operatingSystemLabel;
        private System.Windows.Forms.Label solutionsLabel;
        private CustomControl.ConfigTextBox domainTextBox;
        private CustomControl.ConfigTextBox environmentTextBox;
        private CustomControl.ConfigTextBox operatingSystemTextBox;
        private CustomControl.ConfigTextBox solutionTextBox;
        private System.Windows.Forms.Label associateIDLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox imgPreviewBox;
        private System.Windows.Forms.DataGridView previewDataGrid;
        private System.Windows.Forms.RichTextBox preRequisiteBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox QCCheck;
        private System.Windows.Forms.CheckBox cbPrerequisiteEvidencesCheckbox;
        private System.Windows.Forms.CheckBox cbSecondaryScreenCheckbox;
        private System.Windows.Forms.Label evidenceCaptureStatusLabel;
        //private CustomControl.ConfigTextBox QCPath;
        //private System.Windows.Forms.Label lblQC;
    }
}

