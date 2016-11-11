using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class GridBlock
    {
       
        public Common.ShadeColor mColor;
        public int mX = 0;
        public int mY = 0;
        public bool mbValid;
        public  GridBlock(Common.ShadeColor nColor, int nX, int nY,bool nbValid)
        {
            mColor = nColor;
            mX = nX;
            mY = nY;
            mbValid = nbValid;
        }
        public void Draw( PaintEventArgs e)
        {
            if (mbValid == false)
                return;

            int x = mX * Constants.BOX_SIZE;
            int y = mY * Constants.BOX_SIZE;

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Common.getColor(mColor));
            e.Graphics.FillRectangle(myBrush, new Rectangle(x, y, Constants.BOX_SIZE, Constants.BOX_SIZE));
            myBrush.Dispose();
        }
 
    }
    class Grid
    {
        public List<GridBlock> mListBlocks = new List<GridBlock>();
        public  Grid()
        {
            for (int y = 0; y < Constants.PREVIEW_WINDOW_Y; y++ )
                for (int x= 0; x < Constants.PREVIEW_WINDOW_X; x++)
                    mListBlocks.Add(new GridBlock(Common.ShadeColor.COLOR_YELLOW, x, y,false));
        }

        public void clear()
        {
             for (int y = 0; y < Constants.PREVIEW_WINDOW_Y; y++)
                for (int x = 0; x < Constants.PREVIEW_WINDOW_X; x++)
                    mListBlocks[y * Constants.PREVIEW_WINDOW_X + x].mbValid = false; 
        }
        public void SetGridBlock(ActiveGameObject nActiveGameObject)
        {
            for (int i = 0; i < 4; i++)
            {
                Blocks lTempBlocks = nActiveGameObject.GetBlocks(i);
                int bx = nActiveGameObject.mPosX + lTempBlocks.mRelatedX + 1;
                int by = nActiveGameObject.mPosY + lTempBlocks.mRelatedY + 1;
                mListBlocks[by * Constants.PREVIEW_WINDOW_X + bx].mbValid = true;
                mListBlocks[by * Constants.PREVIEW_WINDOW_X + bx].mColor = lTempBlocks.mColor;
            }

         

        }
        public void Draw(PaintEventArgs e)
        {
            for (int y = 0; y < Constants.PREVIEW_WINDOW_Y; y++)
                for (int x = 0; x < Constants.PREVIEW_WINDOW_X; x++)
                    mListBlocks[y * Constants.PREVIEW_WINDOW_X + x].Draw(e);      
        }
        public int CheckScore(ActiveGameObject nActiveGameObject)
        {
            SortedSet<int> lScoreList = new SortedSet<int>();
            for (int i = 0; i < 4; i++)
            {
                Blocks lTempBlocks = nActiveGameObject.GetBlocks(i);
                int y = nActiveGameObject.mPosY + lTempBlocks.mRelatedY + 1;
                bool bScore = true;
                for (int j = 0; j < Constants.PREVIEW_WINDOW_X; j++)
                {
                    if (mListBlocks[y * Constants.PREVIEW_WINDOW_X + j ].mbValid == false)
                    {
                        bScore = false;
                        break;
                    }
                }
                if (bScore)
                    lScoreList.Add(y);
            }


            foreach (int y in lScoreList)
            {
                for (int j = 0; j < Constants.PREVIEW_WINDOW_X; j++)
                {
                    mListBlocks[y * Constants.PREVIEW_WINDOW_X + j].mbValid = false;
                }
                //Move all blocks above this line 
                for (int ii = y - 1; ii >= 0; ii--)
                {
                    for (int j = 0; j < Constants.PREVIEW_WINDOW_X; j++)
                    {
                        mListBlocks[(ii + 1) * Constants.PREVIEW_WINDOW_X + j].mbValid = mListBlocks[(ii) * Constants.PREVIEW_WINDOW_X + j].mbValid;
                        mListBlocks[(ii + 1) * Constants.PREVIEW_WINDOW_X + j].mColor = mListBlocks[(ii) * Constants.PREVIEW_WINDOW_X + j].mColor;
                        mListBlocks[(ii) * Constants.PREVIEW_WINDOW_X + j].mbValid = false;
                    }
                }
            }


            return lScoreList.Count;
        }
        public bool CheckCollision(int x ,int y,ActiveGameObject nActiveGameObject)
        {
            int npx = nActiveGameObject.mPosX + x;
            int npy = nActiveGameObject.mPosY + y;
            for(int i =  0; i < 4; i++)
            {
                Blocks lTempBlocks = nActiveGameObject.GetBlocks(i);
                int bx = npx + lTempBlocks.mRelatedX + 1;
                int by = npy + lTempBlocks.mRelatedY + 1;
                if (bx < 0 || bx >= Constants.PREVIEW_WINDOW_X ||
                    by < 0 || by >=  Constants.PREVIEW_WINDOW_Y ||
                    mListBlocks[by * Constants.PREVIEW_WINDOW_X + bx].mbValid)
                    return true;
            }
            return false;
        }
        public bool CheckRatate(ActiveGameObject nActiveGameObject,bool bClock)
        {
            List<int> lt = new List<int>();
            nActiveGameObject.PreRotate(bClock,ref lt);
            for (int i = 0; i < 4; i++)
            {
                int bx = nActiveGameObject.mPosX + lt[i * 2] + 1;
                int by = nActiveGameObject.mPosY + lt[i * 2 + 1] + 1;
                if (bx < 0 || bx >= Constants.PREVIEW_WINDOW_X ||
                    by < 0 || by >= Constants.PREVIEW_WINDOW_Y ||
                    mListBlocks[by * Constants.PREVIEW_WINDOW_X + bx].mbValid)
                    return true;
            }
            return false;
        }
        public int GetMostDetla(ActiveGameObject nActiveGameObject)
        {
            for (int y = 0; y < Constants.PREVIEW_WINDOW_Y; y++ )
            {
                if (CheckCollision(0, y, nActiveGameObject))
                {                
                    return y - 1;;
                }
            }
            return 0;
        }
        
    }
}
