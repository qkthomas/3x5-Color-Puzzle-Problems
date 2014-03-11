using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class LoopKiller
    {
        private List<State> mOpenList = new List<State>();
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

        public void AddToOpenList(State IN_state)
        {
            this.mOpenList.Add(IN_state);
        }

        public void AddToCloseList(State IN_state)
        {
            this.mCloseList.Add(IN_state);
        }
    }
}
