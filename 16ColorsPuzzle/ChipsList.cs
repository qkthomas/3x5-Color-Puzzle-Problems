using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle
{
    class ChipsList : List<Chip>
    {
        public void SwapTwoChips(int index1, int index2)
        {
            Chip chip_at_index1 = base[index1];
            Chip chip_at_index2 = base[index2];
            base[index1] = chip_at_index2;
            base[index2] = chip_at_index1;
        }
    }
}
