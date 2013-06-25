using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DarosGame {
    public class Resources {
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void InitResources(ContentManager cm) {
            fonts["04b03m"] = cm.Load<SpriteFont>("04b03");
            fonts["04b03s"] = cm.Load<SpriteFont>("04b03small");
            fonts["04b03l"] = cm.Load<SpriteFont>("04b03large");

            fonts["8bits"] = cm.Load<SpriteFont>("8BitWondersmall");
            fonts["8bitm"] = cm.Load<SpriteFont>("8BitWonder");
            fonts["8bitl"] = cm.Load<SpriteFont>("8BitWonderlarge");
        }
    }
}
