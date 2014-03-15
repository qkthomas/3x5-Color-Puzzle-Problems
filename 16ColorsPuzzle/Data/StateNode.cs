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
            IComparable<State> current_comparable_state = this.mCurrentState;
            return current_comparable_state.CompareTo(other_statenode.mCurrentState);
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
