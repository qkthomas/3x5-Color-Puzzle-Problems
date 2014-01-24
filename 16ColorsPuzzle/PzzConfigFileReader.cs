using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace _16ColorsPuzzle
{
    class PzzConfigFileReader
    {
        private Dictionary<string, Color> dict_string_color = new Dictionary<string, Color>();

        public PzzConfigFileReader()
        {
            //hardcoded defined colors. It should be done better.
            this.dict_string_color.Add("r", Color.Red);
            this.dict_string_color.Add("b", Color.Blue);
            this.dict_string_color.Add("w", Color.White);
            this.dict_string_color.Add("y", Color.Yellow);
            this.dict_string_color.Add("g", Color.Green);
            this.dict_string_color.Add("p", Color.Pink);
            this.dict_string_color.Add("e", Color.Transparent);
        }

        static string[] ReadAsArray(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line = null;

                if((line = sr.ReadLine()) != null)
                {
                    string[] lineArrary = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                }
            }
            return null;
        }

        static Tuple<int, int> ConvertIndexOfArrayToIndexOfMatrix(int IN_position_in_array, int IN_length_of_a_row)
        {
            int row_number = IN_position_in_array / IN_length_of_a_row;
            int column_number = IN_position_in_array % IN_length_of_a_row;
            return Tuple.Create<int, int>(item1 : row_number, item2 : column_number);
        }
    }
}
