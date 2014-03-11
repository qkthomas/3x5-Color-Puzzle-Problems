using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace _16ColorsPuzzle
{
    class Chip
    {
        public readonly Color mChipColor;

        public Chip(Color IN_color)
        {
            this.mChipColor = IN_color;
        }

        public static bool operator ==(Chip a, Chip b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            if (a.mChipColor.ToKnownColor() == b.mChipColor.ToKnownColor())
            {
                return true;
            }

            // Return true if the fields match:
            return (a.mChipColor == b.mChipColor);
        }

        public static bool operator !=(Chip a, Chip b)
        {
            return !(a == b);
        }
    }
}
