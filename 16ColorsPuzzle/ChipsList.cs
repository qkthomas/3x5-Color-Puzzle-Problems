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

        public ChipsList GetRange(int index, int count)
        {
            ChipsList cplst_to_be_returned = new ChipsList();
            cplst_to_be_returned.mChipsList = this.mChipsList.GetRange(index, count);
            return cplst_to_be_returned;
        }

        public void Clear()
        {
            this.mChipsList.Clear();
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

        public bool isEmpty()
        {
            bool isEmpty = !this.mChipsList.Any();
            return isEmpty;
        }
    }
}
