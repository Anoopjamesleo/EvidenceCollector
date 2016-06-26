using System;
using System.Windows.Forms;
using TDAPIOLELib;

namespace EvidenceCollector
{
    public partial class MultipleTestSelection : Form
    {
        private static TDAPIOLELib.List tstlist = new TDAPIOLELib.List();
        public MultipleTestSelection()
        {
            InitializeComponent();
        }
        public void LoadTestPLans(TestFactory testFactory)
        {
            foreach (Test tst in TestList)
            {

                MTSGrid.Rows.Add(tst.Name, testFactory[tst.ID]["TS_SUBJECT"].Path);
            }

        }

        private void MTSGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TestPlan.SelectedTest = TestList[e.RowIndex + 1];
            this.Close();
        }

        public static TDAPIOLELib.List TestList { get; set; }
    }
        
    }

