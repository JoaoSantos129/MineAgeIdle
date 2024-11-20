using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal class Constants
    {
        public static int DEFAULT_SCREEN_WIDTH = 1920;
        public static int DEFAULT_SCREEN_HEIGHT = 1080;
        public static int MENU_WIDTH = 405;
        public static int BACKGROUND_WIDTH_VIEW_WITH_MENU = Constants.DEFAULT_SCREEN_WIDTH - Constants.MENU_WIDTH;
        public static double TICK_DURATION = 700;
        public static int MENU_BUTTONS_HEIGHT = 70;
        public static int MENU_VERTICAL_SPACING = 20;
        public static float INFLATION_MULTIPLIYER = 1.3f;
        public static double VALUES_MAX_AMOUNT = 999_999_999_999_999.999;
    }
}