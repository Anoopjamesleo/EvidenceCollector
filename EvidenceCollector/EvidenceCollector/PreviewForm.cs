using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidenceCollector
{
    public partial class PreviewForm : Form
    {

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public PreviewForm()
        {
            InitializeComponent();
            previewFormNotifyIcon.Visible = false;
        }

        private void dockButton_Click(object sender, EventArgs e)
        {
            previewFormNotifyIcon.Visible = true;
            previewFormNotifyIcon.ShowBalloonTip(500, "minimize", "minimizing to tray", ToolTipIcon.Info);
            // Visible = false;
        }

        private void previewFormNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            previewFormNotifyIcon.Visible = false;
            Visible = true;
            MessageBox.Show("coming back");
        }
    }
}
