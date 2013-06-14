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

        /// <summary>
        /// Create a StaticSprite with a default origin of (0, 0), using the entire texture.
        /// </summary>
        /// <param name="tex">The texture to use.</param>
        public StaticSprite(Texture2D tex) : this(tex, new Point(0, 0)) { }

        /// <summary>
        /// Create a StaticSprite with a specificed origin, but using the entire texture.
        /// </summary>
        /// <param name="tex">The texture to use.</param>
        /// <param name="origin">The origin to use.</param>
        public StaticSprite(Texture2D tex, Point origin) : this(tex, origin, new Rectangle(0, 0, tex.Width, tex.Height)) { }

        /// <summary>
        /// Create a StaticSprite with a specific origin, using only a portion of the specified texture.
        /// </summary>
        /// <param name="tex">The texture to use.</param>
        /// <param name="origin">The origin to use.</param>
        /// <param name="src">A rectangle defining the portion of the texture to use.</param>
        public StaticSprite(Texture2D tex, Point origin, Rectangle src) {
            this.tex = tex;
            this.origin = origin;
            this.src = src;
        }

        public Rectangle Size {
            get { return new Rectangle(0, 0, src.Width, src.Height); }
        }

        public override void Draw(SpriteBatch sb, Point loc, Vector2 scale) {
            sb.Draw(tex, new Rectangle(loc.X - (int)(origin.X * scale.X), loc.Y - (int)(origin.Y * scale.Y), (int)(src.Width * scale.X), (int)(src.Height * scale.Y)), src, this.Tint); 
        }
    }
}
