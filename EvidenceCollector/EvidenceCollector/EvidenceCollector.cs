using Microsoft.Office.Interop.Word;
using ScreenCaptureLibrary;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;


namespace EvidenceCollector
{
    class EvidenceCollector
    {
        static Evidence evEvidence;
        static TestPlan tpPlan;
        static Microsoft.Office.Interop.Word.Application targetWriterWordApp = null;
        public static Microsoft.Office.Interop.Word.Application appPreviewWordApp = null;
        static Document docTemplateDocument = null, docTargetDocument = null;
        public static Document docPreviewDocument = null;
        static string strTempPath = System.IO.Path.GetTempPath();
        static Dictionary<string, InlineShape> dictShpScreenshots = new Dictionary<string, InlineShape>();
        static Dictionary<string, int[]> dictShpDummyScreenshots = new Dictionary<string, int[]>();
        static string strTargetFileURI, strTemplateFileName, strTargetFolderPath;
        static Queue<string> queStrCapturedEvidences = new Queue<string>();
        static List<string> lstStrTablesWritten = new List<string>();
        static List<string> lstStrEvidencesWritten = new List<string>();
        public static ManualResetEvent mreEvidenceSemaphore = new ManualResetEvent(false);
        public static ManualResetEvent mreTableSemaphore = new ManualResetEvent(false);
        public static ManualResetEvent mreMetadataExtractionSemaphore = new ManualResetEvent(false);
        public static string strScreenshotSavePath, strDomain, strSolutions, strTestData, strEnvironment, strOperatingSystem, strAssociateID;
        public static bool bWaitingForEvidence = false, bWaitingForTable = false, bWaitingForMetadata = false;

        public static string strLogFile = System.IO.Path.GetTempPath() + "run.log";
        public static bool bPreProcessingComplete = false, bEvidenceCapturingCompleted = false, bReadyToStartCaptureProcess = false, bWriteEvidencesCompleted = false, bPrepareTargetDocumentCompleted = false;
        public static bool bScreenshotDictionaryReady = true;
        public static bool bResumedSession;

        public static bool bTargetDocumentOpen, bTemplateDocumentOpen, bWordAppOpen;
        public static bool bMetadataExtractionReturned = false, bMetadataExtractionStarted = false;
        public static Bitmap bmpCurrentImageBitmap = new Bitmap(1, 1);
        public static Bitmap bmpEvidenceImageBitmap = new Bitmap(1, 1);
        public static bool bUseSecondaryScreen = false;
        internal static bool bPrerequisiteEvidence;
        private static bool bReadyToInsertPrerequisiteEvidences = false;
        public static readonly bool bShowPrerequisiteEvidenceInPreview = true;

        public static List<string> lstStrPropTablesWritten
        {
            get
            {
                return lstStrTablesWritten;
            }
        }

        public static Queue<string> quePropStrCapturedEvidences
        {
            get
            {
                return queStrCapturedEvidences;
            }
        }

        public static string strPropTargetFileURI
        {
            get
            {
                return strTargetFileURI;
            }
        }

        public static List<string[]> lstPropTestPlanSteps
        {
            get
            {
                return tpPlan.lstPropTestPlanSteps;
            }
        }

        public static List<string> lstPropEvidenceID
        {
            get
            {
                return tpPlan.lstStrPropEvidenceID;
            }
        }




        #region Added By Achal
        public static string strPropPrerequisites
        {
            get
            {
                return tpPlan.strPropPrerequisites;
            }
        }
        #endregion



        public static string GetEvidenceStatus(string strEvidenceID)
        {
            return evEvidence.dictPropActualEvidences[strEvidenceID].strStatus;
        }

        public static void SetEvidenceStatus(string strEvidenceID, bool bPassed)
        {
            string strStatus;
            int iTableIndex;
            if (bPassed)
            {
                strStatus = "Passed";
            }
            else
            {
                strStatus = "Failed";
            }
            evEvidence.dictPropActualEvidences[strEvidenceID].strStatus = strStatus;
            iTableIndex = GetEvidenceOrdinalOf(strEvidenceID) + 2;
            docTargetDocument.Tables[iTableIndex].Rows[1].Cells[4].Range.Text = strStatus;
            saveTargetDocument();
        }

        public static void SetEvidenceComments(string strEvidenceID, string strComments)
        {
            int iTableIndex;
            evEvidence.dictPropActualEvidences[strEvidenceID].strComments = strComments;
            iTableIndex = GetEvidenceOrdinalOf(strEvidenceID) + 2;
            docTargetDocument.Tables[iTableIndex].Rows[2].Cells[2].Range.Text = strComments;
            saveTargetDocument();
        }


