using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _16ColorsPuzzle.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace _16ColorsPuzzle
{
    public partial class Form1 : Form
    {
        private Form_Debug fd;
        private Controller mController;
        private BackgroundWorker worker = new BackgroundWorker();

        private void InitializeWorker()
        {
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;

            this.worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<StateTree> lst_statetrees = e.Argument as List<StateTree>;
            Thread.Sleep(1000);
            this.SolveGames(sender, lst_statetrees);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string text = e.UserState as string;
            this.fd.AppendLine(text);
        }

        public Form1()
        {
            InitializeWorker();
            InitializeComponent();
            this.fd = new Form_Debug();
            fd.Show();
            this.mController = new Controller(Tuple.Create<int, int>(3, 5));    //the board is set 3 by 5
            this.panel1.Paint += this.drawChipsConfiguration;
        }

        private void drawChipsConfiguration(object sender, PaintEventArgs e)
        {
            this.mController.DrawConfiguration(e.Graphics, this.panel1.Size);
        }

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Please select a configuration text file";
            ofd.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                string filename = ofd.FileName;
                this.fd.AppendLine(filename);
                this.mController = new Controller(Tuple.Create<int, int>(3, 5));    //the board is set 3 by 5
                this.mController.ReadConfiguration(filename);
                this.panel1.Refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (false == this.mController.Goal && true == this.mController.isReady())
            {
                if (e.KeyCode == Keys.Up)
                {
                    //we visually want to move the non-empty tile
                    this.mController.MoveEmptySpaceDown();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.mController.MoveEmptySpaceUp();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    this.mController.MoveEmptySpaceRight();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.mController.MoveEmptySpaceLeft();
                }
                else
                {
                    //do nothing
                }
                if (true == this.mController.Goal)
                {
                    this.panel1.Refresh();      //this is for display the goaled board.
                    MessageBox.Show("Reach goal state. Reset the game");
                    this.mController.PrintLogger();
                    this.mController.Reset();
                }
                else
                {
                    this.panel1.Refresh();
                }
            }
            else
            {
                //do nothing
            }
        }

        private void Reset()
        {
            this.mController.Reset();
            this.panel1.Refresh();      //this is for display the reset board.
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        private void autoSolveGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Please select a configuration text file";
            ofd.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                string filename = ofd.FileName;
                fd.AppendLine(filename);
                List<StateTree> lst_statetrees = this.GenerateStateTreesFromFile(filename);
                if (false == this.worker.IsBusy)
                {
                    this.worker.RunWorkerAsync(lst_statetrees);
                }
                else
                {
                    MessageBox.Show("The program is busy solving games.");
                }
                //this.SolveGames(lst_statetrees);
            }
        }

        private List<StateTree> GenerateStateTreesFromFile(string filename)
        {
            List<State> lst_states = StateReader.ReadStatesFromFile(filename);
            List<StateTree> lst_statetrees = new List<StateTree>();
            foreach (State s in lst_states)
            {
                lst_statetrees.Add(new StateTree(s));
            }
            return lst_statetrees;
        }

        private void SolveGames(object sender, List<StateTree> lst_statetrees)
        {
            BackgroundWorker the_worker = sender as BackgroundWorker;
            StringBuilder sb = new StringBuilder();
            int total_number_of_moves = 0;
            long total_time = 0;
            //this.fd.AppendLine("Start solving games...");
            the_worker.ReportProgress(0, "Start solving games...");
            int i_num_of_games = 1;
            foreach (StateTree st in lst_statetrees)
            {
                //fd.AppendLine(String.Format("Solving games No.{0}...", i_num_of_games));
                the_worker.ReportProgress(0, String.Format("Solving games No.{0}...", i_num_of_games));
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
                //fd.AppendLine(String.Format("Finishing solving games No.{0}: {1}ms...", i_num_of_games, time_text));
                the_worker.ReportProgress(0, String.Format("Finishing solving games No.{0}: {1}ms...", i_num_of_games, time_text));
                i_num_of_games++;
            }
            //this.fd.AppendLine(String.Format("All games are solved, using {0}ms", total_time));
            the_worker.ReportProgress(0, String.Format("All games are solved, using {0}ms", total_time));
            //this.fd.AppendLine(String.Format("Writing Result.txt"));
            the_worker.ReportProgress(0, String.Format("Writing Result.txt"));
            sb.AppendLine(total_number_of_moves.ToString() + "moves");
            sb.AppendLine(total_time + "ms");
            TextWriter writer = new StreamWriter("Result.txt");
            writer.Write(sb.ToString());
            writer.Close();
            //this.fd.AppendLine(String.Format("Done!"));
            the_worker.ReportProgress(0, String.Format("Done!"));
        }
    }
}