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

        public enum VisitResult { VisitedNodeGoal, VisitedNodeNotAGoal, VisitedNodeExceededMaxLevel}
        private StateNode mRootNode;
        private StateNode mCurrentVisitingNode = null;
        private LoopKiller mLoopKiller;         //but closelist will be useless for this class.

        public StateTree(State root_node_state)
        {
            this.mLoopKiller = new LoopKiller();
            this.mRootNode = StateNode.CreateNewRootNode(root_node_state, this.mLoopKiller);
            this.mLoopKiller.PushToOpenStack(this.mRootNode);
        }

        //Iterative deepening depth-first search
        public void IDDFSTraverse()
        {
            int max_search_level = 1;
            while(true)
            {
                VisitResult result = this.IIDFSVisitNextNode(max_search_level);
                if(VisitResult.VisitedNodeGoal == result)
                {
                    break;
                }
                else if(VisitResult.VisitedNodeNotAGoal == result)
                {
                    //ignore
                }
                else// (VisitResult.VisitedNodeExceededMaxLevel == result)
                {
                    this.ResetTree();
                    max_search_level++;
                    continue;
                }
            }

        }

        private VisitResult IIDFSVisitNextNode(int until_level)
        {
            this.mCurrentVisitingNode = this.mLoopKiller.PopFromOpenStack();
            if (null == this.mCurrentVisitingNode)
            {
                return VisitResult.VisitedNodeExceededMaxLevel;
            }
            else
            {
                if (this.mCurrentVisitingNode.Level > until_level)
                {
                    //something wrong
                }
                smVisitedNodesCount++;
                this.mCurrentVisitingNode.ToBeVisited();
                if (true == this.mCurrentVisitingNode.InnerState.Goal)
                {
                    //reached the goal node
                    return VisitResult.VisitedNodeGoal;
                }
                else
                {
                    if (!this.mCurrentVisitingNode.Children.Any())
                    {
                        this.mCurrentVisitingNode.Branch();
                    }
                    else
                    {
                        int bp = 0;
                        //something wrong;
                    }
                    if (until_level > this.mCurrentVisitingNode.Level)
                    {
                        List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                        foreach (StateNode sn in lst_children_node_of_current_node)
                        {
                            this.mLoopKiller.PushToOpenStack(sn);
                        }
                    }
                    return VisitResult.VisitedNodeNotAGoal;
                }
            }
            #region deprecated code
            /*
            if (until_level >= this.mCurrentVisitingNode.Level)
            {
                smVisitedNodesCount++;
                this.mCurrentVisitingNode.ToBeVisited();
                if (true == this.mCurrentVisitingNode.InnerState.Goal)
                {
                    //reached the goal node
                    return VisitResult.VisitedNodeGoal;
                }
                else
                {
                    if (!this.mCurrentVisitingNode.Children.Any())
                    {
                        this.mCurrentVisitingNode.Branch();
                    }
                    else
                    {
                        int bp = 0;
                        //something wrong;
                    }
                    if (until_level > this.mCurrentVisitingNode.Level)
                    {
                        List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                        foreach (StateNode sn in lst_children_node_of_current_node)
                        {
                            this.mLoopKiller.PushToOpenStack(sn);
                        } 
                    }
                    return VisitResult.VisitedNodeNotAGoal;
                }
            }
            else
            {
                //this.mLoopKiller.PushToOpenStack(this.mCurrentVisitingNode);
                return VisitResult.VisitedNodeExceededMaxLevel;
            }
             * */
            #endregion
        }

        private void ResetTree()
        {
            this.mCurrentVisitingNode = null;
            this.mLoopKiller.Reset();
            //this.mRootNode = StateNode.CreateNewRootNode(this.mRootNode.InnerState, this.mLoopKiller);
            this.mLoopKiller.PushToOpenStack(this.mRootNode);
        }

        public void BFSTraverse()
        {
            bool find = false;
            while (!find)
            {
                VisitResult vr = this.BFSVisitNextNode();
                if (vr == VisitResult.VisitedNodeGoal)
                {
                    find = true;
                }
                else
                {
                    find = false;
                }
            }
        }

        //uninformed BFS
        private VisitResult BFSVisitNextNode()
        {
            smVisitedNodesCount++;
            this.mCurrentVisitingNode = this.mLoopKiller.PollFromOpenQueue();
            this.mCurrentVisitingNode.ToBeVisited();
            if (true == this.mCurrentVisitingNode.InnerState.Goal)
            {
                //reached the goal node
                return VisitResult.VisitedNodeGoal;
            }
            else
            {
                this.mCurrentVisitingNode.Branch();
                List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                foreach (StateNode sn in lst_children_node_of_current_node)
                {
                    this.mLoopKiller.OfferToOpenQueue(sn);
                }
                return VisitResult.VisitedNodeNotAGoal;
            }
        }

        public void DFSTraverse()
        {
            bool find = false;
            while (!find)
            {
                VisitResult vr = this.DFSVisitNextNode();
                if (vr == VisitResult.VisitedNodeGoal)
                {
                    find = true;
                }
                else
                {
                    find = false;
                }
            }
        }

        //uninformed DFS
        private VisitResult DFSVisitNextNode()
        {
            this.mCurrentVisitingNode = this.mLoopKiller.PopFromOpenStack();
            this.mCurrentVisitingNode.ToBeVisited();
            if (true == this.mCurrentVisitingNode.InnerState.Goal)
            {
                //reached the goal node
                return VisitResult.VisitedNodeGoal;
            }
            else
            {
                this.mCurrentVisitingNode.Branch();
                List<StateNode> lst_children_node_of_current_node = this.mCurrentVisitingNode.Children;
                foreach (StateNode sn in lst_children_node_of_current_node)
                {
                    this.mLoopKiller.PushToOpenStack(sn);
                }
                return VisitResult.VisitedNodeNotAGoal;
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
