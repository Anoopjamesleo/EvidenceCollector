using System;
using System.Windows.Forms;

namespace EvidenceCollector
{
    static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch(Exception ex)
            {
                string strCrashLog = System.IO.Path.GetTempPath() + "crash.log";
                MessageBox.Show("Application terminated unexpectedly. Check " + strCrashLog);
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
    }
}
