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
        public static double FACILITY_COST = 300;
        public static double SHOES_COST = 50;
        public static double HIKING_EQUIPEMENT_COST = 100;
        public static double DEMOLITION_EXPLOSIVE_COST = 750;
        public static double BOAT_COST = 4000;
        public static double SUIT_COST = 2000;
    }
}