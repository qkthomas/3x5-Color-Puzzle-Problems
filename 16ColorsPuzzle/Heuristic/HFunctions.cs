using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Data;

namespace _16ColorsPuzzle.Heuristic
{
    class HFunctions
    {
        public static int MidRowManhattan(State s)      //not admissable
        {
            int current_heuristic_value = 0;
            int number_of_asymmetry_pairs = 0;

            int number_of_row = State.smRowColumnConfig.Item1;
            int number_of_column = State.smRowColumnConfig.Item2;
            int offset_first_row_last_row = (number_of_row - 1) * number_of_column;

            int first_index_mid_row = ((number_of_row - 1) / 2) * number_of_column;
            int last_index_mid_row = first_index_mid_row + number_of_column - 1;

            //SortedSet<int> records_used_indexed_for_manhattan_dist = new SortedSet<int>();

            for (int i = 0; i < number_of_column; i++)
            {
                int index_chip_top_row = i;
                int index_chip_bottom_row = i + offset_first_row_last_row;

                if (s.InnerChipsList[index_chip_top_row] != s.InnerChipsList[index_chip_bottom_row])
                {
                    number_of_asymmetry_pairs++;

                    int smallest_dist_of_bottomOrtop_mid_chips = int.MaxValue;

                    for (int j = first_index_mid_row; j <= last_index_mid_row; j++ )
                    {
                        int index_chip_mid_row = j;

                        if (s.InnerChipsList[index_chip_top_row] == s.InnerChipsList[index_chip_mid_row])
                        {
                            //calculate bottom_mid distance when top == mid
                            int current_dist_of_bottom_mid_chips = HFunctions.GetManhattanDistanceFromOneDIndex(State.smRowColumnConfig, index_chip_bottom_row, index_chip_mid_row);
                            if (current_dist_of_bottom_mid_chips < smallest_dist_of_bottomOrtop_mid_chips)
                            {
                                smallest_dist_of_bottomOrtop_mid_chips = current_dist_of_bottom_mid_chips;
                            }
                        }

                        if (s.InnerChipsList[index_chip_bottom_row] == s.InnerChipsList[index_chip_mid_row])
                        {
                            //calculate top_mid distance when bottom == mid
                            int current_dist_of_top_mid_chips = HFunctions.GetManhattanDistanceFromOneDIndex(State.smRowColumnConfig, index_chip_top_row, index_chip_mid_row);
                            if (current_dist_of_top_mid_chips < smallest_dist_of_bottomOrtop_mid_chips)
                            {
                                smallest_dist_of_bottomOrtop_mid_chips = current_dist_of_top_mid_chips;
                            }
                        }
                    }

                    if (smallest_dist_of_bottomOrtop_mid_chips < int.MaxValue)
                    {
                        current_heuristic_value += (smallest_dist_of_bottomOrtop_mid_chips);
                    }
                    else
                    {
                        current_heuristic_value += 4;       //which number is appropriate?
                    }
                }
            }
            int final_heuristic_value = (int)(((double)number_of_asymmetry_pairs / (double)State.smRowColumnConfig.Item2) * (double)current_heuristic_value);

            return final_heuristic_value;
        }

        public static int MissPlacedChips(State s)
        {
            int number_of_asymmetry_pairs = 0;

            int number_of_row = State.smRowColumnConfig.Item1;
            int number_of_column = State.smRowColumnConfig.Item2;
            int offset_first_row_last_row = (number_of_row - 1) * number_of_column;
            for (int i = 0; i < number_of_column; i++)
            {
                if (s.InnerChipsList[i] != s.InnerChipsList[i + offset_first_row_last_row])
                {
                    number_of_asymmetry_pairs++;
                }
            }
            return number_of_asymmetry_pairs;
        }

        private static bool IsInTheSameRow(Tuple<int, int> rcconfig, int index_a, int index_b)
        {
            int number_of_columns = rcconfig.Item2;

            if ((index_a / number_of_columns) == (index_b / number_of_columns))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsInTheSameColumn(Tuple<int, int> rcconfig, int index_a, int index_b)
        {
            int number_of_columns = rcconfig.Item2;

            if ((index_a % number_of_columns) == (index_b % number_of_columns))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private static int GetManhattanDistanceFromOneDIndex(Tuple<int, int> rcconfig, int index_a, int index_b)
        {
            Tuple<int, int> twod_index_a = HFunctions.OneDIndexToTwoDIndex(rcconfig, index_a);
            Tuple<int, int> twod_index_b = HFunctions.OneDIndexToTwoDIndex(rcconfig, index_b);
            int manhattan_dist = HFunctions.GetManhattanDistanceFromTwoDIndex(twod_index_a, twod_index_b);
            return manhattan_dist;
        }

        private static Tuple<int, int> OneDIndexToTwoDIndex(Tuple<int, int> rcconfig, int oned_index)
        {
            if (
                (oned_index < (rcconfig.Item1 * rcconfig.Item2)) &&
                (oned_index >= 0)
                )
            {
                int how_many_row = rcconfig.Item1;
                int how_many_column = rcconfig.Item2;
                int x = oned_index / how_many_column;
                int y = oned_index % how_many_column;
                return Tuple.Create<int, int>(x, y);
            }
            else
            {
                throw new Exception("Wrong index detected in OneDIndexToTwoDIndex()");
            }
        }

        private static int GetManhattanDistanceFromTwoDIndex(Tuple<int, int> index_a, Tuple<int, int> index_b)
        {
            int distance = Math.Abs(index_a.Item1 - index_b.Item1) + Math.Abs(index_a.Item2 - index_b.Item2);
            return distance;
        }
    }
}
