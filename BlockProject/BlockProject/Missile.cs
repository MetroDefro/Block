using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockProject
{
    class Missile
    {
        private Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        Brush ballColor = new SolidBrush(Color.Blue);
        public int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }
        public int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }

        public double Slope
        {
            get { return slope; }
            set { slope = value; }
        }
        public double VDir
        {
            get { return vDir; }
            set { vDir = value; }
        }

        private double slope = 0;
        private double vDir = -1;

        public Missile(double n)
        {
            rect = new Rectangle(160, 530, 10, 10);
            slope = n;
        }
        public void Delete()
        {
            rect.X = 400;
            rect.Y = 800;
        }
        public void Move()
        {
            rect.X += (int)(10 / slope);
            rect.Y += (int)(slope * vDir * (10 / slope));
        }
        public void Draw(Graphics g)
        {
            g.FillRectangle(ballColor, rect);
        }

        public bool IntersectsWith(Rectangle block)
        {
            return rect.IntersectsWith(block);
        }

    }
   
}
