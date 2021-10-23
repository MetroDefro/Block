using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockProject
{
    class Cannon
    {
        private RectangleF src, dst;
        private Image image;
        private int count = 5;

        public float X
        {
            get { return dst.X; }
            set { dst.X = value; }
        }

        public int Count { get => count; set => count = value; }

        public Cannon()
        {
            image = Image.FromFile("../../cannon.png");
            src = new RectangleF(200, 0, image.Width / 9, image.Height);
            dst = new RectangleF(140, 500, image.Width / 9 , image.Height);
        }

        public void RotateTo(int x)
        {
            count += x;
            if (count <= 0)
                count = 0;
            else if (count >= 8)
                count = 8;
            src.X = image.Height * count;
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(image, dst, src, GraphicsUnit.Pixel);
        }

    }
}
