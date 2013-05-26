using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StickXNAEngine.Graphic {
    public class AnimateSprite : Sprite {
        List<StaticSprite> states;
        int curr = 0;
        TimeSpan delay, count = new TimeSpan(0);

        public AnimateSprite(TimeSpan delay) {
            this.delay = delay;
            states = new List<StaticSprite>();
        }

        public AnimateSprite(IEnumerable<StaticSprite> states, TimeSpan delay) {
            this.delay = delay;
            this.states = states.ToList<StaticSprite>();
        }

        public void Update(GameTime gt) {
            count += gt.ElapsedGameTime;
            if(count >= delay) {
                curr++;
                if(curr >= states.Count) {
                    curr = 0;
                }
            }
        }


        public override void Draw(SpriteBatch sb, Point loc, float scale = 1.0f) {
            states[curr].Draw(sb, loc, scale);
        }
    }
}
