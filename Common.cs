using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    static class Constants
    {
        public const int SPEED_DURATION = 8*1000 / 26;
        public const int SCREEN_HEIGHT = 650;
        public const int SCREEN_WIDTH = 500;
        public const int BOX_SIZE = 20;
        public const int BOX_SIZE_HALF = 10;

        public const int PREVIEW_WINDOW_X = 17;
        public const int PREVIEW_WINDOW_Y = 26;

        public const int GAME_MAX_X = 9;
        public const int GAME_MAX_Y = 29;
        public const int GAME_MIN_X = 0;
        public const int GAME_MIN_Y = 0;
    }
    class Common
    {
        public enum Orientation
        {
           
            ORIENT_0 = 0,
            ORIENT_1,
            ORIENT_2,
            ORIENT_3
        };

        public enum ShadeColor
        {
            COLOR_ORANGE,
            COLOR_DK_ORANGE,
            COLOR_BLUE,
            COLOR_DK_BLUE,
            COLOR_PURPLE,
            COLOR_DK_PURPLE,
            COLOR_LT_GREEN,
            COLOR_DK_GREEN,
            COLOR_LT_BLUE,
            COLOR_YELLOW,
            COLOR_DK_YELLOW,
            COLOR_RED,
            COLOR_DK_RED,
            COLOR_CYAN,
            COLOR_DK_CYAN,
            COLOR_GREY,
            COLOR_DK_GREY,
            COLOR_BACKGROUND_CUSTOM
        };

        public static Color getColor(ShadeColor color)
        {
            Color tmp;

            switch (color)
            {
                case ShadeColor.COLOR_ORANGE:
                    tmp = Color.FromArgb(255,255,0,255);
                    break;

                case ShadeColor.COLOR_YELLOW:
                    tmp = Color.FromArgb(255, 255, 124, 255);
                    break;
                case ShadeColor.COLOR_RED:
                    tmp = Color.FromArgb(255, 200, 200, 100);
                    break;
                case ShadeColor.COLOR_LT_GREEN:
                    tmp = Color.FromArgb(255, 0, 255, 0);
                    break;
                case ShadeColor.COLOR_CYAN:
                    tmp = Color.FromArgb(255, 233, 255, 12);
                    break;

                case ShadeColor.COLOR_DK_YELLOW:
                    tmp = Color.FromArgb(255, 0, 0, 255);
                    break;
                case ShadeColor.COLOR_PURPLE:
                    tmp = Color.FromArgb(255, 23, 0, 255);
                    break;
                default:
                    tmp = Color.FromArgb(255, 255, 0, 255);
                    break;
            }

            return tmp;
        }

    }


}
