using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockProject
{
    class Racket
    {
        private Rectangle rect;
        Brush racketColor = new SolidBrush(Color.Red);

        private int x;
        private int width = 105;

        public int X 
        { 
            get { return x; } 
            set { x = value; } 
        }

        public Racket(int n)
        {
            if(width > 80)
                width -= n * 5;
            rect = new Rectangle(115, 500, width, 10);
        }

        public void Delete()
        {
            rect.X = 400;
            rect.Y = 800;
        }

        public void MoveTo(int x)
        {
            rect.X += x;
            if (rect.X < 0)
                rect.X = 0;
            if (rect.X > 230)
                rect.X = 230;
        }
        public void Draw(Graphics g)
        {
            g.FillRectangle(racketColor, rect);
        }

        public bool IntersectsWith(Rectangle ball)
        {
            return rect.IntersectsWith(ball);
        }

    }
}
