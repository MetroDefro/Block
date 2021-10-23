using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockProject
{
    public partial class BlockBreaker : Form
    {
        private Racket racket;
        private Ball ball;
        private Block block;

        private Cannon cannon;
        private Missile missile;
        private int level = 1;

        private bool isMissile = false;
        private int slope = 0;

        public BlockBreaker()
        {
            InitializeComponent();

            racket = new Racket(level);
            ball = new Ball();
            block = new Block(level);
            cannon = new Cannon();
            cannon.X = 500;
            missile = new Missile(cannon.Count);
            missile.X = 500;
            timer1.Interval = 30;
        }

        private void BlockBreaker_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.stageLabel.Text = " " +level;
            racket.Draw(e.Graphics);
            ball.Draw(e.Graphics);
            block.Draw(e.Graphics);
            cannon.Draw(e.Graphics);
            missile.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(level <= 10)
            {
                ball.Move();
                if (ball.IsGameOver())
                {
                    this.gameOver.Text = "게임오버";
                }

                if (ball.X < 0 || ball.X > 300)
                {
                    ball.Slope = -ball.Slope;
                }
                if (ball.Y < 0 || racket.IntersectsWith(ball.Rect))
                {
                    ball.VDir = -ball.VDir;
                }
                if (block.DeleteBlock(ball))
                {
                    this.gameOver.Text = "게임클리어";
                    timer1.Stop();
                    newLevelStart();
                }
            }else
            {
                if (isMissile)
                {
                    missile.Move();
                    
                    if (missile.X < 0 || missile.X > 300)
                    {
                        missile.Slope = -missile.Slope;
                    }
                    if (missile.Y < 0)
                    {
                        missile.VDir = -missile.VDir;
                    }
                    if (block.DeleteBlock(missile))
                    {
                        this.gameOver.Text = "게임클리어";
                        timer1.Stop();
                        newLevelStart();
                    }
                    if (missile.Y > 600)
                    {
                        missile.X = 160;
                        missile.Y = 530;
                        isMissile = false;
                    }
                    
                }
            }

            panel1.Invalidate();
        }
        private void newLevelStart()
        {
            if (level < 10)
            {
                ++level;

                racket = new Racket(level);
                block = new Block(level);

                this.gameOver.Text = " ";
                this.stageLabel.Text = " " + level;
                timer1.Start();
            }
            else if (level == 10)
            {
                ++level;
                racket.Delete();
                ball.Delete();

                cannon = new Cannon();
                block = new Block(level);
                this.gameOver.Text = " ";
                this.stageLabel.Text = " " + level;
                timer1.Start();
            }
            else if (level < 20)
            {
                ++level;

                block = new Block(level);
                this.gameOver.Text = " ";
                this.stageLabel.Text = " " + level;
                timer1.Start();
            }
            else
            {
                return;
            }

        }

        private void BlockBreaker_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if(level <= 10)
                        racket.MoveTo(-20);
                    else
                    {
                        cannon.RotateTo(-1);
                        slope++;
                        if (slope >= 4)
                            slope = 4;
                    }                 
                    break;
                case Keys.Right:
                    if (level <= 10)
                        racket.MoveTo(20);
                    else
                    {
                        cannon.RotateTo(1);
                        slope--;
                        if (slope <= -4)
                            slope = -4;
                    }
                    break;
                case Keys.Space:
                    if(level > 10)
                    {
                        switch (cannon.Count)
                        {
                            case 8:
                                missile = new Missile(0.2);
                                break;
                            case 7:
                                missile = new Missile(0.6);
                                break;
                            case 6:
                                missile = new Missile(1.2);
                                break;
                            case 5:
                                missile = new Missile(2);
                                break;
                            case 4:
                                missile = new Missile(100);
                                break;
                            case 3:
                                missile = new Missile(-2);
                                break;
                            case 2:
                                missile = new Missile(-1.2);
                                break;
                            case 1:
                                missile = new Missile(-0.6);
                                break;
                            case 0:
                                missile = new Missile(-0.2);
                                break;
                        }
                        isMissile = true;
                    }
                    break;
            }
        }
    }
}
