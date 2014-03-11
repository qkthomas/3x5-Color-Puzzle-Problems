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
            ChipsList ChipsListA = a.mChipsList;
            ChipsList ChipsListB = b.mChipsList;
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

        private ChipsList mChipsList = new ChipsList();
        private Tuple<int, int> mRowColumnConfig = Tuple.Create<int, int>(3, 5);
        private int mCurrentEmptySpaceIndex;
        private bool mGoal = false;

        public bool Goal
        {
            get { return this.mGoal; }
            set { this.mGoal = value; }
        }

    }
}
