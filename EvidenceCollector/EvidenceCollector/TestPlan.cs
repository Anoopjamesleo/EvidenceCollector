using System.Collections.Generic;
using Microsoft.Office.Interop.Word;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using TDAPIOLELib;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace EvidenceCollector
{

    class TestPlan
    {
        string strTestPlanName;
        string[] arrStepData;
        List<string> lstStrEvidenceID;
        List<string[]> lstTestPlanSteps;
        string strPrerequisites;
        Microsoft.Office.Interop.Word.Application appTestPlanReaderWordApp;
        Document docTestPlanDocument;

        public TestPlan()
        {
            appTestPlanReaderWordApp = new Microsoft.Office.Interop.Word.Application();
            docTestPlanDocument = null;
        }

        public List<string[]> lstPropTestPlanSteps
        {
            get
            {
                return lstTestPlanSteps;
            }
        }


        /// <summary>
        /// Parse the test plan document for collecting metadata of the evidences to be captured.
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="bgwReadEvidenceSender"></param>
        public void ParseFile(string strFileName, BackgroundWorker bgwReadEvidenceSender)
        {
            lstTestPlanSteps = new List<string[]>();
            lstStrEvidenceID = new List<string>();
            Cell clStepNameCell, clDescriptionCell, clExpectedResultsCell, clEvidenceRequiredCell;
            Range rngEvidenceIDRange, rngDescriptionRange, rngExpectedResultsRange, rngDecisionStringRange;
            
            Table tbTestPlanTable;
            bool bIsCandidateEvidence;
            string strEvidenceID, strDescription, strExpectedResults, strDecisionString;
            appTestPlanReaderWordApp.Visible = false;
            if (bgwReadEvidenceSender.CancellationPending)
            {
                closeTestPlanReaderWordApp();
                return;
            }
            EvidenceCollector.Log("TestPlanDocument open time: " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second + ":" + DateTime.UtcNow.Millisecond);
            docTestPlanDocument = appTestPlanReaderWordApp.Documents.Open(strFileName, ReadOnly: true);

            strTestPlanName = docTestPlanDocument.Sentences[1].Text.Replace('\n', ' ').Replace('\r', ' ').Replace('\v', ' ').Replace('\t', ' ').Trim(' ');
            tbTestPlanTable = docTestPlanDocument.Tables[1];

            Match m = Regex.Match(docTestPlanDocument.Content.Text, "[Pp]rerequisites.*[Cc]hange [Cc]ontrol");
            strPrerequisites = m.Value;
            strPrerequisites = strPrerequisites.Remove(strPrerequisites.Length - 14, 14);
            strPrerequisites = strPrerequisites.Trim(' ', '\r', '\n', '\t');



            foreach (Row rwTestPlanRow in tbTestPlanTable.Rows)
            {
                if (bgwReadEvidenceSender.CancellationPending)
                {
                    closeTestPlanDocument();
                    closeTestPlanReaderWordApp();
                    return;
                }
                /// Ignore the header row of the table.
                if (rwTestPlanRow.Cells[2].Range.Text.ToLower().Contains("description"))
                {
                    continue;
                }


                clStepNameCell = rwTestPlanRow.Cells[1];
                clDescriptionCell = rwTestPlanRow.Cells[2];
                clExpectedResultsCell = rwTestPlanRow.Cells[3];
                clEvidenceRequiredCell = rwTestPlanRow.Cells[rwTestPlanRow.Cells.Count];


                rngEvidenceIDRange = clStepNameCell.Range;
                strEvidenceID = rngEvidenceIDRange.Text;

                if (rngEvidenceIDRange.ListParagraphs.Count > 0)
                {
                    strEvidenceID = clStepNameCell.Range.ListFormat.ListString;
                }
                else
                {
                    strEvidenceID = clStepNameCell.Range.Text;
                }

                rngDescriptionRange = clDescriptionCell.Range;
                rngExpectedResultsRange = clExpectedResultsCell.Range;
                rngDecisionStringRange = clEvidenceRequiredCell.Range;


                strDescription = rngDescriptionRange.Text;
                strExpectedResults = rngExpectedResultsRange.Text;
                strDecisionString = rngDecisionStringRange.Text;

                // trim the cell marker string "\x0d\x07"
                if (strEvidenceID.Contains("\x0d\x07"))
                {
                    strEvidenceID = strEvidenceID.Remove(strEvidenceID.IndexOf("\x0d\x07"), 2);
                }
                if (strDescription.Contains("\x0d\x07"))
                {
                    strDescription = strDescription.Remove(strDescription.IndexOf("\x0d\x07"), 2);
                }
                if (strExpectedResults.Contains("\x0d\x07"))
                {
                    strExpectedResults = strExpectedResults.Remove(strExpectedResults.IndexOf("\x0d\x07"), 2);
                }
                if (strDecisionString.Contains("\x0d\x07"))
                {
                    strDecisionString = strDecisionString.Remove(strDecisionString.IndexOf("\x0d\x07"), 2);
                }


                if (string.IsNullOrEmpty(strEvidenceID) || string.IsNullOrWhiteSpace(strEvidenceID))
                {
                    throw new Exception("Invalid Step Number");
                }

                arrStepData = new string[3];
                arrStepData[0] = strEvidenceID;
                arrStepData[1] = strDescription;
                arrStepData[2] = strExpectedResults;
                lstTestPlanSteps.Add(arrStepData);
                bIsCandidateEvidence = false;

                /*
                Any non whitespace printable character in the last column of the test plan table is an indicator that screenshot is to be taken for that step.
                So, checking for an ASCII character within range [22,126]
                */
                foreach (char ch in strDecisionString)
                {
                    if ((int)ch >= 33 && (int)ch <= 126)
                    {
                        bIsCandidateEvidence = true;
                        break;
                    }
                }
                if (bIsCandidateEvidence)
                {
                    lstStrEvidenceID.Add(strEvidenceID);
                }
            }
            closeTestPlanDocument();
            closeTestPlanReaderWordApp();
        }

        public void ParseDataSet(params object[] parameters)
        {
            lstTestPlanSteps = new List<string[]>();
            lstStrEvidenceID = new List<string>();
            try
            {
                string strQCURL = (string)parameters[0];
                string strTargetFolderPath = (string)parameters[1];
                string strproject = (string)parameters[2];
                string strdomain = (string)parameters[3];
                string strpass = (string)parameters[4];
                string strTestPlanFileName = Path.GetFileNameWithoutExtension((string)parameters[5]);
                bool bIsCandidateEvidence = false;
                string strscreenprint = string.Empty;
                TDConnection qctd = new TDConnection();
                Test tst;
                
                qctd.InitConnectionEx(strQCURL);
                qctd.ConnectProjectEx(strdomain, strproject, Environment.UserName, strpass);
                if (qctd.Connected)
                {
                    TestFactory testFactory = (TestFactory)qctd.TestFactory;
                    TDFilter testFilter = testFactory.Filter;

                    TDAPIOLELib.List testList;

                    testFilter["TS_NAME"] = "\"" + strTestPlanFileName + "\"";
                    // testFilter["TS_PATH"] = "\"" + strTargetFolderPath + "\"";
                    TDAPIOLELib.List listOfTests = testFilter.NewList();

                    testList = (TDAPIOLELib.List)testFactory.NewList(testFilter.Text);
                    string strdescription = string.Empty;
                    int itestcount = testList.Count;
                    int stepscount = 0;
                    if (testList.Count == 0)
                    {
                        throw new Exception("No test plans Found .Check the testplan name");

                    }
                    else if (testList.Count > 1)
                    {
                        MessageBox.Show("Multiple Test plans found , Click Ok to view and select the Right Test Plan");
                        DisplayListOFTestPlan(testList, testFactory);
                        tst = SelectedTest;
                    }
                    else
                    {
                        tst = testList[1];
                    }
                    if (tst == null)
                    {
                        throw new Exception("No Test plans selected ");
                    }
                    strTestPlanName = tst.Name;
                 //   string noHTML = Regex.Replace(tst["TS_DESCRIPTION"], @"<[^>]+>|&nbsp;", "").Trim();
                  //  noHTML = WebUtility.HtmlDecode(noHTML);
                    string noHTML =RemoveHTML(tst["TS_DESCRIPTION"]);
                    Match m = Regex.Match(noHTML, "(?s)[Pp]rerequisites.*[Cc]hange [Cc]ontrol");
                    strPrerequisites = m.Value;
                    DesignStepFactory dsf = tst.DesignStepFactory;
                    TDAPIOLELib.List dslflist = dsf.NewList("");
                    stepscount = dslflist.Count;
                    foreach (DesignStep ds in dslflist)
                    {

                        arrStepData = new string[3];
                        arrStepData[0] = ds.StepName;
                        arrStepData[1] = RemoveHTML(ds.StepDescription);
                        arrStepData[2] = RemoveHTML(ds.StepExpectedResult);
                        lstTestPlanSteps.Add(arrStepData);
                        bIsCandidateEvidence = false;
                        strscreenprint = (string)ds["DS_USER_01"];
                        if (!string.IsNullOrEmpty(strscreenprint))
                        {
                            foreach (char ch in strscreenprint)
                            {
                                if ((int)ch >= 33 && (int)ch <= 126)
                                {
                                    bIsCandidateEvidence = true;
                                    break;
                                }
                            }
                            if (bIsCandidateEvidence)
                            {
                                lstStrEvidenceID.Add(ds.StepName);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            
            }
        }

        private void DisplayListOFTestPlan(TDAPIOLELib.List testList, TestFactory testFactory)
        {
            MultipleTestSelection mts = new MultipleTestSelection();
            MultipleTestSelection.TestList = (TDAPIOLELib.List)testList;
            mts.LoadTestPLans(testFactory);
            mts.ShowDialog();
            
        }
           

        private void closeTestPlanReaderWordApp()
        {
            try
            {
                appTestPlanReaderWordApp.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
            }
        }

        private void closeTestPlanDocument()
        {
            try
            {
                docTestPlanDocument.Close();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
            }
        }

        public string strPropTestPlanName
        {
            get
            {
                return strTestPlanName;
            }
        }
        public int iPropNumberOfSteps
        {
            get
            {
                return lstStrEvidenceID.Count;
            }
        }

        public List<string> lstStrPropEvidenceID
        {
            get
            {
                return lstStrEvidenceID;
            }
        }

        #region AddedByAchal
        public string strPropPrerequisites
        {
            get
            {
                return strPrerequisites;
            }
        }
        #endregion


        public static Test SelectedTest { get; set; }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        private string RemoveHTML(string strinput)
        {
            string strtemp = string.Empty;
            strtemp = Regex.Replace(strinput, @"<[^>]+>|&nbsp;", "").Trim();
            strtemp = WebUtility.HtmlDecode(strtemp);
            return strtemp;
        }

    }
}
