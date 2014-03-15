using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Moving;
using System.Drawing;

namespace _16ColorsPuzzle.Data
{
    class State : IComparable<State>
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

        public static bool operator !=(State a, State b)
        {
            return !(a == b);
        }
	#endregion

        #region Iplementation of IComparable
        int IComparable<State>.CompareTo(State other_state)
        {
            if(this.HeuristicValue > other_state.HeuristicValue)
            {
                return 0x100;
            }
            else if(this.HeuristicValue < other_state.HeuristicValue)
            {
                return -0x100;
            }
            else
            {
                return 0;
            }
        } 
        #endregion


        #region non-static fields
        private ChipsList mChipsList = new ChipsList();
        private int mCurrentEmptySpaceIndex;
        private char mDestOfPreviousMove = '_';
        private bool mGoal = false;
        private int HeuristicValue = int.MaxValue;
        #endregion

        #region Object creation, clone
        public static State CreateNewStateFromChipsList(ChipsList lst_chips)
        {
            State new_state = new State(lst_chips);
            new_state.StateEvaluation();
            new_state.ReachGoal();
            return new_state;
        }

        public State CreatNewStateFromMoving(IMover<State> mover)
        {
            State new_state = mover.Move(this);
            new_state.StateEvaluation();
            new_state.ReachGoal();
            return new_state;
        }

        //maybe it is not a good idea.
        public State NewShallowClone()
        {
            State new_state = this.MemberwiseClone() as State;
            new_state.mChipsList = new ChipsList();
            foreach (Chip c in this.mChipsList)
            {
                new_state.mChipsList.Add(c);
            }
            return new_state;
        }

        private State(ChipsList lst_chips)
        {
            this.mChipsList = lst_chips;
            int how_many_empty_space = 0;
            for (int i = 0; i < this.mChipsList.Count; i++)
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
            if (how_many_empty_space == 0)
            {
                throw new Exception("There is no empty space in the configuration!!");
            }
        } 
        #endregion

        #region Properties
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
        #endregion

        #region evaluation and goal checking
        //the smaller the better
        private void StateEvaluation()
        {

        }

        private void ReachGoal()
        {
            int number_of_row = State.smRowColumnConfig.Item1;
            int number_of_column = State.smRowColumnConfig.Item2;
            int offset_first_row_last_row = (number_of_row - 1) * number_of_column;
            for (int i = 0; i < number_of_column; i++)
            {
                if (this.mChipsList[i] != this.mChipsList[i + offset_first_row_last_row])
                {
                    this.mGoal = false;
                    return;
                    //return this.mGoal;
                }
            }
            this.mGoal = true;
            return;
            //return this.mGoal;
        } 
        #endregion
    }
}
