using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class LoopKiller
    {
        private LinkedList<StateNode> mOpenList = new LinkedList<StateNode>();
        private List<State> mCloseList = new List<State>();

        public LoopKiller CloneWithSameOpenList()
        {
            LoopKiller cloned_loopkiller = new LoopKiller();
            cloned_loopkiller.mOpenList = this.mOpenList;
            cloned_loopkiller.mCloseList = new List<State>(this.mCloseList);
            return cloned_loopkiller;
        }

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

        public void Reset()
        {
            this.mOpenList.Clear();
            this.mCloseList.Clear();
        }

        public StateNode PollSmallestFromOpenList()
        {
            if (true == this.mOpenList.Any())
            {
                var smallest_node = this.mOpenList.First;
                var iterator_node = this.mOpenList.First.Next;
                while (null != iterator_node)
                {
                    if (iterator_node.Value < smallest_node.Value)
                    {
                        smallest_node = iterator_node;
                    }
                    iterator_node = iterator_node.Next;

                }
                StateNode smallest_value = smallest_node.Value;
                this.mOpenList.Remove(smallest_node);
                return smallest_value;
            }
            else
            {
                return null;
            }
        }

        public void OfferToOpenQueue(StateNode IN_state_node)
        {
            this.mOpenList.AddLast(IN_state_node);
        }

        public StateNode PollFromOpenQueue()
        {
            return this.PopFromOpenStack();
        }

        public void PushToOpenStack(StateNode IN_state_node)
        {
            this.mOpenList.AddFirst(IN_state_node);
        }

        public StateNode PopFromOpenStack()
        {
            if (null != this.mOpenList.First)
            {
                StateNode top = this.mOpenList.First.Value;
                this.mOpenList.RemoveFirst();
                return top;
            }
            else
            {
                return null;
            }
        }

        public void AddToCloseList(State IN_state)
        {
            this.mCloseList.Add(IN_state);
        }
    }
}
