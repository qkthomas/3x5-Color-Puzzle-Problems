using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace _16ColorsPuzzle
{
    class Chip
    {
        private Color chip_color;
        private Tuple<int, int, char> chip_position_x_y_char;

        //record the grid config for self-drawing
        private Tuple<int, int> grid_m_n_config;

        private int edge;

        public Chip(Color IN_color, Tuple<int, int, char> IN_position, int IN_edge, Tuple<int, int> IN_grid_config)
        {
            this.chip_color = IN_color;
            this.chip_position_x_y_char = IN_position;
            this.edge = IN_edge;
            this.grid_m_n_config = IN_grid_config;
        }

        public void DrawMyself(Panel p)
        {
            using (Graphics g = p.CreateGraphics())
            {
                Brush brush = new SolidBrush(this.chip_color);
                //attain chip drawing size according to the size of the Panel.
                Rectangle rtg = new Rectangle(new Point(p.Size.Height / grid_m_n_config.Item1 * chip_position_x_y_char.Item1, p.Size.Width / grid_m_n_config.Item2 * chip_position_x_y_char.Item2), new Size(p.Size.Height / grid_m_n_config.Item1, p.Size.Width / grid_m_n_config.Item2));
                g.FillRectangle(brush, rtg);
            }

        }
    }
}
