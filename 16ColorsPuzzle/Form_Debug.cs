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
    public partial class Form_Debug : Form
    {
        private System.Windows.Forms.TextBox debug_text;
        public Form_Debug()
        {
            InitializeComponent();
            debug_text = this.textBox1;
        }

        public void AppendLine(string str_text)
        {
            this.debug_text.AppendText(str_text + System.Environment.NewLine);
        }
    }
}
