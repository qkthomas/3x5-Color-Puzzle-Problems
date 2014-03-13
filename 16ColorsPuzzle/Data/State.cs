using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Moving;
using System.Drawing;

namespace _16ColorsPuzzle.Data
{
    class State
    {
        #region Static Fields
        public static readonly Tuple<int, int> smRowColumnConfig = Tuple.Create<int, int>(3, 5);
        public static readonly char smIndexBase = 'A';
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
        private int mCurrentEmptySpaceIndex;
        private char mDestOfPreviousMove = '\0';
        private bool mGoal = false;

        public State(ChipsList lst_chips)
        {
            this.mChipsList = lst_chips;
            int how_many_empty_space = 0;
            for (int i = 0; i < this.mChipsList.Count; i++ )
            {
                if (this.mChipsList[i].mChipColor == Color.Transparent)
                {
                    how_many_empty_space++;
                    this.mCurrentEmptySpaceIndex = i;
                }
                if (how_many_empty_space > 1)
                {
                    throw new Exception("There are more than one empty space in the configuration!!");
                }
            }
            if (how_many_empty_space == 1)
            {
                throw new Exception("There is no empty space in the configuration!!");
            }
        }

        public int CurrentEmptySpaceIndex
        {
            get { return this.mCurrentEmptySpaceIndex; }
            set
            {
                if (0 <= value)
                {
                    this.mCurrentEmptySpaceIndex = value;
                }
                else
                {
                    throw new Exception("Trying to give a int < 0 to State.CurrentEmptySpaceIndex");
                }
            }
        }
        
        public bool Goal
        {
            get { return this.mGoal; }
            set { this.mGoal = value; }
        }

        private bool ReachGoal()
        {
            int number_of_row = State.smRowColumnConfig.Item1;
            int number_of_column = State.smRowColumnConfig.Item2;
            int offset_first_row_last_row = (number_of_row - 1) * number_of_column;
            for (int i = 0; i < number_of_column; i++)
            {
                if (this.mChipsList[i] != this.mChipsList[i + offset_first_row_last_row])
                {
                    this.mGoal = false;
                    return this.mGoal;
                }
            }
            this.mGoal = true;
            return this.mGoal;
        }

        //maybe it is not a good idea.
        public State NewShallowClone()
        {
            return this.MemberwiseClone() as State;
        }

        //may lead to some problems
        public ChipsList InnerChipsList
        {
            get { return this.mChipsList; }
        }

        public char DestinationOfPreviousMove
        {
            get { return this.mDestOfPreviousMove; }
            set { this.mDestOfPreviousMove = value; }
        }

    }
}
