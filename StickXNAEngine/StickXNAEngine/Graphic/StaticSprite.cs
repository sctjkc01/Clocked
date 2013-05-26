using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StickXNAEngine.Graphic {
    public class StaticSprite : Sprite {
        Texture2D tex;
        Rectangle src;

        public StaticSprite(Texture2D tex) {
            this.tex = tex;
            src = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public StaticSprite(Texture2D tex, Rectangle src) {
            this.tex = tex;
            this.src = src;
        }

        public override void Draw(SpriteBatch sb, Point loc, float scale = 1.0f) {
            sb.Draw(tex, new Rectangle(loc.X, loc.Y, (int)(src.Width * scale), (int)(src.Height * scale)), src, this.Tint); 
        }
    }
}
