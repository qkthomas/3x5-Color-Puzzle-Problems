using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16ColorsPuzzle
{
    class ChipsList : IEnumerable
    {
        private List<Chip> mChipsList = new List<Chip>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.mChipsList.GetEnumerator();
        }

        public void Add(Chip c)
        {
            this.mChipsList.Add(c);
        }

        public int Count
        {
            get { return this.mChipsList.Count; }
        }

        public Chip this[int index]
        {
            get { return this.mChipsList[index]; }
            set { this.mChipsList[index] = value; }
        }
    }
}
