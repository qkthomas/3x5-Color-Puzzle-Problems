using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using _16ColorsPuzzle.Data;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace _16ColorsPuzzle
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void SortedSetTest()
        {
            SortedSet<int> ssi = new SortedSet<int>();
            for (int i = 0; i < 100; i++)
            {
                ssi.Add(i);
            }
            SortedSet<int> picked_ssi = ssi.GetViewBetween(50, 50);
            int j = picked_ssi.Min;
            ssi.Remove(j);
            j = picked_ssi.Min;
            int bp = 0;
        }
    }
}
