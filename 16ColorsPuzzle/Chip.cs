using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace _16ColorsPuzzle
{
    struct Chip
    {
        public readonly Color mChipColor;

        public Chip(Color IN_color)
        {
            this.mChipColor = IN_color;
        }
    }
}
