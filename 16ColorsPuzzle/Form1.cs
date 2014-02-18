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
        private Form_Debug fd;
        private Controller mController;
        public Form1()
        {
            InitializeComponent();
            this.fd = new Form_Debug();
            fd.Show();
            this.mController = new Controller();
            this.panel1.Paint += this.drawChipsConfiguration;
        }

        private void drawChipsConfiguration(object sender, PaintEventArgs e)
        {
            this.mController.DrawConfiguration(e.Graphics, this.panel1.Size);
        }

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Please select a configuration text file";
            ofd.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            #region debug code
            if (DialogResult.OK == ofd.ShowDialog())
            {
                fd.debug_text.AppendText(ofd.FileName + "\r\n");
            }
            #endregion

            this.mController.ReadConfiguration(ofd.FileName);
            this.panel1.Refresh();
        }
    }
}
