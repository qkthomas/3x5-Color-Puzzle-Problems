using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class State
    {
        #region Static Fields
		public static bool operator == (State a, State b)
        {
            List<Chip> ChipsListA = a.mChipsList;
            List<Chip> ChipsListB = b.mChipsList;
            if (ChipsListA.Count == ChipsListB.Count)
            {
                for (int i = 0; i < ChipsListA.Count; i++)
                {
                    if (ChipsListA[i] != ChipsListB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        } 
	#endregion

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
