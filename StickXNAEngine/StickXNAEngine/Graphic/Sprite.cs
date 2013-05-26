using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StickXNAEngine.Graphic {
    public abstract class Sprite {
        Color tint;

        public Color Tint {
            get { return tint; }
            set { tint = value; }
        }

        public Sprite() {
            tint = Color.White;
        }

        public abstract void Draw(SpriteBatch sb, Point loc, float scale = 1.0f);
    }
}
