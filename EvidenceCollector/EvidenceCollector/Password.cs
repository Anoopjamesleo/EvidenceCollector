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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            configTextBox1.LoadValues();
            textBox1.LoadValues();
            configTextBox1.Text = "http://qualitycenter.cerner.com/qcbin/";
            QCProject.SelectedIndex = 0;
            QCDomain.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.strpass = textBox1.Text;
            MainForm.StrQCURL = configTextBox1.Text;
            MainForm.StrQCProject = Convert.ToString(QCProject.SelectedItem);
            MainForm.StrDomain =Convert.ToString(QCDomain.SelectedItem);            
            this.Close();
        }
    }
}
