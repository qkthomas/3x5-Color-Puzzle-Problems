using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class LoopKiller
    {
        private Stack<StateNode> mOpenStack = new Stack<StateNode>();
        private List<State> mCloseList = new List<State>();

        public bool isInCloseList(State IN_state)
        {
            foreach (State s in this.mCloseList)
            {
                if(s == IN_state)
                {
                    return true;
                }
            }
            return false;
        }

        public void PushToOpenStack(StateNode IN_state_node)
        {
            this.mOpenStack.Push(IN_state_node);
        }

        public StateNode PopFromOpenStack()
        {
            return this.mOpenStack.Pop();
        }

        public void AddToCloseList(State IN_state)
        {
            this.mCloseList.Add(IN_state);
        }
    }
}
