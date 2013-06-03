using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace DarosGame {
    public static class StaticVars {
        public static Point Camera = new Point(0, 0);
        public static Room CurrRoom = null;

        public static Pair<Room, Point> Exit = null;
    }
}
