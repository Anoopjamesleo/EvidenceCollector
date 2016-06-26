using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Diagnostics;

namespace EvidenceCollector
{
    /// <summary>
    /// facilitates continuation of work from a previous seesion
    /// </summary>
    static class SessionRestore
    {
        static Microsoft.Office.Interop.Word.Application wordApp = null;
        static Document docTargetFile = null;
        static string strTargetDocumentName;

        /// <summary>
        /// reads data from the previously saved evidence file to create environment required for session restore
        /// </summary>
        /// <param name="strFileName">The path of the saved evidence</param>
        public static void ResumeState(String strFileName)
        {
            strTargetDocumentName = strFileName;
            Range rngCellRange;
            Dictionary<string, int[]> dictShpDummyScreenshots= new Dictionary<string, int[]>();
            List<Table> lstCorruptedTables = new List<Table>();
            int[] arrShapeRangeDummy = new int[2];
            Queue<string> queStrCapturedEvidences = new Queue<string>();
            List<string> lstStrTablesWritten = new List<string>();
            List<string> lstStrEvidencesWritten = new List<string>();
            string strEvidenceImagesFolder = System.IO.Path.GetTempPath() + "EvidenceCollector\\" + EvidenceCollector.strPropTestPlanName + "\\";
            wordApp = new Microsoft.Office.Interop.Word.Application();
           
            openTargetDocument(false);

            string strEvidenceId, strStatus, strComments;

            bool bFirstTable = true;
            

            foreach (Table tbCurTable in docTargetFile.Tables)
            {
                if (bFirstTable)
                {
                    bFirstTable = false;
                    continue;
                }
                rngCellRange = tbCurTable.Cell(1, 2).Range;
                strEvidenceId = rngCellRange.Text;

                if (strEvidenceId.Contains("\x0d\x07"))
                {
                    strEvidenceId = strEvidenceId.Remove(strEvidenceId.IndexOf("\x0d\x07"), 2);
                }
                if (string.IsNullOrEmpty(strEvidenceId) || string.IsNullOrWhiteSpace(strEvidenceId))
                {
                    // last table of document incompletely populated.
                    lstCorruptedTables.Add(tbCurTable);
                    EvidenceCollector.Log("One table corrupted, flagged for deletion");
                    continue;
                }
                rngCellRange = tbCurTable.Cell(1, 4).Range;
                strStatus = rngCellRange.Text;
                rngCellRange = tbCurTable.Cell(2, 2).Range;
                strComments = rngCellRange.Text;
                
                if (strStatus.Contains("\x0d\x07"))
                {
                    strStatus = strStatus.Remove(strStatus.IndexOf("\x0d\x07"), 2);
                }
                if (strComments.Contains("\x0d\x07"))
                {
                    strComments = strComments.Remove(strComments.IndexOf("\x0d\x07"), 2);
                }



                if (string.IsNullOrEmpty(strEvidenceId) || string.IsNullOrWhiteSpace(strEvidenceId))
                {
                    throw new Exception("Invalid Step Number");
                }


                InlineShape shape = docTargetFile.Range(tbCurTable.Range.End + 1, docTargetFile.Content.End).InlineShapes[1];
                arrShapeRangeDummy[0] = shape.Range.Start;
                arrShapeRangeDummy[1] = shape.Range.End;

                dictShpDummyScreenshots.Add(strEvidenceId, arrShapeRangeDummy);


                if (strStatus.ToLower().Equals("passed") || strStatus.ToLower().Equals("failed"))
                {
                    lstStrEvidencesWritten.Add(strEvidenceId);
                    EvidenceCollector.AddActualEvidence(strEvidenceId, EvidenceCollector.GetEvidenceOrdinalOf(strEvidenceId), strEvidenceImagesFolder + strEvidenceId + ".png", strStatus, strComments);
                    // SaveInlineShapeToFile(shape, tempPath + strEvidenceId + ".png");
                }


                else
                {
                    if (File.Exists(strEvidenceImagesFolder + strEvidenceId + ".png"))
                    {
                        EvidenceCollector.AddActualEvidence(strEvidenceId, EvidenceCollector.GetEvidenceOrdinalOf(strEvidenceId), strEvidenceImagesFolder + strEvidenceId + ".png", "Passed", "");
                        queStrCapturedEvidences.Enqueue(strEvidenceId);
                    }
                }
                lstStrTablesWritten.Add(strEvidenceId);

            }


            if (Directory.Exists(strEvidenceImagesFolder))
            {
                foreach (string strPotentialEvidenceImages in Directory.GetFiles(strEvidenceImagesFolder))
                {
                    string strID = strPotentialEvidenceImages.Split('\\').LastOrDefault();
                    if (strID.Length >= 5)
                    {
                        strID = strID.Remove(strID.Length - 4, 4);
                    }
                    if (EvidenceCollector.lstPropEvidenceID.Contains(strID) && !queStrCapturedEvidences.Contains(strID))
                    {
                        EvidenceCollector.AddActualEvidence(strID, EvidenceCollector.GetEvidenceOrdinalOf(strID), strPotentialEvidenceImages, "Passed", "");
                        queStrCapturedEvidences.Enqueue(strID);
                    }
                }
            }

            foreach (Table tbCorruptedTable in lstCorruptedTables)
            {
                tbCorruptedTable.Delete();
                EvidenceCollector.Log("One corrupted table deleted");
            }



            EvidenceCollector.LoadSession(dictShpDummyScreenshots, queStrCapturedEvidences, lstStrTablesWritten, lstStrEvidencesWritten);
            docTargetFile.Save();
            docTargetFile.Close();
            wordApp.Quit();

        }

