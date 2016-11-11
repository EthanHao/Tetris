using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    class Blocks
    {
        
        public int mID;
        public Common.ShadeColor mColor;
        public int mRelatedX = 0;
        public int mRelatedY = 0;
     
        public Blocks(int nID,Common.ShadeColor nColor,int nX,int nY)
        {
            mID = nID;
            mColor = nColor;
            mRelatedX = nX;
            mRelatedY = nY;
        }
        public void Draw(int nBaseX, int nBaseY, PaintEventArgs e)
        {
            int lx = nBaseX + mRelatedX;
            int ly = nBaseY + mRelatedY;
            int x = (lx + 1 ) * Constants.BOX_SIZE ;
            int y = (ly + 1) * Constants.BOX_SIZE ;

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Common.getColor(mColor));


            e.Graphics.FillRectangle(myBrush, new Rectangle(x, y, Constants.BOX_SIZE, Constants.BOX_SIZE));
            myBrush.Dispose();
        }
        public void Rotate(int nRelX, int nRelY)
        {
            mRelatedX = nRelX;
            mRelatedY = nRelY;

        }
    }
}
