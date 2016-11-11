using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Grid mGrid = new Grid();
        ActiveGameObject l = new GameT();
        ActiveGameObject lNext = new GameL1();
        int mnSpeed = 0;
        int mnScroe = 0;
        bool mbPaused = false;
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();

        }

        private ActiveGameObject RandomCreate()
        {
            ActiveGameObject ltemp;
            long nR = Math.Abs(DateTime.Now.ToBinary() % 7);
            if (nR == 0)
                ltemp = new GameT();
            else if (nR == 1)
                ltemp = new GameL1();
            else if (nR == 2)
                ltemp = new GameL2();
            else if (nR == 3)
                ltemp = new GameZ1();
            else if (nR == 4)
                ltemp = new GameZ2();
            else if (nR == 5)
                ltemp = new GameLine();
            else
                ltemp = new GameSquare();

            return ltemp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Rotate
            l.Rotate(true);

        }

        private void Move_Click(object sender, EventArgs e)
        {
            //Move
            l.Move(0, 1);

        }
        private bool Check(int x, int y)
        {
            if (false != mGrid.CheckCollision(x, y, l))
            {
                if (y != 0)
                {
                    mGrid.SetGridBlock(l);
                    //Check Score
                    int lnTempScrore = mGrid.CheckScore(l);
                    if (lnTempScrore > 0)
                    {
                        if (lnTempScrore == 1)
                            mnScroe += 100;
                        else if (lnTempScrore == 2)
                            mnScroe += 300;
                        else if (lnTempScrore == 3)
                            mnScroe += 600;
                        else if (lnTempScrore == 4)
                            mnScroe += 1000;
                        label7.Text = mnScroe.ToString();
                    }

                    l = lNext;
                    lNext = RandomCreate();
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Color.Gray);

            e.Graphics.FillRectangle(myBrush, new Rectangle(0, 0, Constants.PREVIEW_WINDOW_X * Constants.BOX_SIZE, Constants.PREVIEW_WINDOW_Y * Constants.BOX_SIZE));
            myBrush.Dispose();
          
            lNext.DrawInPosition(e, Constants.PREVIEW_WINDOW_X  + 2 , 1);
            mGrid.Draw(e);
            l.Draw(e);

           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (mbPaused)
                return;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                //Check l.x - 1, ly can move
                if (false == Check(-1, 0))
                    l.Move(-1, 0);
                //Play sound
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.S)
            {
                //Check l.x + 1, ly can move
                if (false == Check(1, 0))
                    l.Move(1, 0);
                //Play sound
            }
            else if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                //Check Rotate Position  can move
                if (false == mGrid.CheckRatate(l, true))
                    l.Rotate(true);
                //Play sound
            }
            else if (e.KeyCode == Keys.Z)
            {
                //Check Rotate Position  can move
                if (false == mGrid.CheckRatate(l, false))
                    l.Rotate(false);
                //Play sound
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Down)
            {
                //speed up
                //Play sound
                l.SpeedUp(++mnSpeed);
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Control)
            {
                //Immediately down
                //int ly =  mGrid.GetDestinalPostion(l);
                l.Move(0, mGrid.GetMostDetla(l));
            }
            else if (e.KeyCode == Keys.Q)
            {
                //Quit
                //this.Close();

            }
            else if (e.KeyCode == Keys.M)
            {
                //Mute Sound

            }
            else if (e.KeyCode == Keys.P)
            {
                //Pause or resume


            }
        }

        private void TimerEventProcessor(Object myObject,
                                           EventArgs myEventArgs)
        {
            //first Check l.x , ly-1 can move 
            if (mbPaused)
                return;
            Check(0, 1);
            if (l.Move(0, 1))
                this.Refresh();
        }
        private void Load1(object sender, EventArgs e)
        {
            myTimer.Tick += new EventHandler(TimerEventProcessor);
           
            // Sets the timer interval to 5 seconds.
            myTimer.Interval = 16;
            myTimer.Start();
            this.KeyPreview = true;
            label7.Text = mnScroe.ToString();
           
        }

        private void Form1_KeyUP(object sender, KeyEventArgs e)
        {
            if (mbPaused)
                return;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Down)
            {
                //speed down
                l.SpeedUp(0);
                mnSpeed = 0;
                //l.Move(0, 1);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //pause
            mbPaused = mbPaused ? false : true;
        }

        private void button1_Click_restart(object sender, EventArgs e)
        {
            //restrart
            l = RandomCreate();
            mGrid.clear();
            mnScroe = 0;
            mbPaused = false;
            label7.Text = mnScroe.ToString();
        }

    }
}