        private static void openTargetDocument(bool bReadOnly)
        {
            FileInfo fi = new FileInfo(strTargetDocumentName);
            string strLockFile = fi.Name;
            strLockFile = fi.Directory + "\\" + "~$" + strLockFile.Remove(0, 2);
            if (File.Exists(strLockFile))
            {
                try
                {
                    File.Delete(strLockFile);
                }
                catch
                {
                }
            }
            while (true)
            {
                
                try
                {
                    FileStream fs_ = File.Open(strTargetDocumentName, FileMode.Append);
                    fs_.Close();
                    break;
                }
                catch
                {
                    if (MessageBox.Show(new Form { TopMost = true }, "Target file " + strTargetDocumentName + " is open in another application. Please close it to and press Retry ", "Close File", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        //try
                        //{

                        //    //Closeword(strTargetDocumentName);
                        //    FileStream fs_ = File.Open(strTargetDocumentName, FileMode.Append);
                        //    fs_.Close();
                        //}
                        //catch
                        //{
                        //    MessageBox.Show(new Form { TopMost = true }, "Close option for the file " + strTargetDocumentName + " was not successful.Click Ok to Kill the File process , Else use the task manger and kill the process with id " + EvidenceCollector.GetProcessesLockingFile(fi.Name));
                           
                        //}

                    }
                }
            }
        
            docTargetFile = wordApp.Documents.Open(strTargetDocumentName);
        }

       public static void  Closeword (string filepath)
        {
            var procs = Process.GetProcessesByName("winword");
            foreach (var item in procs)
                if (string.IsNullOrEmpty(item.MainWindowTitle))
                {
                    item.Kill();
                }
        }

        /*
        /// <summary>
        /// saves images from the evidence file to the specified folder
        /// </summary>
        /// <param name="inlineShape">image extracted from the word file</param>
        /// <param name="wordApplication">the Microsoft.Office.Interop.Word.Application object which contains the evidence document</param>
        /// <param name="strSavePath">the path where the image is saved</param>
        static void SaveInlineShapeToFile(InlineShape inlineShape,string strSavePath)
        {
            // Select and copy the shape to the clipboard

            inlineShape.Select();
            // wordApp.Selection.Copy();
            inlineShape.Range.CopyAsPicture();


            // Check data is in the clipboard
            if (Clipboard.GetDataObject() != null)
            {
                var data = Clipboard.GetDataObject();                

                // Check if the data conforms to a bitmap format
                if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                {
                    // Fetch the image and convert it to a Bitmap
                    Image image = (Image)data.GetData(DataFormats.Bitmap, true);
                    Bitmap currentBitmap = new Bitmap(image);

                    // Save the bitmap to a file
                    currentBitmap.Save(strSavePath,System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
        */
    }
}
