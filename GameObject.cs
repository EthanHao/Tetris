using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    abstract class ActiveGameObject
    {
        public List<Blocks> mListBlocks = new List<Blocks>();

        public int mPosX = 6;
        public int mPosY = 0;
        public Common.Orientation mOrient = Common.Orientation.ORIENT_0;
        public DateTime mLastUpdateTime;
        public int mDuration = Constants.SPEED_DURATION;
        public ActiveGameObject()
        {
        
            mLastUpdateTime = DateTime.Now;
        }
        public void Draw(PaintEventArgs e)
        {
            foreach (var lBlock in mListBlocks)
                lBlock.Draw(mPosX,mPosY,e);
        }

        public void DrawInPosition(PaintEventArgs e,int x,int y)
        {
            foreach (var lBlock in mListBlocks)
                lBlock.Draw(x, y, e);
        }
        public bool Move(int x,int y)
        {
            if( y == 0)
            {
                mPosX += x;
                mLastUpdateTime.AddMilliseconds(-mDuration);
                return true;
            }
            DateTime lTempTime = DateTime.Now;
            if ((lTempTime - mLastUpdateTime).TotalMilliseconds < mDuration)
                return false;

            mPosX += x;
            mPosY += y;
            mLastUpdateTime = DateTime.Now;
            return true;
        }
        public void SpeedUp(int nSpeed)
        {
            if (nSpeed > 0)
                mDuration = Constants.SPEED_DURATION / nSpeed;
            else
                mDuration = Constants.SPEED_DURATION;

        }

        public Blocks GetBlocks(int i)
        {
            return mListBlocks[i];

        }
        public  void Rotate(bool bClock)
        {
            List<int> lList = new List<int>();
            mOrient= PreRotate(bClock, ref lList);
            mListBlocks[0].Rotate(lList[0], lList[1]);
            mListBlocks[1].Rotate(lList[2], lList[3]);
            mListBlocks[2].Rotate(lList[4], lList[5]);
            mListBlocks[3].Rotate(lList[6], lList[7]);
          
        }
        public abstract Common.Orientation PreRotate(bool bClock, ref List<int> nPos);
    }

    class GameT : ActiveGameObject
    {
        public GameT()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_PURPLE, -1, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_PURPLE, 0, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_PURPLE, 1, 0));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_PURPLE, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(-1);nPos.Add(0);
                    nPos.Add(0);nPos.Add(0);
                    nPos.Add(1);nPos.Add(0);
                    nPos.Add(0);nPos.Add(-1);                  
                    break;

                case Common.Orientation.ORIENT_1:                 
                    nPos.Add(0);nPos.Add(1);
                    nPos.Add(0);nPos.Add(0);
                    nPos.Add(0);nPos.Add(-1);
                    nPos.Add(-1);nPos.Add(0);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(0);nPos.Add(1);
                    nPos.Add(0);nPos.Add(0);
                    nPos.Add(1);nPos.Add(0);
                    nPos.Add(-1);nPos.Add(0);               

                    break;

                case Common.Orientation.ORIENT_3:                   
                    nPos.Add(1);nPos.Add(0);
                    nPos.Add(0);nPos.Add(0);
                    nPos.Add(0);nPos.Add(1);
                    nPos.Add(0);nPos.Add(-1);               

                    break;

                default:
                    break;
            }

            return lTemp;
        }
      
    }


    class GameLine : ActiveGameObject
    {
        public GameLine()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_ORANGE, -1, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_ORANGE, 0, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_ORANGE, 1, 0));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_ORANGE, 2, 0));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(2); nPos.Add(0);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(-2);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(-2); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(0); nPos.Add(2);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(-1);

                    break;

                default:
                    break ;
            }

            return lTemp;
        }
    
    }



    class GameL1 : ActiveGameObject
    {
        public GameL1()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_YELLOW, 0, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_YELLOW, 1, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_YELLOW, 2, 0));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_YELLOW, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(2); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(-2);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(-2); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(2);

                    break;

                default:
                    break;
            }

            return lTemp;
        }
      
    }


    class GameL2: ActiveGameObject
    {
        public GameL2()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_DK_YELLOW, -2, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_DK_YELLOW, -1, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_DK_YELLOW, 0, 0));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_DK_YELLOW, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(-2); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(2);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(2); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(0); nPos.Add(-2);
                    nPos.Add(0); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);

                    break;

                default:
                    break;
            }

            return lTemp;
        }
     
    }



    class GameZ1 : ActiveGameObject
    {
        public GameZ1()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_RED, -1, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_RED, 0, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_RED, 1, -1));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_RED, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(-1);
                    nPos.Add(-1); nPos.Add(0);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(-1); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(1); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);

                    break;

                default:
                    break;
            }

            return lTemp;
        }
     
    }


    class GameZ2: ActiveGameObject
    {
        public GameZ2()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_LT_GREEN, -1, -1));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_LT_GREEN, 0, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_LT_GREEN, 1, 0));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_LT_GREEN, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(-1); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(-1); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(0); nPos.Add(-1);
                    nPos.Add(-1); nPos.Add(0);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(1);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(0); nPos.Add(1);
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(-1);

                    break;

                default:
                    break;
            }

            return lTemp;
        }
       
    }



    class GameSquare: ActiveGameObject
    {
        public GameSquare()
        {
            mListBlocks.Add(new Blocks(0, Common.ShadeColor.COLOR_CYAN, 0, 0));
            mListBlocks.Add(new Blocks(1, Common.ShadeColor.COLOR_CYAN, 1, 0));
            mListBlocks.Add(new Blocks(2, Common.ShadeColor.COLOR_CYAN, 1, -1));
            mListBlocks.Add(new Blocks(3, Common.ShadeColor.COLOR_CYAN, 0, -1));
        }
        public override Common.Orientation PreRotate(bool bClock, ref List<int> nPos)
        {
            Common.Orientation lTemp = bClock ? (Common.Orientation)((int)(mOrient + 1) % 4) : (Common.Orientation)((int)(mOrient - 1 + 4) % 4);
            switch (lTemp)
            {
                case Common.Orientation.ORIENT_0:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(1); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_1:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(-1);
                    nPos.Add(0); nPos.Add(-1);
                    break;

                case Common.Orientation.ORIENT_2:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(0);
                    nPos.Add(-1); nPos.Add(1);
                    nPos.Add(0); nPos.Add(1);

                    break;

                case Common.Orientation.ORIENT_3:
                    nPos.Add(0); nPos.Add(0);
                    nPos.Add(1); nPos.Add(0);
                    nPos.Add(1); nPos.Add(1);
                    nPos.Add(0); nPos.Add(1);

                    break;

                default:
                    break ;
            }

            return lTemp;
        }
     
    }
}
