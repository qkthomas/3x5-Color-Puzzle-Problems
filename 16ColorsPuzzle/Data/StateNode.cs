using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Moving;

namespace _16ColorsPuzzle.Data
{
    class StateNode
    {
        private State mCurrentState = null;
        private StateNode mParent = null;
        private List<StateNode> mChildren = new List<StateNode>();
        private LoopKiller mLoopKiller = null;      //openlist will be useless for this class
        private int mLevel = int.MinValue;

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
            foreach(IMover<State> mover in lst_movers)
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
    }
}
