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
        private LoopKiller mLoopKiller = null;

        //this constructor need to be better
        private StateNode(State state, StateNode parent, LoopKiller loopkiller)
        {
            this.mCurrentState = state;
            this.mParent = parent;
            this.mLoopKiller = loopkiller;
        }

        public static StateNode CreateNewRootNode(State root_state)
        {
            StateNode new_root = new StateNode(root_state, null, new LoopKiller());
            return new_root;
        }

        private void Branch()
        {
            List<IMover<State>> lst_movers = new List<IMover<State>>();
            lst_movers.Add(new LeftMover());
            lst_movers.Add(new RightMover());
            lst_movers.Add(new UpMover());
            lst_movers.Add(new DownMover());
            foreach(IMover<State> mover in lst_movers)
            {
                State new_state = mover.Move(mCurrentState);
                if (!this.mLoopKiller.isInCloseList(new_state))
                {
                    this.mLoopKiller.PushToOpenStack(new_state);
                    this.mChildren.Add(new StateNode(new_state, this, this.mLoopKiller));       //need to do some improvement on the constructor
                }
            }
        }
    }
}
