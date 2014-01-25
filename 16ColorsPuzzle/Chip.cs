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
        private Tuple<int, char> position_by_number_and_letter;

        //record the grid config for self-drawing
        private Tuple<int, int> row_column_config;

        public Chip(Color IN_color, Tuple<int, char> IN_position, Tuple<int, int> IN_grid_config)
        {
            this.chip_color = IN_color;
            this.position_by_number_and_letter = IN_position;
            this.row_column_config = IN_grid_config;
        }

        public void DrawMyself(Panel p)
        {
            using (Graphics g = p.CreateGraphics())
            {
                Brush brush = new SolidBrush(this.chip_color);
                Pen pen = new Pen(Color.Black);
                //attain chip drawing size according to the size of the Panel.
                int column_number = this.position_by_number_and_letter.Item1 % this.row_column_config.Item2;
                int row_number = this.position_by_number_and_letter.Item1 / this.row_column_config.Item2;
                Point rtg_upperleft_point = new Point(p.Size.Width / this.row_column_config.Item2 * column_number, p.Size.Height / this.row_column_config.Item1 * row_number);
                Size rtg_size = new Size(p.Size.Width / this.row_column_config.Item2, p.Size.Height / this.row_column_config.Item1);
                Rectangle rtg = new Rectangle(rtg_upperleft_point, rtg_size);
                g.FillRectangle(brush, rtg);
                g.DrawRectangle(pen, rtg);
                if(Color.Transparent == this.chip_color)
                {
                    g.DrawLine(pen, new Point(rtg.X, rtg.Y), new Point(rtg.X + rtg.Width, rtg.Y + rtg.Height));
                    g.DrawLine(pen, new Point(rtg.X + rtg.Width, rtg.Y), new Point(rtg.X, rtg.Y + rtg.Height));
                }
            }
        }
    }
}
