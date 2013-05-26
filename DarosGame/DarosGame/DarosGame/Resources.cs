using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DarosGame {
    public class Resources {
        public static SpriteFont testfont;

        public static void InitResources(ContentManager cm) {
            testfont = cm.Load<SpriteFont>("test/testfont");
        }
    }
}
