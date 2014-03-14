using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class StateTree
    {
        #region MyRegion
        public static int smVisitedNodesCount = 0; 
        #endregion

        public enum VisitResult { Goal, NotAGoal, ExceededMaxLevel}
        private StateNode mRootNode;
        private StateNode mCurrentVisitingNode = null;
        private LoopKiller mLoopKiller;

        public StateTree(State root_node_state)
        {
            this.mLoopKiller = new LoopKiller();
            this.mRootNode = StateNode.CreateNewRootNode(root_node_state, this.mLoopKiller);
            this.mLoopKiller.PushToOpenStack(this.mRootNode);
        }

        //Iterative deepening depth-first search
        public void IDDFSVisitNextNode()
        {
            smVisitedNodesCount++;
            int max_search_level = 1;
            while(true)
            {
                VisitResult result = this.DFSVisitNextNode(max_search_level);
                if(VisitResult.Goal == result)
                {
                    break;
                }
                if (VisitResult.ExceededMaxLevel == result)
                {
                    this.ResetTree();
                    max_search_level++;
                    continue;
                }
            }

        }

        private void ResetTree()
        {
            this.mCurrentVisitingNode = null;
            this.mLoopKiller.Reset();
            this.mRootNode = StateNode.CreateNewRootNode(this.mRootNode.InnerState, this.mLoopKiller);
            this.mLoopKiller.PushToOpenStack(this.mRootNode);
        }

        //uninformed BFS
        private VisitResult BFSVisitNextNode()
        {
            smVisitedNodesCount++;
            this.mCurrentVisitingNode = this.mLoopKiller.PollFromOpenQueue();
            if (true == this.mCurrentVisitingNode.InnerState.Goal)
            {
                //reached the goal node
                return VisitResult.Goal;
            }
            else
            {
                if (null != this.mCurrentVisitingNode)
                {
                    this.mLoopKiller.AddToCloseList(this.mCurrentVisitingNode.InnerState);
                }
                else
                {
                    //something wrong.
                }
                this.mCurrentVisitingNode.Branch();
                List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                foreach (StateNode sn in lst_children_node_of_current_node)
                {
                    this.mLoopKiller.OfferToOpenQueue(sn);
                }
                return VisitResult.NotAGoal;
            }
        }

        //uninformed DFS
        private VisitResult DFSVisitNextNode()
        {
            this.mCurrentVisitingNode = this.mLoopKiller.PopFromOpenStack();
            if (true == this.mCurrentVisitingNode.InnerState.Goal)
            {
                //reached the goal node
                return VisitResult.Goal;
            }
            else
            {
                if (null != this.mCurrentVisitingNode)
                {
                    this.mLoopKiller.AddToCloseList(this.mCurrentVisitingNode.InnerState);
                }
                else
                {
                    //something wrong.
                }
                this.mCurrentVisitingNode.Branch();
                List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                foreach (StateNode sn in lst_children_node_of_current_node)
                {
                    this.mLoopKiller.PushToOpenStack(sn);
                }
                return VisitResult.NotAGoal;
            }
        }

        private VisitResult DFSVisitNextNode(int until_level)
        {
            this.mCurrentVisitingNode = this.mLoopKiller.PopFromOpenStack();
            if (until_level >= this.mCurrentVisitingNode.Level)
            {
                if (true == this.mCurrentVisitingNode.InnerState.Goal)
                {
                    //reached the goal node
                    return VisitResult.Goal;
                }
                else
                {
                    if (null != this.mCurrentVisitingNode)
                    {
                        this.mLoopKiller.AddToCloseList(this.mCurrentVisitingNode.InnerState);
                    }
                    else
                    {
                        //something wrong.
                    }
                    if (!this.mCurrentVisitingNode.Children.Any())
                    {
                        this.mCurrentVisitingNode.Branch();
                    }
                    List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                    foreach (StateNode sn in lst_children_node_of_current_node)
                    {
                        this.mLoopKiller.PushToOpenStack(sn);
                    }
                    return VisitResult.NotAGoal;
                }
            }
            else
            {
                return VisitResult.ExceededMaxLevel;
            }
        }

        public void FindGoal()
        {
            bool find = false;
            while(!find)
            {
                VisitResult vr = this.BFSVisitNextNode();
                if(vr == VisitResult.Goal)
                {
                    find = true;
                }
                else
                {
                    find = false;
                }
            }
        }

        public string GetSoFarTrace()
        {
            StringBuilder sb = new StringBuilder();
            StateNode cursor = this.mCurrentVisitingNode;
            while (cursor != null)
            {
                sb.Insert(0, cursor.InnerState.DestinationOfPreviousMove);
                cursor = cursor.Parent;
            }
            return sb.ToString();
        }
    }
}
