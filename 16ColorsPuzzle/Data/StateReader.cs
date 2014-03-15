using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace _16ColorsPuzzle.Data
{
    class StateReader
    {
        static private Dictionary<string, Color> dict_string_color = new Dictionary<string, Color>();

        static StateReader()
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

        private static List<List<string>> ReadStringArraysFromFile(string filename)
        {
            List<List<string>> configs = new List<List<string>>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line = null;

                while ((line = sr.ReadLine())!= null)
                {
                    line = line.Trim();
                    if(line == "")
                    {
                        continue;
                    }
                    string[] lineArrary = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                    configs.Add(new List<string>(lineArrary));
                }
            }
            return configs;
        }

        public static List<State> ReadStatesFromFile(string filename)
        {
            List<State> states = new List<State>();
            List<List<string>> arrs_string = ReadStringArraysFromFile(filename);
            foreach (List<string> arr_str in arrs_string)
            {
                ChipsList cplst = new ChipsList();
                foreach (string str in arr_str)
                {
                    Color color = dict_string_color[str];
                    cplst.Add(new Chip(color));
                }
                State new_init_state = State.CreateNewStateFromChipsList(cplst);
                states.Add(new_init_state);
            }
            return states;
        }
    }
}
