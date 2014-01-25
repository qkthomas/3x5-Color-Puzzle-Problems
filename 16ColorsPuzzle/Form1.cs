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
        ChipsList cplst;
        public Form1()
        {
            InitializeComponent();
            this.fd = new Form_Debug();
            fd.Show();
            cplst = new ChipsList();
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

            string[] inputed_str = PzzConfigFileReader.ReadAsArray(ofd.FileName);
            this.cplst = PzzConfigFileReader.BuildChipsConfiguration(Tuple.Create<int, int>(3, 5), inputed_str);

            foreach(var cp in cplst)
            {
                cp.DrawMyself(this.panel1);
            }

        }
    }
}
