using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockProject
{
    class Block
    {
        private int bLevel = -1;
        Random r = new Random();
        private int nBlocks = 5;
        private int[] rockBlock = new int[100];
        private Rectangle[] blocks = new Rectangle[100];  // 블록 rectagle 배열
        private Rectangle[] hardBlocks = new Rectangle[100];  // 블록 rectagle 배열
        private bool[] visible = new bool[100];
        Brush blockColor = new SolidBrush(Color.Magenta);
        Brush HardBlockColor = new SolidBrush(Color.Black);
        private int dieBlocks = 0;
        //private int level = 5;

        public bool[] Visible { get => visible; set => visible = value; }
        public Rectangle[] Blocks { get => blocks; set => blocks = value; }
        public Rectangle[] HardBlocks { get => hardBlocks; set => hardBlocks = value; }
        public int NBlocks { get => nBlocks; set => nBlocks = value; }

        public Block(int n)
        {
            if (n > 10)
                n -= 10;
            bLevel += n;
            nBlocks += n * 5;
            for (int i = 0; i < nBlocks; i++)
                Visible[i] = true;
            for (int i = 0; i < nBlocks; i++)
            {
                if(r.Next(1, 20) > bLevel)
                {
                    Blocks[i] = new Rectangle(i % 10 * 30, 30 + 20 * (i / 10), 29, 19);
                }
                else
                {
                    HardBlocks[i] = new Rectangle(i % 10 * 30, 30 + 20 * (i / 10), 29, 19);
                    rockBlock[i] = 2;
                }

            }
 
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < nBlocks; i++)
                if (Visible[i])
                    g.FillRectangle(blockColor, Blocks[i]);
            for (int i = 0; i < nBlocks; i++)
                if (Visible[i])
                    g.FillRectangle(HardBlockColor, HardBlocks[i]);

        }

        public bool DeleteBlock(Ball ball)
        {
            for (int i = 0; i < nBlocks; i++)
            {
                if (visible[i] && ball.IntersectsWith(blocks[i]))
                {
                    visible[i] = false;
                    ball.VDir = -ball.VDir;
                    ++dieBlocks;
                    if (dieBlocks >= nBlocks)
                        return true;
                    else
                        return false;
                }
                
                else if (visible[i] && ball.IntersectsWith(hardBlocks[i]))
                {

                    ball.VDir = -ball.VDir;
                    --rockBlock[i];
                    if(rockBlock[i] <= 0)
                    {
                        visible[i] = false;
                        ++dieBlocks;
                        if (dieBlocks >= nBlocks)
                            return true;
                        else
                            return false;
                    }

                }
                
            }
            return false;
        }
        public bool DeleteBlock(Missile missile)
        {
            for (int i = 0; i < nBlocks; i++)
            {
                if (visible[i] && missile.IntersectsWith(blocks[i]))
                {
                    visible[i] = false;
                    missile.VDir = -missile.VDir;
                    ++dieBlocks;
                    if (dieBlocks >= nBlocks)
                        return true;
                    else
                        return false;
                }

                else if (visible[i] && missile.IntersectsWith(hardBlocks[i]))
                {

                    missile.VDir = -missile.VDir;
                    --rockBlock[i];
                    if (rockBlock[i] <= 0)
                    {
                        visible[i] = false;
                        ++dieBlocks;
                        if (dieBlocks >= nBlocks)
                            return true;
                        else
                            return false;
                    }

                }

            }
            return false;
        }
    }
}
