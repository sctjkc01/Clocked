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
        Point origin;

        public StaticSprite(Texture2D tex, Point origin) {
            this.tex = tex;
            this.origin = origin;
            src = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public StaticSprite(Texture2D tex, Point origin, Rectangle src) {
            this.tex = tex;
            this.origin = origin;
            this.src = src;
        }

        public override void Draw(SpriteBatch sb, Point loc, float scale = 1.0f) {
            sb.Draw(tex, new Rectangle(loc.X - (int)(origin.X * scale), loc.Y - (int)(origin.Y * scale), (int)(src.Width * scale), (int)(src.Height * scale)), src, this.Tint); 
        }
    }
}
