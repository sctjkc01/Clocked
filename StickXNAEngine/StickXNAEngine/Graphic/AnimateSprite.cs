using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace StickXNAEngine.Graphic {
    public class AnimateSprite : Sprite, IUpdating {
        List<StaticSprite> states;
        int curr = 0;
        TimeSpan delay, count = new TimeSpan(0);

        public AnimateSprite(TimeSpan delay) {
            this.delay = delay;
            states = new List<StaticSprite>();
        }

        public override SpriteEffects Mirror {
            get {
                return base.Mirror;
            }
            set {
                base.Mirror = value;
                foreach(StaticSprite sprite in states) {
                    sprite.Mirror = value;
                }
            }
        }

        public void Add(StaticSprite sprite) {
            states.Add(sprite);
        }

        public void Update(GameTime gt) {
            count += gt.ElapsedGameTime;
            if(count >= delay) {
                curr++;
                if(curr >= states.Count) {
                    curr = 0;
                }
                count -= delay;
            }
        }

        public override void Draw(SpriteBatch sb, Point loc, Vector2 scale) {
            states[curr].Draw(sb, loc, scale);
        }
    }
}
