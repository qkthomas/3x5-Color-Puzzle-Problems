using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _16ColorsPuzzle
{
    class Controller
    {
        private Tuple<int, int> mRowColumnConfig;
        private ChipsList mChipsList;
        private int mCurrentEmptySpaceIndex;

        enum Directions { up, down, left, right }

        public Controller(Tuple<int, int> row_column_config)
        {
            this.mRowColumnConfig = row_column_config;
            this.mChipsList = new ChipsList();
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
            }
        }

        public void MoveEmptySpaceLeft()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex - 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index)
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
            }
        }

        public void MoveEmptySpaceRight()
        {
            int dest_swapping_index = this.mCurrentEmptySpaceIndex + 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (this.mChipsList.Count > dest_swapping_index)
            {
                this.SwapTwoChips(this.mCurrentEmptySpaceIndex, dest_swapping_index);
                this.mCurrentEmptySpaceIndex = dest_swapping_index;
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
            }
        }
    }
}