        public static string GetEvidenceComments(string strEvidenceID)
        {
            return evEvidence.dictPropActualEvidences[strEvidenceID].strComments;
        }



        internal static int GetEvidenceOrdinalOf(string strEvidenceID)
        {
            return lstPropEvidenceID.IndexOf(strEvidenceID);
        }


        internal static int GetTestPlanStepOrdinalOf(string strEvidenceID)
        {
            int i = 0;
            while (i < lstPropTestPlanSteps.Count)
            {
                if (lstPropTestPlanSteps[i][0] == strEvidenceID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }



        /// <summary>
        /// Restore a session
        /// </summary>
        /// <param name="dictShpDummyScreenshots"></param>
        /// <param name="queStrCapturedEvidences"></param>
        /// <param name="lstStrTablesWritten"></param>
        /// <param name="lstStrEvidencesWritten"></param>
        internal static void LoadSession(Dictionary<string, int[]> dictShpDummyScreenshots, Queue<string> queStrCapturedEvidences, List<string> lstStrTablesWritten, List<string> lstStrEvidencesWritten)
        {
            EvidenceCollector.dictShpDummyScreenshots = dictShpDummyScreenshots;
            EvidenceCollector.queStrCapturedEvidences = queStrCapturedEvidences;
            EvidenceCollector.lstStrTablesWritten = lstStrTablesWritten;
            EvidenceCollector.lstStrEvidencesWritten = lstStrEvidencesWritten;
        }

        


        public static void Log(string strData)
        {
            try
            {
                File.AppendAllText(strLogFile, strData + Environment.NewLine);
            }
            catch { }
        }
        public static void Log(string strData, string strTargetLogFile)
        {
            try
            {
                File.AppendAllText(strTargetLogFile, strData + Environment.NewLine);
            }
            catch { }
        }




        /// <summary>
        /// Write testplan independent data and evidence tables into the target document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void PrepareTargetDocumentBackgroundWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bgwPrepareTargetDocumentSender = sender as BackgroundWorker;
                Range rngNewEvidenceTableRange, rngNewEvidenceStringRange, rngTemplateEvidenceTable, rngEvidenceStringRange, rngPageBreakHelper, rngActualEvidenceBegin;
                Table tbNewEvidenceTable;
                Paragraph paraEvidenceString;
                int iEvidenceStringRangeStart, iEvidenceStringRangeEnd, iTableID;
                Bitmap bmpDummyImage = new Bitmap(4, 4);
                string strDummyImagePath = strTempPath + "dummy.png", strEvidenceID;
                bmpDummyImage.Save(strDummyImagePath, System.Drawing.Imaging.ImageFormat.Png);
                InlineShape shpDummyImageShape, shpScreenShot;
                Range rngScreenShotRange;

                openWordApp();
                if (!bResumedSession)
                {
                    openTemplateDocument(true);
                    cloneDocument();
                    bgwPrepareTargetDocumentSender.ReportProgress(1);
                    closeTemplateDocument();
                }

                openTemplateDocument(true);

                rngTemplateEvidenceTable = docTemplateDocument.Tables[2].Range;
                iEvidenceStringRangeStart = docTemplateDocument.Range(rngTemplateEvidenceTable.End, docTemplateDocument.Content.End).Sentences[1].Start;
                iEvidenceStringRangeEnd = docTemplateDocument.Range(rngTemplateEvidenceTable.End, docTemplateDocument.Content.End).Sentences[1].End;
                rngEvidenceStringRange = docTemplateDocument.Range(iEvidenceStringRangeStart, iEvidenceStringRangeEnd);
                openTargetDocument(false);
                try
                {
                    evEvidence.AddTemplateDetails(docTargetDocument.Tables[2], docTargetDocument.Tables[2].Range.Next(WdUnits.wdSentence, 1).Text);
                }
                catch (Exception ex)
                {
                    Log(ex.Message + ex.StackTrace);
                    closeTargetDocument();
                    cloneDocument();
                    bgwPrepareTargetDocumentSender.ReportProgress(1);
                    closeTemplateDocument();
                    openTemplateDocument(true);
                    rngTemplateEvidenceTable = docTemplateDocument.Tables[2].Range;
                    iEvidenceStringRangeStart = docTemplateDocument.Range(rngTemplateEvidenceTable.End, docTemplateDocument.Content.End).Sentences[1].Start;
                    iEvidenceStringRangeEnd = docTemplateDocument.Range(rngTemplateEvidenceTable.End, docTemplateDocument.Content.End).Sentences[1].End;
                    rngEvidenceStringRange = docTemplateDocument.Range(iEvidenceStringRangeStart, iEvidenceStringRangeEnd);
                    openTargetDocument(false);
                    evEvidence.AddTemplateDetails(docTargetDocument.Tables[2], docTargetDocument.Tables[2].Range.Next(WdUnits.wdSentence, 1).Text);
                }



                if (!bResumedSession)
                {
                    docTargetDocument.Tables[1].Rows[1].Cells[2].Range.Text = evEvidence.strPropTestPlanName;
                    docTargetDocument.Tables[1].Rows[2].Cells[2].Range.Text = evEvidence.strPropSolutions;
                    docTargetDocument.Tables[1].Rows[3].Cells[2].Range.Text = evEvidence.dtPropCreatedDate.ToString("dd/MM/yyyy");
                    docTargetDocument.Tables[1].Rows[3].Cells[4].Range.Text = evEvidence.iPropNumberOfEvidences.ToString();
                    docTargetDocument.Tables[1].Rows[4].Cells[2].Range.Text = evEvidence.strPropEnvironment;
                    docTargetDocument.Tables[1].Rows[4].Cells[4].Range.Text = evEvidence.strPropOperatingSystem;
                    docTargetDocument.Tables[1].Rows[5].Cells[2].Range.Text = evEvidence.strPropAssociateID;
                    docTargetDocument.Tables[1].Rows[5].Cells[4].Range.Text = evEvidence.strPropDomain;
                    docTargetDocument.Tables[1].Rows[6].Cells[2].Range.Text = evEvidence.strPropTestData;



                    rngActualEvidenceBegin = docTargetDocument.Range();
                    rngActualEvidenceBegin.SetRange(docTargetDocument.Tables[2].Range.Previous(WdUnits.wdSentence, 1).Start, docTargetDocument.Content.End);
                    rngActualEvidenceBegin.Collapse(WdCollapseDirection.wdCollapseStart);
                    rngActualEvidenceBegin.InsertBreak(WdBreakType.wdPageBreak);

                    docTargetDocument.Range(docTargetDocument.Tables[2].Range.Start, docTargetDocument.Content.End).Delete(1); // delete initial evidence tables.
                }

                rngScreenShotRange = docTargetDocument.Range();
                bReadyToInsertPrerequisiteEvidences = true;

                if (bResumedSession)
                {
                    foreach (string strEvID in dictShpDummyScreenshots.Keys)
                    {
                        if (bgwPrepareTargetDocumentSender.CancellationPending)
                        {
                            saveTargetDocument();
                            e.Cancel = true;
                            return;
                        }
                        if (!dictShpScreenshots.Keys.Contains(strEvID))
                        {
                            rngScreenShotRange.SetRange(dictShpDummyScreenshots[strEvID][0], dictShpDummyScreenshots[strEvID][1] + 1);
                            shpScreenShot = docTargetDocument.Range(dictShpDummyScreenshots[strEvID][0], dictShpDummyScreenshots[strEvID][1] + 1).InlineShapes[1];
                            // blah
                            dictShpScreenshots.Add(strEvID, shpScreenShot);
                        }
                    }
                }



                bScreenshotDictionaryReady = true;


                rngPageBreakHelper = docTargetDocument.Range();
                rngNewEvidenceTableRange = docTargetDocument.Range();
                rngNewEvidenceStringRange = docTargetDocument.Range();

                for (int iEvidenceOrdinal = lstStrPropTablesWritten.Count; iEvidenceOrdinal < evEvidence.iPropNumberOfEvidences; iEvidenceOrdinal++)
                {

                    strEvidenceID = lstPropEvidenceID[iEvidenceOrdinal];

                    rngNewEvidenceTableRange.SetRange(docTargetDocument.Content.End, docTargetDocument.Content.End);
                    tbNewEvidenceTable = docTargetDocument.Tables.Add(rngNewEvidenceTableRange, 1, 1);
                    rngTemplateEvidenceTable.Copy();
                    tbNewEvidenceTable.Range.Paste();

                    rngNewEvidenceStringRange.SetRange(docTargetDocument.Content.End, docTargetDocument.Content.End);
                    paraEvidenceString = docTargetDocument.Paragraphs.Add();
                    rngEvidenceStringRange.Copy();
                    paraEvidenceString.Range.Paste();

                    shpDummyImageShape = paraEvidenceString.Range.InlineShapes.AddPicture(strDummyImagePath);
                    shpDummyImageShape.AlternativeText = strEvidenceID;
                    shpDummyImageShape.Title = strEvidenceID;
                    Log("Adding dummy " + strEvidenceID);
                    dictShpScreenshots.Add(strEvidenceID, shpDummyImageShape);

                    rngPageBreakHelper.SetRange(docTargetDocument.Content.End, docTargetDocument.Content.End);
                    rngPageBreakHelper.Collapse(WdCollapseDirection.wdCollapseEnd);
                    rngPageBreakHelper.InsertBreak(WdBreakType.wdPageBreak);


                    iTableID = iEvidenceOrdinal + 2;
                    docTargetDocument.Tables[iTableID].Rows[1].Cells[2].Range.Text = strEvidenceID;


                    saveTargetDocument();

                    lstStrTablesWritten.Add(strEvidenceID);
                    mreTableSemaphore.Set();

                    if (bEvidenceCapturingCompleted)
                    {
                        // even if user is done, do not return before writing evidences that are already captured but not written
                        if (queStrCapturedEvidences.Count <= 0)
                        {
                            return;
                        }
                    }

                    if (bgwPrepareTargetDocumentSender.CancellationPending && lstStrPropTablesWritten.Count >= 1)
                    {
                        saveTargetDocument();
                        e.Cancel = true;
                        return;
                    }
                }
                saveTargetDocument();
            }
            catch (Exception ex)
            {
                
                Log(ex.Message + ex.StackTrace);
                throw (new Exception(ex.Message));
            }
        }



