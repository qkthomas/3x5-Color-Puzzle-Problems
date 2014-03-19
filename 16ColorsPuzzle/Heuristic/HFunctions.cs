using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Heuristic
{
    class HFunctions
    {
        public static Tuple<int, int> OneDIndexToTwoDIndex(Tuple<int, int> rcconfig, int oned_index)
        {
            int how_many_row = rcconfig.Item1;
            int how_many_column = rcconfig.Item2;
            int x = oned_index / how_many_column;
            int y = oned_index % how_many_column;
            return Tuple.Create<int, int>(x, y);
        }

        public static int GetManhattanDistance(Tuple<int, int> index_a, Tuple<int, int> index_b)
        {
            int distance = Math.Abs(index_a.Item1 - index_b.Item1) + Math.Abs(index_a.Item2 - index_b.Item2);
            return distance;
        }
    }
}
