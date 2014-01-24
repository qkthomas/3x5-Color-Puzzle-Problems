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
        public System.Windows.Forms.TextBox debug_text;
        public Form_Debug()
        {
            InitializeComponent();
            debug_text = this.textBox1;
        }
    }
}