        /// <summary>
        /// Read data from test plan file and populate preview data grid and status label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void readEvidenceMetadataWork(object sender, DoWorkEventArgs e)
        {
            List<object> lstObjArguments = new List<object>();
            BackgroundWorker bgwReadEvidenceSender = sender as BackgroundWorker;

            lstObjArguments = e.Argument as List<object>;
            
            string strTestPlanFileName = lstObjArguments[0] as string;
            strTemplateFileName = lstObjArguments[1] as string;
            strTargetFolderPath = lstObjArguments[2] as string;
            bool qccheck = (bool)lstObjArguments[3];
            
            tpPlan = new TestPlan();
            try
            {
                if (!qccheck)
                {
                    tpPlan.ParseFile(strTestPlanFileName, bgwReadEvidenceSender);
                }
                else
                {
                    tpPlan.ParseDataSet(lstObjArguments[4], strTargetFolderPath, lstObjArguments[5], lstObjArguments[6], lstObjArguments[7], strTestPlanFileName);
                }
                bMetadataExtractionReturned = true;
                if (bgwReadEvidenceSender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message, "Evidence Collector");
                Log("Invalid test plan\n" + ex.Message);
                e.Cancel = true;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message, "Evidence Collector");
                Log("Exception\n" + ex.Message);
                e.Cancel = true;
                return;
            
            }
            evEvidence = new Evidence(tpPlan.strPropTestPlanName, tpPlan.iPropNumberOfSteps);
            evEvidence.FetchDetails();
            evEvidence.AddDetails(strSolutions, strTestData, strDomain, strEnvironment, strOperatingSystem, strAssociateID);

