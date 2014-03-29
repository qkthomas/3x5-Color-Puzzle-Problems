using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using _16ColorsPuzzle.Data;
using System.ComponentModel;
using System.Diagnostics;

namespace _16ColorsPuzzle.Heuristic
{
    class StateTreeSolver
    {
        private StateTree mST;
        private ManualResetEvent mDoneEvent;
        private BackgroundWorker mWorker;
        private int mID;
        private string mResult;
        private long mTimeInMs;

        public StateTreeSolver(StateTree st, ManualResetEvent doneEvent, BackgroundWorker worker, int id)
        {
            this.mST = st;
            this.mDoneEvent = doneEvent;
            this.mWorker = worker;
            this.mID = id;
        }

        public long TimeCost
        {
            get { return this.mTimeInMs; }
        }

        public string Result
        {
            get { return this.mResult; }
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            this.mWorker.ReportProgress(0, String.Format("Solving games No.{0}...", this.mID));
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.mST.HeuristicTraverse();
            this.mResult = this.mST.PrintResultAndReset();
            watch.Stop();
            this.mTimeInMs = watch.ElapsedMilliseconds;
            //fd.AppendLine(String.Format("Finishing solving games No.{0}: {1}ms...", i_num_of_games, time_text));
            this.mWorker.ReportProgress(0, String.Format("Finishing solving games No.{0}: {1}ms...", this.mID, this.mTimeInMs.ToString()));
            this.mDoneEvent.Set();
        }

    }
}
