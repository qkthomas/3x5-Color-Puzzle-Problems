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
            IN_state_node.mTailly = null;
            bool add_result = this.mOpenSortedList.Add(IN_state_node);
            if (false == add_result)
            {
                SortedSet<StateNode> view_sn = this.mOpenSortedList.GetViewBetween(IN_state_node, IN_state_node);
                StateNode found_node = view_sn.Min;
                if (null == found_node.mTailly)
                {
                    found_node.mTailly = new LinkedList<StateNode>();
                }
                found_node.mTailly.AddLast(IN_state_node);
                //StateNode to_next_node_in_chain_of_found_node = found_node.mNextNodeWithSameHeuristic;
                //to_next_node_in_chain_of_found_node can be null, but it does not matter
                //found_node.mNextNodeWithSameHeuristic = IN_state_node;
                //IN_state_node.mNextNodeWithSameHeuristic = to_next_node_in_chain_of_found_node;

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
                LinkedList<StateNode> tailly_min_sn = min_sn.mTailly;
                if (null != tailly_min_sn)
                {
                    StateNode new_min_sn = tailly_min_sn.First.Value;
                    tailly_min_sn.RemoveFirst();
                    if (new_min_sn.mTailly == null)         //this if-else is just for debug.
                    {
                        if (true == tailly_min_sn.Any())
                        {
                            new_min_sn.mTailly = tailly_min_sn;
                        }
                        else
                        {
                            new_min_sn.mTailly = null;
                        }
                    }
                    else
                    {
                        //something wrong
                        int bp = 0;
                    }
                    this.mOpenSortedList.Remove(min_sn);
                    bool add_result = this.mOpenSortedList.Add(new_min_sn);
                    if (false == add_result)
                    {
                        //something wrong
                        int bp = 0;
                    }
                }
                else
                {
                    //if min_sn is the only one with the value in mOpenSortedList, just remove it after polling.
                    this.mOpenSortedList.Remove(min_sn);
                }
                min_sn.mTailly = null;
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
                if (s == IN_state)
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

        //public StateNode PollSmallestFromOpenList()     //depricated due to bad performance.
        //{
        //    if (true == this.mOpenList.Any())
        //    {
        //        var smallest_node = this.mOpenList.First;
        //        var iterator_node = this.mOpenList.First.Next;
        //        while (null != iterator_node)
        //        {
        //            if (iterator_node.Value < smallest_node.Value)
        //            {
        //                smallest_node = iterator_node;
        //            }
        //            iterator_node = iterator_node.Next;

        //        }
        //        StateNode smallest_value = smallest_node.Value;
        //        this.mOpenList.Remove(smallest_node);
        //        return smallest_value;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

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