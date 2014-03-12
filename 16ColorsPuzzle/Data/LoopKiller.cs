using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class LoopKiller
    {
        private Stack<State> mOpenStack = new Stack<State>();
        private List<State> mCloseList = new List<State>();

        public bool isInCloseList(State IN_state)
        {
            foreach(State s in this.mCloseList)
            {
                if(s == IN_state)
                {
                    return true;
                }
            }
            return false;
        }

        public void PushToOpenStack(State IN_state)
        {
            this.mOpenStack.Push(IN_state);
        }

        public State PopFromOpenStack()
        {
            return this.mOpenStack.Pop();
        }

        public void AddToCloseList(State IN_state)
        {
            this.mCloseList.Add(IN_state);
        }
    }
}
