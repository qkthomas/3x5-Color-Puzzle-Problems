using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class State
    {
        private List<Chip> mChipsList = new List<Chip>();

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
