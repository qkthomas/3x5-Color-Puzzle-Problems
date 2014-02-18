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
        private ChipsList mChipsList;
        private int mCurrentEmptySpaceIndex;

        enum Directions { up, down, left, right }

        public Controller()
        {
            this.mChipsList = new ChipsList();
        }

        //better to use delegate
        public void MoveEmptySpaceInDirection(Directions direct)
        {

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
            this.mChipsList = PzzConfigFileReader.BuildChipsConfiguration(Tuple.Create<int, int>(3, 5), inputed_str);
            for (int i = 0; i < this.mChipsList.Count; i++ )
            {
                if (Color.Transparent == this.mChipsList[i].chip_color)
                {
                    this.mCurrentEmptySpaceIndex = i;
                }
            }

        }

        public void DrawConfiguration(Graphics g, Size s)
        {
            foreach (Chip cp in mChipsList)
            {
                cp.DrawMyself(g, s);
            }
        }
    }
}
