using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16ColorsPuzzle
{
    public partial class Form1 : Form
    {
        Form_Debug fd;
        public Form1()
        {
            InitializeComponent();
            this.fd = new Form_Debug();
            fd.Show();
        }

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Please select a configuration text file";
            ofd.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            if (DialogResult.OK == ofd.ShowDialog())
            {
                fd.debug_text.AppendText(ofd.FileName + "\r\n");
            }
        }
    }
}
