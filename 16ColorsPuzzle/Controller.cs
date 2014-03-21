using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace _16ColorsPuzzle
{
    class Controller
    {
        private Tuple<int, int> mRowColumnConfig;
        private ChipsList mChipsList;
        private int mCurrentEmptySpaceIndex;
        private Logger mLogger = new Logger();
        private bool mGoal = false;

        enum Directions { up, down, left, right };

        public class Logger
        {
            private List<char> mListMovements;
            private int mLogStartPosition = 1;
            private int mNewLineEveryNRecord = 5;
            private string mLogFileName = "log.txt";
            private char mFirstPositionRepresentChar = 'A';

            public Logger()
            {
                mListMovements = new List<char>();
            }

            public void AddMove(int index_in_chips_list)
            {
                int position_number_unicode = (int)this.mFirstPositionRepresentChar + index_in_chips_list;
                char move = (char)position_number_unicode;
                this.mListMovements.Add(move);
            }

            public void WriteLogToDisk()
            {
                FileStream fs = new FileStream(this.mLogFileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                StringBuilder sb = new StringBuilder();
                int number_step = 1;
                foreach(char c in this.mListMovements)
                {
                    if (0 == this.mLogStartPosition % this.mNewLineEveryNRecord)
                    {
                        sb.Append(number_step.ToString() + ")\t");
                        sb.Append(c.ToString());
                        sb.AppendLine(", ");
                    }
                    else
                    {
                        sb.Append(number_step.ToString() + ")\t");
                        sb.Append(c.ToString());
                        sb.Append(", ");
                    }
                    this.mLogStartPosition++;
                    number_step++;
                }
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        public bool Goal
        {
            get { return this.mGoal; }
        }

        public Controller(Tuple<int, int> row_column_config)
        {
            this.mRowColumnConfig = row_column_config;
            this.mChipsList = new ChipsList();
        }

        public void Reset()
        {
            this.mChipsList.Clear();
            this.mGoal = false;
            this.mLogger = new Logger();
            this.mCurrentEmptySpaceIndex = -1;  //for debug.
        }

        public void PrintLogger()
        {
            this.mLogger.WriteLogToDisk();
        }

        //better to use delegate, and also need refactoring
        public void MoveEmptySpaceUp()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex - this.mRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index)
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
                this.mLogger.AddMove(this.mCurrentEmptySpaceIndex);
                this.ReachGoal();
            }
        }

        public void MoveEmptySpaceDown()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex + this.mRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (this.mChipsList.Count > dest_swapping_index)
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
                this.mLogger.AddMove(this.mCurrentEmptySpaceIndex);
                this.ReachGoal();
            }
        }

        public void MoveEmptySpaceLeft()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex - 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index && 0 != ((this.mCurrentEmptySpaceIndex) % this.mRowColumnConfig.Item2))
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
                this.mLogger.AddMove(this.mCurrentEmptySpaceIndex);
                this.ReachGoal();
            }
        }

        public void MoveEmptySpaceRight()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex + 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (this.mChipsList.Count > dest_swapping_index && (this.mRowColumnConfig.Item2 - 1) != ((this.mCurrentEmptySpaceIndex) % this.mRowColumnConfig.Item2))
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
                this.mLogger.AddMove(this.mCurrentEmptySpaceIndex);
                this.ReachGoal();
            }
        }

        public void SwapTwoChips(int index1, int index2)
        {
            Chip chip_at_index1 = this.mChipsList[index1];
            Chip chip_at_index2 = this.mChipsList[index2];
            this.mChipsList[index1] = chip_at_index2;
            this.mChipsList[index2] = chip_at_index1;
        }

        public void ReadConfiguration(string filename)
        {
            string[] inputed_str = PzzConfigFileReader.ReadAsArray(filename);
            this.mChipsList = PzzConfigFileReader.BuildChipsConfiguration(this.mRowColumnConfig, inputed_str);
            for (int i = 0; i < this.mChipsList.Count; i++ )
            {
                if (Color.Transparent == this.mChipsList[i].mChipColor)
                {
                    this.mCurrentEmptySpaceIndex = i;
                }
            }

        }

        public void DrawConfiguration(Graphics g, Size s)
        {
            int char_base = (int)'A';
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 48);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            for (int i = 0; i < this.mChipsList.Count; i++ )
            {
                int position = i;   //positio = index.
                Brush brush = new SolidBrush(this.mChipsList[i].mChipColor);
                Pen pen = new Pen(Color.Black);
                //attain chip drawing size according to the size of the Panel.
                int column_number = position % this.mRowColumnConfig.Item2;
                int row_number = position / this.mRowColumnConfig.Item2;
                Point rtg_upperleft_point = new Point(s.Width / this.mRowColumnConfig.Item2 * column_number, s.Height / this.mRowColumnConfig.Item1 * row_number);
                Size rtg_size = new Size(s.Width / this.mRowColumnConfig.Item2, s.Height / this.mRowColumnConfig.Item1);
                Rectangle rtg = new Rectangle(rtg_upperleft_point, rtg_size);
                g.FillRectangle(brush, rtg);
                g.DrawRectangle(pen, rtg);
                if (Color.Transparent == this.mChipsList[i].mChipColor)
                {
                    g.DrawLine(pen, new Point(rtg.X, rtg.Y), new Point(rtg.X + rtg.Width, rtg.Y + rtg.Height));
                    g.DrawLine(pen, new Point(rtg.X + rtg.Width, rtg.Y), new Point(rtg.X, rtg.Y + rtg.Height));
                }

                string str_to_draw = ((char)(char_base + i)).ToString();
                g.DrawString(str_to_draw, drawFont, drawBrush, rtg);
            }
        }

        public bool ReachGoal()
        {
            int number_of_row = this.mRowColumnConfig.Item1;
            int number_of_column = this.mRowColumnConfig.Item2;
            int offset_first_row_last_row = (number_of_row - 1) * number_of_column;
            for (int i = 0; i < number_of_column; i++)
            {
                if(this.mChipsList[i] != this.mChipsList[i+offset_first_row_last_row])
                {
                    this.mGoal = false;
                    return this.mGoal;
                }
            }
            this.mGoal = true;
            return this.mGoal;
        }

        public bool isReady()
        {
            if(this.mChipsList.isEmpty())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
