using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Moving;

namespace _16ColorsPuzzle.Data
{
    class StateNode : IComparable<StateNode>
    {
        #region Implementation of IComparable<StateNode>
        int IComparable<StateNode>.CompareTo(StateNode other_statenode)
        {
            int this_comparable_value = this.mCurrentState.HeuristicValue + this.Level;
            int other_comparable_value = other_statenode.mCurrentState.HeuristicValue + other_statenode.Level;
            if(this_comparable_value > other_comparable_value)
            {
                return 0x100;
            }
            else if (this_comparable_value < other_comparable_value)
            {
                return -0x100;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Override operator for comparison
        public static bool operator >(StateNode a, StateNode b)
        {
            int a_comparable_value = a.mCurrentState.HeuristicValue + a.Level;
            int b_comparable_value = b.mCurrentState.HeuristicValue + b.Level;
            if (a_comparable_value > b_comparable_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(StateNode a, StateNode b)
        {
            int a_comparable_value = a.mCurrentState.HeuristicValue + a.Level;
            int b_comparable_value = b.mCurrentState.HeuristicValue + b.Level;
            if (a_comparable_value < b_comparable_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(StateNode a, StateNode b)
        {
            int a_comparable_value = a.mCurrentState.HeuristicValue + a.Level;
            int b_comparable_value = b.mCurrentState.HeuristicValue + b.Level;
            if (a_comparable_value == b_comparable_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <=(StateNode a, StateNode b)
        {
            int a_comparable_value = a.mCurrentState.HeuristicValue + a.Level;
            int b_comparable_value = b.mCurrentState.HeuristicValue + b.Level;
            if (a_comparable_value <= b_comparable_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator >=(StateNode a, StateNode b)
        {
            int a_comparable_value = a.mCurrentState.HeuristicValue + a.Level;
            int b_comparable_value = b.mCurrentState.HeuristicValue + b.Level;
            if (a_comparable_value >= b_comparable_value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Fields
        private State mCurrentState = null;
        private StateNode mParent = null;
        private List<StateNode> mChildren = new List<StateNode>();
        private LoopKiller mLoopKiller = null;      //openlist will be useless for this class
        private int mLevel = int.MinValue; 
        #endregion

        #region Properties
        public StateNode Parent
        {
            get { return this.mParent; }
        }
        public State InnerState
        {
            get { return this.mCurrentState; }
        }

        public List<StateNode> Children
        {
            get { return this.mChildren; }
        }

        public int Level
        {
            get { return this.mLevel; }
        } 
        #endregion

        #region Object creation, constructors
        //this constructor need to be better
        private StateNode(State state, StateNode parent, LoopKiller loopkiller)
        {
            this.mCurrentState = state;
            this.mParent = parent;
            if (null == this.mParent)
            {
                this.mLevel = 0;
            }
            else
            {
                this.mLevel = this.mParent.mLevel + 1;
            }
            this.mLoopKiller = loopkiller;
        }

        public static StateNode CreateNewRootNode(State root_state, LoopKiller loopkiller_of_tree)
        {
            StateNode new_root = new StateNode(root_state, null, loopkiller_of_tree);
            return new_root;
        } 
        #endregion

        #region methods
        public void ToBeVisited()
        {
            this.mLoopKiller.AddToCloseList(this.mCurrentState);
        }

        public void Branch()
        {
            List<IMover<State>> lst_movers = new List<IMover<State>>();
            lst_movers.Add(new LeftMover());
            lst_movers.Add(new RightMover());
            lst_movers.Add(new UpMover());
            lst_movers.Add(new DownMover());
            foreach (IMover<State> mover in lst_movers)
            {
                if (mover.CanMove(mCurrentState))
                {
                    State new_state = mCurrentState.CreatNewStateFromMoving(mover);
                    if (!this.mLoopKiller.isInCloseList(new_state))
                    {
                        StateNode new_node = new StateNode(new_state, this, this.mLoopKiller.CloneWithSameOpenList());
                        this.mChildren.Add(new_node);       //need to do some improvement on the constructor
                    }
                }
            }
        } 
        #endregion
    }
}
