using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using KeyLibrary;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace EvidenceCollector
{
    public partial class MainForm : Form
    {
        string strTestPlanFileName, strTemplateFileName, strTargetFolderPath;
        BackgroundWorker bgwPrepareTargetDocumentBackgroundWorker, bgwCaptureEvidenceBackgroundWorker, bgwWriteEvidencesBackgroundWorker, bgwMetadataExtractionWorker;
        BackgroundWorker bgwDoneCaptureBackgroundWorker;
        KeyUtility kuKeyUtility,kuminimize;
        int iCurrentEvidenceOrdinal = 0;
        DataGridViewImageColumn dgvImgColPreviewColumn;
        DataGridViewCheckBoxColumn dgvCbColStatusColumn;
        DataGridViewCheckBoxCell dgvCbCell;
        DataGridViewTextBoxColumn dgvTbCommentsColumn;
        DataGridViewTextBoxCell dgvTbCell;
        bool bExitWhenDone = false;
        private FormWindowState fwsSavedState;
        public static string strpass = string.Empty;
        static string m_strQCURL = string.Empty;
        static string m_strdomain = string.Empty;
        static string m_strproject = string.Empty;


        /// <summary>
        /// Initialize variables for performing a new test on the same process execution.
        /// </summary>
        public void InitializeFormData()
        {
            refactorForm(true);
            iCurrentEvidenceOrdinal = 0;
            imgPreviewBox.Image = Properties.Resources.grey;
            doneCapturingButton.Visible = false;
            processButton.Visible = true;
            evidenceCaptureStatusLabel.Text="Ready to load";
            processButton.Text = "Load";
            preRequisiteBox.Text = "Prerequisites :\n\n(Displayed on Load)";

            browseTargetFolderButton.Visible = true;
            browseTemplateFileButton.Visible = true;
            browseTestPlanButton.Visible = true;
            btPreviewDocumentButton.Visible = false;

            domainTextBox.Enabled = true;
            environmentTextBox.Enabled = true;
            operatingSystemTextBox.Enabled = true;
            solutionTextBox.Enabled = true;
            associateIDTextBox.Enabled = true;


            testPlanFilePathBox.Enabled = true;
            templateFilePathBox.Enabled = true;
            targetFolderPathBox.Enabled = true;
            domainTextBox.Enabled = true;
            environmentTextBox.Enabled = true;
            operatingSystemTextBox.Enabled = true;
            solutionTextBox.Enabled = true;
            associateIDTextBox.Enabled = true;
            bExitWhenDone = false;
            //operatingSystemTextBox.Text = Environment.OSVersion.ToString();
            associateIDTextBox.Text = Environment.UserName;
            //solutionTextBox.Text = " ";
            m_strQCURL = "http://qualitycenter.cerner.com/qcbin/";
            m_strdomain = "IP";
            m_strproject = "TD_VALIDATION_TESTS";

            EvidenceCollector.Initialize();
        }






        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        public MainForm()
        {
            try
            {
                InitializeComponent();
                this.InitializeFormData();

                bgwPrepareTargetDocumentBackgroundWorker = new BackgroundWorker();
                bgwPrepareTargetDocumentBackgroundWorker.WorkerReportsProgress = true;
                bgwPrepareTargetDocumentBackgroundWorker.WorkerSupportsCancellation = true;
                bgwPrepareTargetDocumentBackgroundWorker.DoWork += new DoWorkEventHandler(EvidenceCollector.PrepareTargetDocumentBackgroundWork);
                bgwPrepareTargetDocumentBackgroundWorker.ProgressChanged += BgwPrepareTargetDocumentBackgroundWorker_ProgressChanged;
                bgwPrepareTargetDocumentBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(prepareTargetDocumentReturnedEvent);


                bgwCaptureEvidenceBackgroundWorker = new BackgroundWorker();
                bgwCaptureEvidenceBackgroundWorker.WorkerReportsProgress = true;
                bgwCaptureEvidenceBackgroundWorker.WorkerSupportsCancellation = true;
                bgwCaptureEvidenceBackgroundWorker.DoWork += new DoWorkEventHandler(EvidenceCollector.CaptureEvidenceBackgroundWork);
                bgwCaptureEvidenceBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(captureReturnedEvent);


                bgwWriteEvidencesBackgroundWorker = new BackgroundWorker();
                bgwWriteEvidencesBackgroundWorker.WorkerReportsProgress = true;
                bgwWriteEvidencesBackgroundWorker.WorkerSupportsCancellation = true;
                bgwWriteEvidencesBackgroundWorker.DoWork += new DoWorkEventHandler(EvidenceCollector.WriteEvidencesBackgroundWork);
                bgwWriteEvidencesBackgroundWorker.ProgressChanged += BgwWriteEvidencesBackgroundWorker_ProgressChanged;
                bgwWriteEvidencesBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(writeEvidenceCompletedEvent);


                bgwMetadataExtractionWorker = new BackgroundWorker();
                bgwMetadataExtractionWorker.WorkerReportsProgress = true;
                bgwMetadataExtractionWorker.WorkerSupportsCancellation = true;
                bgwMetadataExtractionWorker.DoWork += new DoWorkEventHandler(EvidenceCollector.readEvidenceMetadataWork);
                bgwMetadataExtractionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(metadataExtractionCompleteEvent);

                bgwDoneCaptureBackgroundWorker = new BackgroundWorker();
                bgwDoneCaptureBackgroundWorker.WorkerReportsProgress = true;
                bgwDoneCaptureBackgroundWorker.WorkerSupportsCancellation = true;
                bgwDoneCaptureBackgroundWorker.DoWork += BgwDoneCaptureBackgroundWorker_DoWork;
                bgwDoneCaptureBackgroundWorker.RunWorkerCompleted += BgwDoneCaptureBackgroundWorker_RunWorkerCompleted;

                evidenceCaptureStatusLabel.Text = "Ready to load";
                kuKeyUtility = new KeyUtility(KeyLibrary.Constants.CTRL + Constants.ALT, Keys.Z, this);
                RegisterHotKey(this.Handle, this.GetType().GetHashCode(), KeyLibrary.Constants.CTRL + Constants.ALT, (int)Keys.X);
                // kuminimize = new KeyUtility(KeyLibrary.Constants.CTRL + Constants.ALT, Keys.X, this);
            }
            catch (Exception ex)
            {
                this.bringFormToFront();                
                MessageBox.Show(new Form { TopMost = true }, "Exception Occurred , Check the Log file ", "Evidence Collector");
                this.InitializeFormData();               
                EvidenceCollector.closeTemplateDocument();
                EvidenceCollector.closeTargetDocument();
                EvidenceCollector.closeWordApp();
            
            }
        }

        private void BgwPrepareTargetDocumentBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                btPreviewDocumentButton.Visible = true;
            }
        }

        private void BgwWriteEvidencesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int iRowIndex = EvidenceCollector.GetTestPlanStepOrdinalOf(EvidenceCollector.lstPropEvidenceID[e.ProgressPercentage]);
            evidenceCaptureStatusLabel.Text = EvidenceCollector.lstStrPropEvidencesWritten.Count.ToString() + " of " + EvidenceCollector.iPropNumberOfEvidences.ToString() + " evidences collected";
            previewDataGrid[3, iRowIndex].Value = Properties.Resources.Done;
            previewDataGrid[4, iRowIndex].Value = dgvCbColStatusColumn.TrueValue;
        }

        private void BgwDoneCaptureBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EvidenceCollector.bReadyToStartCaptureProcess = false;
            EvidenceCollector.bEvidenceCapturingCompleted = true;
            while (!EvidenceCollector.bWriteEvidencesCompleted || !EvidenceCollector.bPrepareTargetDocumentCompleted)
            {
                if (EvidenceCollector.bWaitingForEvidence)
                {
                    EvidenceCollector.mreEvidenceSemaphore.Set();
                }
                if (EvidenceCollector.bWaitingForTable)
                {
                    EvidenceCollector.mreTableSemaphore.Set();
                }
            }

            EvidenceCollector.closeTemplateDocument();
            EvidenceCollector.closeTargetDocument();
            EvidenceCollector.closeWordApp();
        }


        private void BgwDoneCaptureBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (EvidenceCollector.iPropNumberOfEvidences == EvidenceCollector.lstStrPropEvidencesWritten.Count)
            {
                evidenceCaptureStatusLabel.Text = "All " + EvidenceCollector.iPropNumberOfEvidences.ToString() + " evidence(s) collected";
            }
            else
            {
                evidenceCaptureStatusLabel.Text = EvidenceCollector.lstStrPropEvidencesWritten.Count.ToString() + " evidence(s) collected and " + (EvidenceCollector.iPropNumberOfEvidences - EvidenceCollector.lstStrPropEvidencesWritten.Count).ToString() + " discarded as per user request";
            }
            if (bExitWhenDone)
            {
                this.bringFormToFront();
                MessageBox.Show(this, evidenceCaptureStatusLabel.Text + "\nPress OK to exit", "Exiting...", MessageBoxButtons.OK);
                EvidenceCollector.closeTemplateDocument();
                EvidenceCollector.closeTargetDocument();
                EvidenceCollector.closeWordApp();
                this.Close();
            }
            this.InitializeFormData();
            return;
        }







        private void prepareTargetDocumentReturnedEvent(object sender, RunWorkerCompletedEventArgs e)
        {
            EvidenceCollector.bPrepareTargetDocumentCompleted = true;
        }

        private void metadataExtractionCompleteEvent(object sender, RunWorkerCompletedEventArgs e)
        {
            this.bringFormToFront();
            EvidenceCollector.bMetadataExtractionReturned = true;
            if (e.Cancelled)
            {
                this.bringFormToFront();
                MessageBox.Show(new Form { TopMost = true }, "Metadata extraction Failed or cancelled by user", "Evidence Collector");
                this.InitializeFormData();
                return;
            }
            EvidenceCollector.bResumedSession = false;

            #region Added by Achal
            preRequisiteBox.Text = EvidenceCollector.strPropPrerequisites;
            #endregion

            EvidenceCollector.strScreenshotSavePath = System.IO.Path.GetTempPath() + "EvidenceCollector\\" + EvidenceCollector.strPropTestPlanName + "\\";
            Directory.CreateDirectory(EvidenceCollector.strScreenshotSavePath + "\\preview\\");


            #region Added by Achal
            if (File.Exists(EvidenceCollector.strPropTargetFileURI))
            {
                this.bringFormToFront();
                DialogResult drReply = MessageBox.Show(this, "An evidence collection session is already active.\n Do you wish to resume ?", "Resume previous session", MessageBoxButtons.YesNo);
                if (drReply == DialogResult.Yes)
                {
                    EvidenceCollector.bResumedSession = true;
                    EvidenceCollector.bScreenshotDictionaryReady = false;
                    try
                    {
                        SessionRestore.ResumeState(EvidenceCollector.strPropTargetFileURI);
                        btPreviewDocumentButton.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        this.bringFormToFront();
                        this.InitializeFormData();
                        MessageBox.Show(this, "Unable to resume sesion\n" + ex.Message, "Evidence Collector");
                        return;
                    }

                    for (int i = 0; i < EvidenceCollector.lstPropTestPlanSteps.Count; i++)
                    {
                        if (EvidenceCollector.lstPropEvidenceID.Contains(EvidenceCollector.lstPropTestPlanSteps[i][0]) && !EvidenceCollector.lstStrPropEvidencesWritten.Contains(EvidenceCollector.lstPropTestPlanSteps[i][0]))
                        {
                            iCurrentEvidenceOrdinal = EvidenceCollector.GetEvidenceOrdinalOf(EvidenceCollector.lstPropTestPlanSteps[i][0]);
                            break;
                        }
                    }
                }
                else
                {
                    string strEvidenceImagesFolder = System.IO.Path.GetTempPath() + "EvidenceCollector\\" + EvidenceCollector.strPropTestPlanName + "\\";
                    try
                    {
                        foreach (string strPotentialEvidenceImages in Directory.GetFiles(strEvidenceImagesFolder))
                        {
                            string strID = strPotentialEvidenceImages.Split('\\').LastOrDefault();
                            if (strID.Length >= 5)
                            {
                                strID = strID.Remove(strID.Length - 4, 4);
                            }
                            if (EvidenceCollector.lstPropEvidenceID.Contains(strID))
                            {
                                File.Delete(strPotentialEvidenceImages);
                            }
                        }
                    }
                    catch
                    {
                        EvidenceCollector.Log("Unable to clean abandoned session from temp folder [" + strEvidenceImagesFolder + "]");
                    }

                }
            }
            #endregion


            initializePreviewDataGrid();
            EvidenceCollector.bPreProcessingComplete = true;


            processButton.Text = "Process";
            processButton.Visible = true;
            evidenceCaptureStatusLabel.Text = "Ready to process " + EvidenceCollector.iPropNumberOfEvidences.ToString() + " evidence(s) ";
            if (EvidenceCollector.bResumedSession)
            {
                evidenceCaptureStatusLabel.Text += "( " + EvidenceCollector.lstStrPropEvidencesWritten.Count.ToString() + " of which already collected in a previous session) ";
            }
            process();
        }


        /// <summary>
        /// Initialize the data grid view
        /// </summary>
        private void initializePreviewDataGrid()
        {
            string strEvidenceID;


            previewDataGrid.ColumnCount = 3; // add others to make 6;
            dgvImgColPreviewColumn = new DataGridViewImageColumn();
            dgvImgColPreviewColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvImgColPreviewColumn.Width = 40;
            dgvCbColStatusColumn = new DataGridViewCheckBoxColumn();
            dgvCbColStatusColumn.HeaderText = "Status";
            dgvCbColStatusColumn.ReadOnly = false; // true; // blah
            dgvCbColStatusColumn.Width = 40;
            dgvCbColStatusColumn.TrueValue = true;
            dgvCbColStatusColumn.FalseValue = false;
            dgvCbCell = new DataGridViewCheckBoxCell();
            dgvTbCommentsColumn = new DataGridViewTextBoxColumn();
            // dgvTbCell = new DataGridViewTextBoxCell();
            dgvTbCommentsColumn.ReadOnly = false;


            previewDataGrid.ReadOnly = false;

            previewDataGrid.Columns.Add(dgvImgColPreviewColumn);
            previewDataGrid.Columns.Add(dgvCbColStatusColumn);
            previewDataGrid.Columns.Add(dgvTbCommentsColumn);
            // previewDataGrid.Columns.Add(new DataGridViewColumn());


            previewDataGrid.Columns[5].ReadOnly = false;


            for (int i = 0; i < previewDataGrid.Columns.Count; i++)
            {
                previewDataGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                previewDataGrid.Columns[i].Resizable = DataGridViewTriState.True;
                if (i != 5)
                {
                    previewDataGrid.Columns[i].ReadOnly = true;
                }
            }
            previewDataGrid.Columns[5].DefaultCellStyle.BackColor = Color.White;
            previewDataGrid.Columns[5].DefaultCellStyle.ForeColor = Color.Black;
            previewDataGrid.Columns[5].DefaultCellStyle.SelectionBackColor = Color.Blue;
            previewDataGrid.Columns[5].DefaultCellStyle.SelectionForeColor = Color.White;


            previewDataGrid.Columns[0].HeaderText = "Step No.";
            previewDataGrid.Columns[1].HeaderText = "Description";
            previewDataGrid.Columns[2].HeaderText = "Expected Results";
            previewDataGrid.Columns[3].HeaderText = "Saved";
            previewDataGrid.Columns[4].HeaderText = "Status";
            previewDataGrid.Columns[5].HeaderText = "User Comments";



            previewDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            previewDataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            previewDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            previewDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            previewDataGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            previewDataGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            previewDataGrid.Columns[1].FillWeight = 35;
            previewDataGrid.Columns[2].FillWeight = 35;
            previewDataGrid.Columns[5].FillWeight = 30;




            int iCurrentTestPlanStepOrdinal = EvidenceCollector.GetTestPlanStepOrdinalOf(EvidenceCollector.lstPropEvidenceID[iCurrentEvidenceOrdinal]);
            previewDataGrid.Rows.Clear();
            previewDataGrid.RowCount = EvidenceCollector.lstPropTestPlanSteps.Count;

            for (int i = 0; i < EvidenceCollector.lstPropTestPlanSteps.Count; i++)
            {
                previewDataGrid.Rows[i].Resizable = DataGridViewTriState.False;
                previewDataGrid[0, i].Value = EvidenceCollector.lstPropTestPlanSteps[i][0];
                previewDataGrid[1, i].Value = EvidenceCollector.lstPropTestPlanSteps[i][1];
                previewDataGrid[2, i].Value = EvidenceCollector.lstPropTestPlanSteps[i][2];

                previewDataGrid[5, i].ReadOnly = true;

                strEvidenceID = previewDataGrid[0, i].Value.ToString();

                if (EvidenceCollector.lstPropEvidenceID.Contains(EvidenceCollector.lstPropTestPlanSteps[i][0]))
                {
                    // Step requires evidence
                    if (EvidenceCollector.lstStrPropEvidencesWritten.Contains(EvidenceCollector.lstPropTestPlanSteps[i][0]))
                    {
                        // Evidence captured
                        previewDataGrid[3, i].Value = Properties.Resources.Done;
                        previewDataGrid[4, i].Value = dgvCbColStatusColumn.TrueValue; ///   blah required ?
                        if ("Passed".Equals(EvidenceCollector.GetEvidenceStatus(strEvidenceID)))
                        {
                            previewDataGrid[4, i].Value = dgvCbColStatusColumn.TrueValue;
                        }
                        else if ("Failed".Equals(EvidenceCollector.GetEvidenceStatus(strEvidenceID)))
                        {
                            previewDataGrid[4, i].Value = dgvCbColStatusColumn.FalseValue;
                            previewDataGrid[5, i].ReadOnly = false;
                            previewDataGrid[5, i].Value = EvidenceCollector.GetEvidenceComments(strEvidenceID);
                        }
                        else
                        {
                            previewDataGrid[4, i].Value = dgvCbColStatusColumn.TrueValue; /// blah [ or false value ? ]
                        }
                    }
                    else
                    {
                        // Evidence capture pending
                        previewDataGrid[4, i].Value = dgvCbColStatusColumn.TrueValue; /// blah [ or false value ? ]
                        previewDataGrid[5, i].ReadOnly = true;
                    }
                }
                else
                {
                    // Step does not require evidence
                    previewDataGrid.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    previewDataGrid.Rows[i].DefaultCellStyle.SelectionBackColor = Color.LightGray;
                    previewDataGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                    previewDataGrid.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Gray;
                    previewDataGrid[3, i].Value = Properties.Resources.grey;
                    previewDataGrid[4, i].Value = dgvCbColStatusColumn.TrueValue; /// blah [ or false value ? ]
                }
            }

            previewDataGrid.Rows[iCurrentTestPlanStepOrdinal].DefaultCellStyle.BackColor = Color.LimeGreen;
            previewDataGrid.Rows[iCurrentTestPlanStepOrdinal].DefaultCellStyle.SelectionBackColor = Color.DarkOliveGreen;
            previewDataGrid.Rows[iCurrentTestPlanStepOrdinal].DefaultCellStyle.ForeColor = Color.RoyalBlue;
            previewDataGrid.Rows[iCurrentTestPlanStepOrdinal].DefaultCellStyle.SelectionForeColor = Color.White;
            if (EvidenceCollector.bResumedSession)
            {
                previewDataGrid.CurrentCell = previewDataGrid.Rows[iCurrentTestPlanStepOrdinal].Cells[2];
            }
        }






        private void writeEvidenceCompletedEvent(object sender, RunWorkerCompletedEventArgs e)
        {
            EvidenceCollector.bWriteEvidencesCompleted = true;
            return;
        }

        private void captureReturnedEvent(object sender, RunWorkerCompletedEventArgs e)
        {
            kuKeyUtility.Register();
            if (EvidenceCollector.bPrerequisiteEvidence)
            {
                if (EvidenceCollector.bShowPrerequisiteEvidenceInPreview)
                {
                    imgPreviewBox.Image = EvidenceCollector.bmpEvidenceImageBitmap;
                }
                return;
            }
            if (EvidenceCollector.bWaitingForEvidence)
            {
                EvidenceCollector.mreEvidenceSemaphore.Set();
            }
            if (!e.Cancelled)
            {
                if (iCurrentEvidenceOrdinal + 1 < EvidenceCollector.iPropNumberOfEvidences)
                {
                    iCurrentEvidenceOrdinal++;
                    shiftCurrentStepIndicator(iCurrentEvidenceOrdinal - 1, iCurrentEvidenceOrdinal);
                    imgPreviewBox.Image = EvidenceCollector.bmpCurrentImageBitmap;
                }
                else
                {
                    NotifyLastEvidenceCapture();
                }
            }
        }

        private void NotifyLastEvidenceCapture()
        {
            this.bringFormToFront();
            MessageBox.Show(this, "Last evidence in the test plan captured. Further capture attempts will overwrite the last captured evidence unless manually navigated to a different test plan step", "Evidence Collector");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            testPlanFilePathBox.LoadValues();
            templateFilePathBox.LoadValues();
            targetFolderPathBox.LoadValues();
            domainTextBox.LoadValues();
            environmentTextBox.LoadValues();
            operatingSystemTextBox.LoadValues();
            solutionTextBox.LoadValues();
            associateIDTextBox.LoadValues();
            //QCPath.LoadValues();
            kuKeyUtility.Register();
            setTooltips();
            setLastSelectedPath();
            if (string.IsNullOrEmpty(templateFilePathBox.Text))
            {
                templateFilePathBox.Text = @"C:\Program Files (x86)\Cerner Corporation\Evidence Collector\TemplateFolder\1296995853 Manual Test Evidence Template v1.docx";
            }
            //operatingSystemTextBox.Text = Environment.OSVersion.ToString();
            associateIDTextBox.Text = Environment.UserName;
           // solutionTextBox.Text = "Pharmacy Inpatient";
           
            //QCPath.Text = @"Subject\PharmNet -- Inpatient\Functional\_temp\_MM017949-Temp";
            // PreviewForm fmPreview = new PreviewForm();
            // fmPreview.Show();
        }

        private void CheckForWordFiles()
        {
            Process []plist = Process.GetProcessesByName("Winword");
            bool bemptywordflg=false;

            foreach (Process p in plist)
            {
                if (p.MainWindowTitle == string.Empty)
                {
                    bemptywordflg = true;
                    break;

                }
            }
                if (bemptywordflg) 
                {
                    if (MessageBox.Show("Un-named Word process found , this could lead to the software to not perform accurately , Click Yes if you would like to kill the empty processes of word", "Word Process Check", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (Process pi in plist)
                        {
                            if (pi.MainWindowTitle == string.Empty)
                            {
                                pi.Kill();
                            
                            }
                        }
                    
                    }
                }
            
            
        }


        #region Added by Achal

        private void setTooltips()
        {

            toolTip1.ShowAlways = true;
            toolTip1.InitialDelay = 225;
            toolTip1.SetToolTip(testPlanFilePathBox, testPlanFilePathBox.Text);
            toolTip1.SetToolTip(templateFilePathBox, templateFilePathBox.Text);
            toolTip1.SetToolTip(targetFolderPathBox, targetFolderPathBox.Text);
        }

        private void setLastSelectedPath()
        {
            string strTestPlanDirectory = testPlanFilePathBox.Text;
            string strTemplateFileDirectory = templateFilePathBox.Text;
            if (!String.IsNullOrEmpty(strTestPlanDirectory))
            {
                if (File.Exists(testPlanFilePathBox.Text))
                {
                    if (testPlanFilePathBox.Text.Contains("\\"))
                    {
                        browseTestPlanDialog.InitialDirectory = testPlanFilePathBox.Text.Remove(testPlanFilePathBox.Text.LastIndexOf("\\"));
                    }
                }
            }

            if (!String.IsNullOrEmpty(strTemplateFileDirectory))
            {
                if (File.Exists(templateFilePathBox.Text))
                {
                    if (templateFilePathBox.Text.Contains("\\"))
                    {
                        browseTemplateFileDialog.InitialDirectory = templateFilePathBox.Text.Remove(templateFilePathBox.Text.LastIndexOf("\\"));
                    }
                }
            }

        }
        #endregion



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EvidenceCollector.Log("Mainform close request time: " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second + ":" + DateTime.UtcNow.Millisecond);
            bExitWhenDone = true;
            kuKeyUtility.Unregiser();
            testPlanFilePathBox.Enabled = false;
            templateFilePathBox.Enabled = false;
            targetFolderPathBox.Enabled = false;
            domainTextBox.Enabled = false;
            environmentTextBox.Enabled = false;
            operatingSystemTextBox.Enabled = false;
            solutionTextBox.Enabled = false;
            associateIDTextBox.Enabled = false;

            browseTestPlanButton.Visible = false;
            browseTemplateFileButton.Visible = false;
            browseTargetFolderButton.Visible = false;
            processButton.Visible = false;
            doneCapturingButton.Visible = false;

            Text = "Preparing to exit...";



            if (EvidenceCollector.quePropStrCapturedEvidences.Count > 0)
            {
                this.bringFormToFront();
                DialogResult drReply = MessageBox.Show(this, EvidenceCollector.quePropStrCapturedEvidences.Count.ToString() + " unsaved evidences detected.\n Do you want to save changes before exiting ?\n (this may take a while)", "Save changes", MessageBoxButtons.YesNo);
                if (drReply == DialogResult.No)
                {
                    if (bgwPrepareTargetDocumentBackgroundWorker.IsBusy)
                    {
                        bgwPrepareTargetDocumentBackgroundWorker.CancelAsync();
                    }
                    if (bgwWriteEvidencesBackgroundWorker.IsBusy)
                    {
                        bgwWriteEvidencesBackgroundWorker.CancelAsync();
                    }
                }
                else
                {
                    Text = "Saving evidences and preparing to exit...";
                    e.Cancel = true;
                }
                if (!bgwDoneCaptureBackgroundWorker.IsBusy)
                {
                    bgwDoneCaptureBackgroundWorker.RunWorkerAsync();
                }
               
            }
            while (bgwMetadataExtractionWorker.IsBusy)
            {
                bgwMetadataExtractionWorker.CancelAsync();
                if (EvidenceCollector.bMetadataExtractionReturned)
                {
                    break;
                }
            }
            EvidenceCollector.closeTemplateDocument();
            EvidenceCollector.closeTargetDocument();
            EvidenceCollector.closeWordApp();
            if (!EvidenceCollector.bWriteEvidencesCompleted || !EvidenceCollector.bPrepareTargetDocumentCompleted)
            {
                
            
            }
        }

        private void bringFormToFront()
        {
            this.WindowState = this.fwsSavedState;
            this.Activate();
        }



        /// <summary>
        /// Global keyboard shortcut
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == KeyLibrary.Constants.WM_HOTKEY_MSG_ID)
            {  
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                if (key == Keys.X)
                {
                    if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                    else 
                    {
                        this.WindowState = FormWindowState.Normal;
                    
                    }
                }
                else if (key == Keys.Z)
                {
                    captureScreenshotHelper();
                }
            }
            if (m.Msg == 0x0112) // sys command
            {
                if ((m.WParam.ToInt32() & 0xfff0) == 0xF020) // minimize
                {
                    this.fwsSavedState = this.WindowState;
                }
                if ((m.WParam.ToInt32() & 0xfff0) == 0xF030) // maximize
                {
                    this.fwsSavedState = FormWindowState.Maximized;
                }
                if ((m.WParam.ToInt32() & 0xfff0) == 0xF120) // restore
                {
                    this.fwsSavedState = FormWindowState.Normal;
                }
            }
            base.WndProc(ref m);
        }


        /// <summary>
        /// Informs evidence capturing thread that user is ready to take a screenshot.
        /// </summary>
        private void captureScreenshotHelper()
        {
            TimeSpan tsEpoch;
            long liEpoch;
            string strID;
            if (EvidenceCollector.bEvidenceCapturingCompleted)
            {
                return;
            }
            EvidenceCollector.bUseSecondaryScreen = cbSecondaryScreenCheckbox.Checked;
            EvidenceCollector.bPrerequisiteEvidence = cbPrerequisiteEvidencesCheckbox.Checked;
            if (!EvidenceCollector.bReadyToStartCaptureProcess)
            {
                this.bringFormToFront();
                MessageBox.Show(this, "Not ready. Try after pre processing is completed", "Evidence Collector");
                return;
            }

            if (bgwCaptureEvidenceBackgroundWorker.IsBusy)
            {
                MessageBox.Show(new Form { TopMost = true }, "A capture operation is pending. Please try after it is completed or cancelled", "Evidence Collector");
                return;
            }

            kuKeyUtility.Unregiser();
            if (cbPrerequisiteEvidencesCheckbox.Checked)
            {
                tsEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
                liEpoch = (long)tsEpoch.TotalSeconds;
                strID = EvidenceCollector.PREREQUISITE_ALT_TEXT + "_epoch_" + liEpoch.ToString();
                EvidenceCollector.Log("Requesting to save prerequisite evidence " + strID);
                bgwCaptureEvidenceBackgroundWorker.RunWorkerAsync(strID); // unique name
            }
            else
            {
                strID = EvidenceCollector.lstPropEvidenceID[iCurrentEvidenceOrdinal];
                EvidenceCollector.Log("Requesting to save evidence " + strID);
                bgwCaptureEvidenceBackgroundWorker.RunWorkerAsync(strID);
            }
        }


        /// <summary>
        /// Shift the custom highlight on the data grid view to indicate the current step for which evidence is about to be captured.
        /// </summary>
        /// <param name="iOldEvidenceOrdinal"></param>
        /// <param name="iNewEvidenceOrdinal"></param>
        private void shiftCurrentStepIndicator(int iOldEvidenceOrdinal, int iNewEvidenceOrdinal)
        {
            Color clrTempColor;
            string strOldEvidenceID, strNewEvidenceID;
            int iOldTestPlanOrdinal, iNewTestPlanOrdinal;

            strOldEvidenceID = EvidenceCollector.lstPropEvidenceID[iOldEvidenceOrdinal];
            strNewEvidenceID = EvidenceCollector.lstPropEvidenceID[iNewEvidenceOrdinal];

            iOldTestPlanOrdinal = EvidenceCollector.GetTestPlanStepOrdinalOf(strOldEvidenceID);
            iNewTestPlanOrdinal = EvidenceCollector.GetTestPlanStepOrdinalOf(strNewEvidenceID);

            clrTempColor = previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.BackColor;
            previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.BackColor = Color.LimeGreen;
            previewDataGrid.Rows[iOldTestPlanOrdinal].DefaultCellStyle.BackColor = clrTempColor;

            clrTempColor = previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.SelectionBackColor;
            previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.SelectionBackColor = Color.DarkOliveGreen;
            previewDataGrid.Rows[iOldTestPlanOrdinal].DefaultCellStyle.SelectionBackColor = clrTempColor;

            clrTempColor = previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.ForeColor;
            previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.ForeColor = Color.RoyalBlue;
            previewDataGrid.Rows[iOldTestPlanOrdinal].DefaultCellStyle.ForeColor = clrTempColor;

            clrTempColor = previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.SelectionForeColor;
            previewDataGrid.Rows[iNewTestPlanOrdinal].DefaultCellStyle.SelectionForeColor = Color.White;
            previewDataGrid.Rows[iOldTestPlanOrdinal].DefaultCellStyle.SelectionForeColor = clrTempColor;

            previewDataGrid.CurrentCell = previewDataGrid.Rows[iNewTestPlanOrdinal].Cells[2];
            iCurrentEvidenceOrdinal = iNewEvidenceOrdinal;
        }




        private void previewDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Make sure to check if the click is on header row in case some logic is being added here.
        }

        private void previewDataGrid_CellDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == 5 || e.ColumnIndex == 4)
            {
                // Header row || comments || checkbox cell
                return;
            }
            string strnewEvidenceID = (string)previewDataGrid[0, e.RowIndex].Value;
            int iNewEvidenceOrdinal;

            int iCurrentTestPlanStepOrdinal = EvidenceCollector.GetTestPlanStepOrdinalOf(EvidenceCollector.lstPropEvidenceID[iCurrentEvidenceOrdinal]);

            if (EvidenceCollector.lstPropEvidenceID.Contains(strnewEvidenceID))
            {
                iNewEvidenceOrdinal = EvidenceCollector.GetEvidenceOrdinalOf(strnewEvidenceID);
                this.shiftCurrentStepIndicator(iCurrentEvidenceOrdinal, iNewEvidenceOrdinal);
            }
        }




        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void captureScreenshotEvent(object sender, EventArgs e)
        {
            captureScreenshotHelper();
        }




        /// <summary>
        /// Browse test plan document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseTestPlan(object sender, EventArgs e)
        {
            DialogResult drBrowseTestPlanDialogResult = browseTestPlanDialog.ShowDialog();
            if (DialogResult.OK == drBrowseTestPlanDialogResult)
            {
                //Added by Achal
                browseTestPlanDialog.InitialDirectory = browseTestPlanDialog.FileName.Remove(browseTestPlanDialog.FileName.LastIndexOf("\\"));
                strTestPlanFileName = browseTestPlanDialog.FileName;
                testPlanFilePathBox.Text = strTestPlanFileName;
                evidenceCaptureStatusLabel.Text = "Ready to load";
                this.InitializeFormData();
            }
        }

        private void doneCapturing_Click(object sender, EventArgs e)
        {
            previewDataGrid.Rows.Clear();
            imgPreviewBox.Image = Properties.Resources.grey;
            doneCapturingButton.Visible = false;
            evidenceCaptureStatusLabel.Text = "Saving evidences...";
            bgwDoneCaptureBackgroundWorker.RunWorkerAsync();
        }


        /// <summary>
        /// Browse for the folder where the target document containing the evidences is to be saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseTargetFolder(object sender, EventArgs e)
        {
            DialogResult drBrowseTargetFolderDialogResult = browseTargetFolderDialog.ShowDialog();
            if (DialogResult.OK == drBrowseTargetFolderDialogResult)
            {
                strTargetFolderPath = browseTargetFolderDialog.SelectedPath;
                targetFolderPathBox.Text = strTargetFolderPath;
                evidenceCaptureStatusLabel.Text = "Ready to load";
                this.InitializeFormData();
            }
        }


        /// <summary>
        /// Browse for the document which specifies the template in which the evidences are to be stored
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseTemplateFile(object sender, EventArgs e)
        {
            DialogResult drBrowseTemplateFileDialogResult = browseTemplateFileDialog.ShowDialog();
            if (DialogResult.OK == drBrowseTemplateFileDialogResult)
            {
                strTemplateFileName = browseTemplateFileDialog.FileName;
                templateFilePathBox.Text = strTemplateFileName;
                evidenceCaptureStatusLabel.Text = "Ready to load";
                this.InitializeFormData();
            }

        }

        private void btPreviewDocumentButton_Click(object sender, EventArgs e)
        {
            EvidenceCollector.PreviewTargetDocument();
        }

        private void previewDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string strEvidenceID;
            if (e.RowIndex == -1)
            {
                return;
            }
            strEvidenceID = previewDataGrid[0, e.RowIndex].Value.ToString();
            if (e.ColumnIndex == 4 && EvidenceCollector.lstStrPropEvidencesWritten.Contains(strEvidenceID))
            {
                if (previewDataGrid[4, e.RowIndex].Value == dgvCbColStatusColumn.TrueValue)
                {
                    previewDataGrid[4, e.RowIndex].Value = dgvCbColStatusColumn.FalseValue;
                    previewDataGrid[5, e.RowIndex].ReadOnly = false;
                    previewDataGrid[5, e.RowIndex].Value = "<Enter comments here>";
                    EvidenceCollector.SetEvidenceComments(strEvidenceID, previewDataGrid[5, e.RowIndex].Value.ToString());
                    EvidenceCollector.SetEvidenceStatus(strEvidenceID, false);

                }
                else
                {
                    previewDataGrid[4, e.RowIndex].Value = dgvCbColStatusColumn.TrueValue;
                    previewDataGrid[5, e.RowIndex].Value = "";
                    EvidenceCollector.SetEvidenceComments(strEvidenceID, "");
                    previewDataGrid[5, e.RowIndex].ReadOnly = true;
                    EvidenceCollector.SetEvidenceStatus(strEvidenceID, true);
                }
            }
        }


        private void previewDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string strEvidenceID;
            string strComments;
            if (e.RowIndex == -1)
            {
                return;
            }
            strEvidenceID = previewDataGrid[0, e.RowIndex].Value.ToString();
            strComments = previewDataGrid[5, e.RowIndex].Value.ToString();
            EvidenceCollector.SetEvidenceComments(strEvidenceID, strComments);
        }

        private void previewDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (!EvidenceCollector.lstPropEvidenceID.Contains(previewDataGrid[0, e.RowIndex].Value))
            {
                // No evidence required.
                imgPreviewBox.Image = Properties.Resources.grey;
            }
            else if (EvidenceCollector.lstStrPropEvidencesWritten.Contains(previewDataGrid[0, e.RowIndex].Value))
            {
                string strPreviewImageURI = EvidenceCollector.strScreenshotSavePath + "preview\\" + previewDataGrid[0, e.RowIndex].Value + ".png";
                if (System.IO.File.Exists(strPreviewImageURI))
                {
                    MemoryStream ms_ = new MemoryStream();
                    byte[] by_ = System.IO.File.ReadAllBytes(strPreviewImageURI);
                    ms_.Write(by_, 0, by_.Length);
                    imgPreviewBox.Image = Image.FromStream(ms_);
                }
                else
                {
                    // Evidence image got deleted from file system due to unknown reason.
                }
            }
            else
            {
                // Evidence image not captured yet.
                imgPreviewBox.Image = Properties.Resources.pending;
            }
        }

        private void processButtonEvent(object sender, EventArgs e)
        {
            //CheckForWordFiles();
            bool bInsufficientInput = false;
            string strMsg = "";
            if (!QCCheck.Checked)
            {
                if (!File.Exists(testPlanFilePathBox.Text.Trim()))
                {
                    strMsg += "Invalid Test plan document.Enable the Pull from QC to fetch from QC\n";
                    bInsufficientInput = true;
                }
            }
            if (!File.Exists(templateFilePathBox.Text.Trim()))
            {
                strMsg += "Invalid Template document.\n";
                bInsufficientInput = true;
            }
            if (!Directory.Exists(targetFolderPathBox.Text.Trim() + "\\" + domainTextBox.Text.Trim()))
            {
                Directory.CreateDirectory(targetFolderPathBox.Text.Trim() + "\\" + domainTextBox.Text.Trim());
            }
            if (string.IsNullOrEmpty(testPlanFilePathBox.Text.Trim()))            
            {
               bInsufficientInput = true;
            }

            if (bInsufficientInput)
            {
                MessageBox.Show(this, strMsg, "Insufficient input");
                return;
            }

            EvidenceCollector.Log("Load/process button click time: " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second + ":" + DateTime.UtcNow.Millisecond);
            if (!EvidenceCollector.bPreProcessingComplete)
            {
                EvidenceCollector.bMetadataExtractionStarted = true;
                refactorForm(false);
                preprocess();
            }
            else
            {
                process();
            }
        }


        /// <summary>
        /// Hide unwanted text boxes
        /// </summary>
        /// <param name="bInitalState"></param>
        private void refactorForm(bool bInitalState)
        {
            return; /// blah change this
            domainLabel.Visible = bInitalState;
            domainTextBox.Visible = bInitalState;
            environmentLabel.Visible = bInitalState;
            environmentTextBox.Visible = bInitalState;
            operatingSystemLabel.Visible = bInitalState;
            operatingSystemTextBox.Visible = bInitalState;
            solutionsLabel.Visible = bInitalState;
            solutionTextBox.Visible = bInitalState;
            associateIDLabel.Visible = bInitalState;
            associateIDTextBox.Visible = bInitalState;
        }



        /// <summary>
        /// Begin the evidence collection routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preprocess()
        {
            List<object> lstObjArguments;
            processButton.Visible = false;
            browseTestPlanButton.Visible = false;
            browseTemplateFileButton.Visible = false;
            browseTargetFolderButton.Visible = false;

            domainTextBox.Enabled = false;
            environmentTextBox.Enabled = false;
            operatingSystemTextBox.Enabled = false;
            solutionTextBox.Enabled = false;
            associateIDTextBox.Enabled = false;


            strTemplateFileName = templateFilePathBox.Text.Trim(); 
            strTargetFolderPath = targetFolderPathBox.Text.Trim('\\').Trim()+ "\\" + domainTextBox.Text.Trim();
            strTestPlanFileName = testPlanFilePathBox.Text.Trim();



            /*
            strOperatingSystem = Environment.OSVersion.ToString();
            strAssociateID = Environment.UserName;
            */

            EvidenceCollector.strDomain = domainTextBox.Text.Trim();
            EvidenceCollector.strAssociateID = associateIDTextBox.Text.Trim();
            EvidenceCollector.strEnvironment = environmentTextBox.Text.Trim();
            EvidenceCollector.strOperatingSystem = operatingSystemTextBox.Text.Trim();
            EvidenceCollector.strSolutions = solutionTextBox.Text.Trim();
            EvidenceCollector.strTestData = "";

            evidenceCaptureStatusLabel.Text = "Pre-processing...";
            lstObjArguments = new List<object>();
            lstObjArguments.Add(strTestPlanFileName);
            lstObjArguments.Add(strTemplateFileName);
            lstObjArguments.Add(strTargetFolderPath);
            lstObjArguments.Add(QCCheck.Checked);
            lstObjArguments.Add(m_strQCURL);
            lstObjArguments.Add(m_strproject);
            lstObjArguments.Add(m_strdomain);
            lstObjArguments.Add(strpass);
            bgwMetadataExtractionWorker.RunWorkerAsync(lstObjArguments);
        }


        private void process()
        {
            EvidenceCollector.bReadyToStartCaptureProcess = true;
            processButton.Visible = false;
            processButton.Text = "Load";
            if (EvidenceCollector.bResumedSession)
            {
                evidenceCaptureStatusLabel.Text = EvidenceCollector.lstStrPropEvidencesWritten.Count.ToString() + " of " + EvidenceCollector.iPropNumberOfEvidences.ToString() + " evidences collected";
            }
            else
            {
                evidenceCaptureStatusLabel.Text = "Ready to capture 1 of " + EvidenceCollector.iPropNumberOfEvidences.ToString() + " evidence(s) ";
            }
            doneCapturingButton.Visible = true;
            try
            {
                bgwPrepareTargetDocumentBackgroundWorker.RunWorkerAsync();
                bgwWriteEvidencesBackgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show(new Form { TopMost = true }, "Evidence collection Failed. Check " + strCrashLog, "Evidence Collector");
                try
                {
                    System.IO.File.WriteAllText(strCrashLog, ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.ToString());
                }
                catch
                {
                }
            }

        }


        private void captureScreenshotButton_Click(object sender, EventArgs e)
        {
            captureScreenshotHelper();
        }

        private void QCCheck_CheckedChanged(object sender, EventArgs e)
        {

            if (QCCheck.Checked)
            {
                Password pass = new Password();
                pass.ShowDialog();

                //   QCPath.Visible = true;
                //  lblQC.Visible = true;

            }
            else
            {
                strpass = string.Empty;
                //QCPath.Visible = false;
                //lblQC.Visible = false;
            }
        }


        public static string StrQCURL
        {
            get
            {
                return m_strQCURL;
            }
            set
            {
                m_strQCURL = value;
            }
        }

        public static string StrQCProject
        {
            get
            {
                return m_strproject;
            }
            set
            {
                m_strproject = value;
            }
        }

        public static string StrDomain
        {
            get
            {
                return m_strdomain;
            }
            set
            {
                m_strdomain = value;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = tableLayoutPanel1.Width - preRequisiteBox.Width;        
        }
       
       
       
    }
}
