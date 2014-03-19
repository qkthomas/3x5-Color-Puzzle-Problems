using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Data;

namespace _16ColorsPuzzle.Moving
{
    static class MoveController
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

        //better to use delegate, and also need refactoring
        public static void MoveEmptySpaceUp(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex - State.smRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index)
            {
                SwapTwoChips(s, s.CurrentEmptySpaceIndex, dest_swapping_index);
                s.CurrentEmptySpaceIndex = dest_swapping_index;
                s.DestinationOfPreviousMove = (char)((int)State.smIndexBase + dest_swapping_index);
            }
        }

        public static bool EmptySpaceUpMovable(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex - State.smRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void MoveEmptySpaceDown(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex + State.smRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (s.InnerChipsList.Count > dest_swapping_index)
            {
                SwapTwoChips(s, s.CurrentEmptySpaceIndex, dest_swapping_index);
                s.CurrentEmptySpaceIndex = dest_swapping_index;
                s.DestinationOfPreviousMove = (char)((int)State.smIndexBase + dest_swapping_index);
            }
        }

        public static bool EmptySpaceDownMovable(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex + State.smRowColumnConfig.Item2;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (s.InnerChipsList.Count > dest_swapping_index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void MoveEmptySpaceLeft(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex - 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index && 0 != ((s.CurrentEmptySpaceIndex) % State.smRowColumnConfig.Item2))
            {
                SwapTwoChips(s, s.CurrentEmptySpaceIndex, dest_swapping_index);
                s.CurrentEmptySpaceIndex = dest_swapping_index;
                s.DestinationOfPreviousMove = (char)((int)State.smIndexBase + dest_swapping_index);
            }
        }

        public static bool EmptySpaceLeftMovable(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex - 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (0 <= dest_swapping_index && 0 != ((s.CurrentEmptySpaceIndex) % State.smRowColumnConfig.Item2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void MoveEmptySpaceRight(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex + 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (s.InnerChipsList.Count > dest_swapping_index && (State.smRowColumnConfig.Item2 - 1) != ((s.CurrentEmptySpaceIndex) % State.smRowColumnConfig.Item2))
            {
                SwapTwoChips(s, s.CurrentEmptySpaceIndex, dest_swapping_index);
                s.CurrentEmptySpaceIndex = dest_swapping_index;
                s.DestinationOfPreviousMove = (char)((int)State.smIndexBase + dest_swapping_index);
            }
        }

        public static bool EmptySpaceRightMovable(State s)
        {
            int dest_swapping_index = s.CurrentEmptySpaceIndex + 1;   //Item2 is the number of column
            //do the swapping only when the dest_swapping_index is within range.
            if (s.InnerChipsList.Count > dest_swapping_index && (State.smRowColumnConfig.Item2 - 1) != ((s.CurrentEmptySpaceIndex) % State.smRowColumnConfig.Item2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SwapTwoChips(State s, int index1, int index2)
        {
            Chip chip_at_index1 = s.InnerChipsList[index1];
            Chip chip_at_index2 = s.InnerChipsList[index2];
            s.InnerChipsList[index1] = chip_at_index2;
            s.InnerChipsList[index2] = chip_at_index1;
        }
    }
}