            strTargetFileURI = strTargetFolderPath +"\\Evidence-" + EvidenceCollector.strPropTestPlanName + ".docx";
            return;
        }




        /// <summary>
        /// Fetch evidence captured from the temporary folder by referring to the captured evidences queue and write them to the target document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void WriteEvidencesBackgroundWork(object sender, DoWorkEventArgs e)
        {
            string strCurrentEvidenceID;
            BackgroundWorker bgwWriteEvidencesSender = sender as BackgroundWorker;

            while (!bTargetDocumentOpen || !bScreenshotDictionaryReady) ;


            while (true)
            {
                if (bgwWriteEvidencesSender.CancellationPending)
                {
                    saveTargetDocument();
                    e.Cancel = true;
                    return;
                }
                
                while (queStrCapturedEvidences.Count <= 0)
                {
                    if (bEvidenceCapturingCompleted)
                    {
                        return;
                    }
                    Log("Waiting for new evidences to be captured");
                    bWaitingForEvidence = true;
                    mreEvidenceSemaphore.WaitOne();
                    mreEvidenceSemaphore.Reset();
                    bWaitingForEvidence = false;

                    if (bgwWriteEvidencesSender.CancellationPending)
                    {
                        saveTargetDocument();
                        e.Cancel = true;
                        return;
                    }

                    if (bEvidenceCapturingCompleted)
                    {
                        return;
                    }
                }



                while (queStrCapturedEvidences.Count > 0)
                {
                    while (!lstStrTablesWritten.Contains(queStrCapturedEvidences.Peek()))
                    {
                        Log("Waiting for new table to be inserted");
                        bWaitingForTable = true;
                        mreTableSemaphore.WaitOne();
                        mreTableSemaphore.Reset();
                        bWaitingForTable = false;

                        if (bgwWriteEvidencesSender.CancellationPending)
                        {
                            saveTargetDocument();
                            e.Cancel = true;
                            return;
                        }
                    }
                    if (bgwWriteEvidencesSender.CancellationPending)
                    {
                        saveTargetDocument();
                        e.Cancel = true;
                        return;
                    }
                    strCurrentEvidenceID = queStrCapturedEvidences.Peek();
                    writeEvidence(strCurrentEvidenceID); // peek and dequeue used instead of single dequeue to prevent race condition bug.
                    queStrCapturedEvidences.Dequeue();
                    bgwWriteEvidencesSender.ReportProgress(GetEvidenceOrdinalOf(strCurrentEvidenceID));
                }
            }
        }

        internal static void CaptureEvidenceBackgroundWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgwCaptureEvidenceSender = sender as BackgroundWorker;
            string strEvidenceID = e.Argument as string;
            string strEvidenceImageURI;
            strEvidenceImageURI = strScreenshotSavePath + strEvidenceID + ".png";
            try
            {
                LaunchUtility.LaunchScreenFilter(strEvidenceImageURI, bUseSecondaryScreen);
                if (ScreenFilter.bEscPressed)
                {
                    ScreenFilter.bEscPressed = false;
                    e.Cancel = true;
                    return;
                }


                MemoryStream ms_ = new MemoryStream();
                byte[] by_ = File.ReadAllBytes(strEvidenceImageURI);
                ms_.Write(by_, 0, by_.Length);

                if (!bPrerequisiteEvidence)
                {
                    bmpCurrentImageBitmap = (Bitmap)Image.FromStream(ms_);
                    evEvidence.AddActualEvidence(strEvidenceID, GetEvidenceOrdinalOf(strEvidenceID), strEvidenceImageURI, "Passed", "N/A");
                }
                else
                {
                    bmpEvidenceImageBitmap = (Bitmap)Image.FromStream(ms_);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form {TopMost = true}, "Failed to capture evidence " + strEvidenceID + "\n\n" + ex.Message);
                e.Cancel = true;
                return;
            }
            if (bPrerequisiteEvidence)
            {
                Log("About to write prerequisite evidence " + strEvidenceID);
                writePrerequisiteEvidence(strEvidenceImageURI);
            }
            else
            {
                if (!queStrCapturedEvidences.Contains(strEvidenceID))
                {
                    Log("Adding evidence " + strEvidenceID + " to queue");
                    queStrCapturedEvidences.Enqueue(strEvidenceID);
                }
            }
        }

        private static void writePrerequisiteEvidence(string strPrerequisiteEvidenceImageURI)
        {
            while (!bTargetDocumentOpen || !bReadyToInsertPrerequisiteEvidences) ;

            InlineShape shpFirstPrerequisiteEvidence = null, shpLastPrerequisiteEvidence = null;
            Range rngPrerequisiteEvidences = null;
            for (int i = 1; i<= docTargetDocument.InlineShapes.Count; i++)
            {
                if (PREREQUISITE_ALT_TEXT.Equals(docTargetDocument.InlineShapes[i].AlternativeText))
                {
                    if (null == shpFirstPrerequisiteEvidence)
                    {
                        shpFirstPrerequisiteEvidence = docTargetDocument.InlineShapes[i];
                    }
                    shpLastPrerequisiteEvidence = docTargetDocument.InlineShapes[i];
                }
                if (lstPropEvidenceID.Contains(docTargetDocument.InlineShapes[i].AlternativeText))
                {
                    break;
                }
            }



            if (null == shpFirstPrerequisiteEvidence)
            {

                docTargetDocument.Tables[1].Range.Next(WdUnits.wdSentence, 3).Delete(1); // delete pre-requisite evidence placeholder. 1 paragraph marker + 2 sentences away from the first table
                shpLastPrerequisiteEvidence = docTargetDocument.Tables[1].Range.Next(WdUnits.wdSentence, 3).InlineShapes.AddPicture(strPrerequisiteEvidenceImageURI);
            }
            else
            {
                rngPrerequisiteEvidences = shpLastPrerequisiteEvidence.Range;
                rngPrerequisiteEvidences.SetRange(rngPrerequisiteEvidences.End+2, rngPrerequisiteEvidences.End+2);
                shpLastPrerequisiteEvidence = rngPrerequisiteEvidences.InlineShapes.AddPicture(strPrerequisiteEvidenceImageURI);
            }

            shpLastPrerequisiteEvidence.AlternativeText = PREREQUISITE_ALT_TEXT;
            shpLastPrerequisiteEvidence.Title = PREREQUISITE_ALT_TEXT;
            rngPrerequisiteEvidences = shpLastPrerequisiteEvidence.Range;
            rngPrerequisiteEvidences.Collapse(WdCollapseDirection.wdCollapseEnd);
            rngPrerequisiteEvidences.InsertBreak(WdBreakType.wdPageBreak);

            Log("Written prerequisite evidence " + strPrerequisiteEvidenceImageURI);


            if (File.Exists(strPrerequisiteEvidenceImageURI))
            {
                File.Delete(strPrerequisiteEvidenceImageURI);
            }
        }


        /// <summary>
        /// Add an acutal evidence captured and it's details to the Evidence class object by calling addactualevidences routine of Evidence class.
        /// </summary>
        /// <param name="strParamEvidenceID"></param>
        /// <param name="iParamEvidenceOrdinal"></param>
        /// <param name="strParamEvidenceImageURI"></param>
        /// <param name="strParamStatus"></param>
        /// <param name="strParamComments"></param>
        public static void AddActualEvidence(string strParamEvidenceID, int iParamEvidenceOrdinal, string strParamEvidenceImageURI, string strParamStatus, string strParamComments)
        {
            evEvidence.AddActualEvidence(strParamEvidenceID, iParamEvidenceOrdinal, strParamEvidenceImageURI, strParamStatus, strParamComments);
        }






        public static List<string> lstStrPropEvidencesWritten
        {
            get
            {
                return lstStrEvidencesWritten;
            }
        }

        public static int iPropNumberOfEvidences
        {
            get
            {
                return evEvidence.iPropNumberOfEvidences;
            }
        }

        public static string strPropTestPlanName
        {
            get
            {
                return evEvidence.strPropTestPlanName;
            }
        }

        public static string PREREQUISITE_ALT_TEXT
        {
            get
            {
                return "__PREREQUISITE__";
            }
        }

        public static string DUMMY_PREREQUISITE_ALT_TEXT
        {
            get
            {
                return "__PREREQUISITE__";
            }
        }





        /// <summary>
        /// reinitialize the evidence collector class for another test plan on the same process execution.
        /// </summary>
        public static void Initialize()
        {
            appPreviewWordApp = null;
            docPreviewDocument = null;
            targetWriterWordApp = null;
            docTemplateDocument = null;
            docTargetDocument = null;
            strTempPath = System.IO.Path.GetTempPath();
            dictShpScreenshots = new Dictionary<string, InlineShape>();
            dictShpDummyScreenshots = new Dictionary<string, int[]>();
            queStrCapturedEvidences = new Queue<string>();
            mreEvidenceSemaphore = new ManualResetEvent(false);
            mreTableSemaphore = new ManualResetEvent(false);
            mreMetadataExtractionSemaphore = new ManualResetEvent(false);
            lstStrTablesWritten = new List<string>();
            lstStrEvidencesWritten = new List<string>();

            bPreProcessingComplete = false;
            bEvidenceCapturingCompleted = false;
            bWriteEvidencesCompleted = false;
            bPrepareTargetDocumentCompleted = false;

            bWaitingForEvidence = false;
            bWaitingForTable = false;
            bWaitingForMetadata = false;
            bScreenshotDictionaryReady = true;
            bReadyToInsertPrerequisiteEvidences = false;

            bMetadataExtractionReturned = false;
            bMetadataExtractionStarted = false;
            bUseSecondaryScreen = false;
            bmpCurrentImageBitmap = new Bitmap(1, 1);
            bmpEvidenceImageBitmap = new Bitmap(1, 1);
        }





        /// <summary>
        /// Fetch an evidence from the temp folder referenced by the evidence ID (step number) and write it to the target document.
        /// </summary>
        /// <param name="strEvidenceID"></param>
        private static void writeEvidence(string strEvidenceID)
        {
            string strScreenshotImageURI;
            Range rngScreenshotRange = null;
            InlineShape shpScreenshot = null;

            int iEvidenceOrdinal = tpPlan.lstStrPropEvidenceID.IndexOf(strEvidenceID);
            int iTableID = iEvidenceOrdinal + 2;


            strScreenshotImageURI = evEvidence.dictPropActualEvidences[strEvidenceID].strPropEvidenceImageURI;
            
            foreach (InlineShape shpImage in docTargetDocument.InlineShapes)
            {
                if (strEvidenceID.Equals(shpImage.AlternativeText))
                {
                    shpScreenshot = shpImage;
                    rngScreenshotRange = shpImage.Range;
                    break;
                }
            }

            if (shpScreenshot == null || rngScreenshotRange == null)
            {
                Log("Corrupted target document. Could not save evidence for " + strEvidenceID);
                return;
            }


            try
            {
                shpScreenshot.Delete();
                dictShpScreenshots[strEvidenceID] = rngScreenshotRange.InlineShapes.AddPicture(strScreenshotImageURI);
                dictShpScreenshots[strEvidenceID].AlternativeText = strEvidenceID;
                dictShpScreenshots[strEvidenceID].Title = strEvidenceID;
                Log(" \nAdd actual " + dictShpScreenshots[strEvidenceID].AlternativeText);
                if (!lstStrEvidencesWritten.Contains(strEvidenceID))
                {
                    lstStrEvidencesWritten.Add(strEvidenceID);
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(new Form {TopMost = true}, "Could not save evidence for " + strEvidenceID);
            }

            deleteEvidenceImage(strScreenshotImageURI);
            docTargetDocument.Tables[iTableID].Rows[1].Cells[4].Range.Text = evEvidence.dictPropActualEvidences[strEvidenceID].strStatus;
            docTargetDocument.Tables[iTableID].Rows[2].Cells[2].Range.Text = evEvidence.dictPropActualEvidences[strEvidenceID].strComments;
            saveTargetDocument();
        }

        /// <summary>
        /// Remove an evidence image after it is written to the target document from the evidence images folder and move it to preview images folder.
        /// </summary>
        /// <param name="strScreenshotImageURI"></param>
        private static void deleteEvidenceImage(string strScreenshotImageURI)
        {
            string strPreviewImageURI;
            string strBaseFolder;
            string strImageName;
            strBaseFolder = strScreenshotImageURI.Remove(strScreenshotImageURI.LastIndexOf("\\"));
            strImageName =  strScreenshotImageURI.Remove(0, strBaseFolder.Length + 1);
            strPreviewImageURI = strBaseFolder + "\\preview\\" + strImageName;
            try
            {
                if (System.IO.File.Exists(strPreviewImageURI))
                {
                    System.IO.File.Delete(strPreviewImageURI);
                }
                if (File.Exists(strScreenshotImageURI))
                {
                    System.IO.File.Move(strScreenshotImageURI, strPreviewImageURI);
                }
                else
                {
                    Log("Could not find the captured evidence image\n[" + strScreenshotImageURI + "]");
                }
            }
            catch(Exception ex)
            {
                Log("Warning: Could not delete " + strScreenshotImageURI +  "\n" + ex.Message);
            }
        }





        /// <summary>
        /// Create a target document from the template document.
        /// </summary>
        private static void cloneDocument()
        {
            try
            {
                if (!bTemplateDocumentOpen)
                {
                    MessageBox.Show(new Form {TopMost = true}, "Could not open template document for cloning");
                    Environment.Exit(-1);
                }


                while (true)
                {
                    try
                    {
                        if (File.Exists(strTargetFileURI))
                        {
                            FileStream fs_ = File.Open(strTargetFileURI, FileMode.Append);
                            fs_.Close();
                        }
                        break;
                    }
                    catch
                    {
                        MessageBox.Show(new Form {TopMost = true}, "Target file " + strTargetFileURI + " is open in another application. Please close it to press OK ");
                    }
                }


                if (File.Exists(strTargetFileURI))
                {
                    File.Delete(strTargetFileURI);
                }
               
                docTemplateDocument.SaveAs2(strTargetFileURI);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show(new Form {TopMost = true}, "Unable to save target file " + strTargetFileURI + ". Check " + strCrashLog);
                try
                {
                    System.IO.File.WriteAllText(strCrashLog, ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.ToString());
                }
                catch
                {

                }
                closeTargetDocument();
                closeWordApp();
                Environment.Exit(-1);
            }
        }




        private static void saveTargetDocument()
        {
            try
            {
                if (bTargetDocumentOpen)
                {
                    docTargetDocument.Save();
                }
                else
                {
                    MessageBox.Show(new Form {TopMost = true}, "Could not save target document. Document not open");
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show(new Form {TopMost = true}, "Unable to save target file " + strTargetFileURI + ". Check " + strCrashLog);
                try
                {
                    System.IO.File.WriteAllText(strCrashLog, ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.ToString());
                }
                catch
                {

                }
                closeTargetDocument();
                closeWordApp();
                Environment.Exit(-1);
            }
        }




        private static void openTargetDocument(bool bReadOnly)
        {
            try
            {
                if (bTargetDocumentOpen)
                {
                    Log("Target document already open");
                    return;
                }
                if (!bWordAppOpen)
                {
                    MessageBox.Show(new Form {TopMost = true}, "Could not open target document. Interop word application is not running");
                    Environment.Exit(-1);
                }


                while (!bReadOnly)
                {
                    FileInfo fi = new FileInfo(strTargetFileURI);  
                    try
                    {
                                              
                        FileStream fs_ = File.Open(strTargetFileURI, FileMode.Append);
                        fs_.Close();
                        break;
                    }
                    catch
                    {
                        if (MessageBox.Show(new Form { TopMost = true }, "Target file " + strTargetFileURI + " is open in another application. Please close it to and press Retry ","Close File", MessageBoxButtons.RetryCancel) == DialogResult.Retry) 
                        {
                            try
                            {
                                closeTargetDocument();
                               
                                FileStream fs_ = File.Open(strTargetFileURI, FileMode.Append);
                                fs_.Close();
                            }
                            catch 
                            { 
                            MessageBox.Show(new Form { TopMost = true }, "Close option for the file "+strTargetFileURI+ " was not successful.Click Ok to Kill the File process , Else use the task manger and kill the process with id "+ GetProcessesLockingFile(fi.Name));
                            Process.GetProcessById(GetProcessesLockingFile(fi.Name)).Kill();
                            }

                        }
                    }
                }

                docTargetDocument = targetWriterWordApp.Documents.Open(strTargetFileURI, ReadOnly: bReadOnly);
                bTargetDocumentOpen = true;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show(new Form {TopMost = true}, "Unable to open target file " + strTargetFileURI + " for writing. Check " + strCrashLog);
                try
                {
                    System.IO.File.WriteAllText(strCrashLog, ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.ToString());
                }
                catch
                {
                }
                Environment.Exit(-1);
            }

        }


        private static void openTemplateDocument(bool bReadOnly)
        {
            try
            {
                if (bTemplateDocumentOpen)
                {
                    Log("Template document already open");
                    return;
                }
                if (!bWordAppOpen)
                {
                    MessageBox.Show(new Form {TopMost = true}, "Could not open template document. Intereop Word is not running");
                    Environment.Exit(-1);
                }
                docTemplateDocument = targetWriterWordApp.Documents.Open(strTemplateFileName, ReadOnly: bReadOnly);
                bTemplateDocumentOpen = true;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show(new Form {TopMost = true}, "Unable to open template file " + strTemplateFileName + " for reading. Check " + strCrashLog);
                try
                {
                    System.IO.File.WriteAllText(strCrashLog, ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.ToString());
                }
                catch
                {

                }
                Environment.Exit(-1);
            }
            Log("leaving open template");
        }

        /// <summary>
        /// Target document preview within the application.
        /// </summary>
        internal static void PreviewTargetDocument()
        {
            appPreviewWordApp = new Microsoft.Office.Interop.Word.Application();
            appPreviewWordApp.Visible = true;
            docPreviewDocument = appPreviewWordApp.Documents.Open(strTargetFileURI, ReadOnly: true);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void closeTargetDocument()
        {
            try
            {
                if (!bTargetDocumentOpen)
                {
                    Log("Target document is already closed");
                    return;
                }
                docTargetDocument.Close();
                bTargetDocumentOpen = false;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void closeTemplateDocument()
        {
            try
            {
                if (!bTemplateDocumentOpen)
                {
                    Log("Template document is already closed");
                    return;
                }
                docTemplateDocument.Close();
                bTemplateDocumentOpen = false;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
            }
        }



        private static void openWordApp()
        {
            if (bWordAppOpen)
            {
                Log("Word App is already open");
                return;
            }
            targetWriterWordApp = new Microsoft.Office.Interop.Word.Application();
            bWordAppOpen = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void closeWordApp()
        {
            try
            {
                if (!bWordAppOpen)
                {
                    Log("Word app is already closed");
                    return;
                }
                targetWriterWordApp.Quit();
                bWordAppOpen = false;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
            }
        }
        public static int  GetProcessesLockingFile(string filePath)
        {

            var procs = Process.GetProcessesByName("winword");
            foreach (var item in procs)
                if (item.MainWindowTitle.Contains(filePath))
                {
                    return item.Id;

                }
                else if (string.IsNullOrEmpty(item.MainWindowTitle))
                {
                    item.Kill();
                }
                { }
            return -1;
        }



    }
}