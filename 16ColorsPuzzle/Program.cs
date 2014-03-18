using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using _16ColorsPuzzle.Data;
using System.Diagnostics;
using System.Text;
using System.IO;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //string filename = @"C:\Users\Q.K.Lim.Chan\SkyDrive\学业\Graduate\Courses\COMP 6721 - Introduction to A.I\Project\sample-input1.txt";
            //List<StateTree> lst_statetrees = Program.GenerateStateTreesFromFile(filename);
            //StateTree st = lst_statetrees[0];
            //SolveGame(st);

        }

        static void SortedSetTest()
        {
            SortedSet<int> ssi = new SortedSet<int>();
            for (int i = 0; i < 100; i++)
            {
                ssi.Add(i);
            }
            SortedSet<int> picked_ssi = ssi.GetViewBetween(50, 50);
            int j = picked_ssi.Min;
            ssi.Remove(j);
            j = picked_ssi.Min;
            int bp = 0;
        }

        static private List<StateTree> GenerateStateTreesFromFile(string filename)
        {
            List<State> lst_states = StateReader.ReadStatesFromFile(filename);
            List<StateTree> lst_statetrees = new List<StateTree>();
            foreach (State s in lst_states)
            {
                lst_statetrees.Add(new StateTree(s));
            }
            return lst_statetrees;
        }

        private static void SolveGame(StateTree st)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            st.HeuristicTraverse();
            string str_result = st.PrintResultAndReset();
            watch.Stop();
            string time_text = watch.ElapsedMilliseconds.ToString();
            return;
        }

        private static void SolveGames(List<StateTree> lst_statetrees)
        {
            StringBuilder sb = new StringBuilder();
            int total_number_of_moves = 0;
            long total_time = 0;
            int i_num_of_games = 1;
            foreach (StateTree st in lst_statetrees)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                st.HeuristicTraverse();
                string str_result = st.PrintResultAndReset();
                watch.Stop();
                string time_text = watch.ElapsedMilliseconds.ToString();
                total_number_of_moves += (str_result.Length - 1);
                total_time += watch.ElapsedMilliseconds;
                sb.AppendLine(str_result);
                sb.AppendLine(time_text + "ms");
                i_num_of_games++;
            }
            sb.AppendLine(total_number_of_moves.ToString() + "moves");
            sb.AppendLine(total_time + "ms");
            TextWriter writer = new StreamWriter("Result.txt");
            writer.Write(sb.ToString());
            writer.Close();
        }
    }
}