using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using _16ColorsPuzzle.Data;
using System.Diagnostics;

namespace _16ColorsPuzzle
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<State> lst_states = StateReader.ReadStatesFromFile(@"C:\Users\Q.K.Lim.Chan\SkyDrive\学业\Graduate\Courses\COMP 6721 - Introduction to A.I\Project\sample-input1.txt");
            List<StateTree> lst_statetrees = new List<StateTree>();
            foreach (State s in lst_states)
            {
                lst_statetrees.Add(new StateTree(s));
            }
            StateTree st = lst_statetrees[4];
            Stopwatch watch = new Stopwatch();
            watch.Start();
            st.IDDFSTraverse();
            string str = st.GetSoFarTrace();
            watch.Stop();
            string text = watch.ElapsedMilliseconds.ToString();
            int count = StateTree.smVisitedNodesCount;
            int bp1 = 0;
            
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
             * */
        }
    }
}
