using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;
using Microsoft.Xna.Framework.Input;

namespace DarosGame {
    public static class StaticVars {
        public static Point Camera = new Point(0, 0);
        public static Room CurrRoom = null;

        public static Pair<Room, Point> Exit = null;
    }

    public static class EZTweakVars {
        // Interaction Variables
        public static int PlayerInteractRange = 100; //Units: Pixels
        public static Keys InteractKey = Keys.E; //Replace letter as you will [REQUIRES RESTART]


    }
}
