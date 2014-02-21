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
        static private Dictionary<string, Color> dict_string_color = new Dictionary<string, Color>();

        static PzzConfigFileReader()
        {
            //hardcoded defined colors. It should be done better.
            dict_string_color.Add("r", Color.Red);
            dict_string_color.Add("b", Color.Blue);
            dict_string_color.Add("w", Color.White);
            dict_string_color.Add("y", Color.Yellow);
            dict_string_color.Add("g", Color.Green);
            dict_string_color.Add("p", Color.Pink);
            dict_string_color.Add("e", Color.Transparent);
        }

        public static string[] ReadAsArray(string filename)
        {
            string[] lineArrary = null;
            using (StreamReader sr = new StreamReader(filename))
            {
                string line = null;

                if((line = sr.ReadLine()) != null)
                {
                    lineArrary = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                }
            }
            return lineArrary;
        }

        private static char ConvertIndexOfArrayToPositionOfBoard(int IN_position_in_array)
        {
            char position_representing_char = (char)(IN_position_in_array + 0x0041); //0x0041 refer to 'A' in Unicode
            return position_representing_char;
        }

        public static ChipsList BuildChipsConfiguration(Tuple<int, int> IN_row_column_config, string[] IN_inputed_str_array)
        {
            ChipsList cplst = new ChipsList();
            List<string> strlst = IN_inputed_str_array.OfType<string>().ToList();
            foreach (var str in IN_inputed_str_array)
            {
                Color color = dict_string_color[str];
                cplst.Add(new Chip(color));
            }
            return cplst;
        }
    }
}
