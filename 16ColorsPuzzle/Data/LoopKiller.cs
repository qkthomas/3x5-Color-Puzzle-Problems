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
        private SortedSet<StateNode> mOpenSortedList = new SortedSet<StateNode>();
        private List<State> mCloseList = new List<State>();

        public void AddedToSortedOpenList(StateNode IN_state_node)
        {
            IN_state_node.mNextNodeWithSameHeuristic = null;
            bool add_result = this.mOpenSortedList.Add(IN_state_node);
            if(false == add_result)
            {
                SortedSet<StateNode> view_sn = this.mOpenSortedList.GetViewBetween(IN_state_node, IN_state_node);
                StateNode found_node = view_sn.Min;
                StateNode to_next_node_in_chain_of_found_node = found_node.mNextNodeWithSameHeuristic;

                found_node.mNextNodeWithSameHeuristic = IN_state_node;
                IN_state_node.mNextNodeWithSameHeuristic = to_next_node_in_chain_of_found_node;

                //while (null != to_find_the_last_node_in_chain_of_found_node.mNextNodeWithSameHeuristic)
                //{
                //    to_find_the_last_node_in_chain_of_found_node = to_find_the_last_node_in_chain_of_found_node.mNextNodeWithSameHeuristic;
                //}
                //to_find_the_last_node_in_chain_of_found_node.mNextNodeWithSameHeuristic = IN_state_node;
                //if (to_find_the_last_node_in_chain_of_found_node.Equals(IN_state_node))
                //{
                //    //something wrong
                //    int bp = 0;
                //}

            }
        }

        public StateNode PollMinFromSortedOpenList()
        {
            StateNode min_sn = this.mOpenSortedList.Min;
            if (null != min_sn)
            {
                if (null != min_sn.mNextNodeWithSameHeuristic)
                {
                    StateNode min_sn_next = min_sn.mNextNodeWithSameHeuristic;
                    this.mOpenSortedList.Remove(min_sn);
                    bool add_result = this.mOpenSortedList.Add(min_sn_next);
                    if (false == add_result)
                    {
                        int bp = 0;
                    }
                }
                else
                {
                    this.mOpenSortedList.Remove(min_sn);
                } 
            }
            return min_sn;
        }

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
            this.mOpenSortedList.Clear();
        }

        public StateNode PollSmallestFromOpenList()     //depricated due to bad performance.
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
