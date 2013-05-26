using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace StickXNAEngine.Input {
    public class KeyboardKey {
        TimeSpan pressed = new TimeSpan(0);
        bool prev = false, now = false;
        Keys key;

        public KeyboardKey(Keys key) {
            this.key = key;
        }

        public bool JustPressed {
            get { return (now && !prev); }
        }

        public bool IsPressed {
            get { return (now); }
        }

        /// <summary>
        /// Gets how long this key has been pressed, in milliseconds.
        /// </summary>
        public double MillisPressed {
            get { return pressed.TotalMilliseconds; }
        }

        public void Update(KeyboardState ks, GameTime gt) {
            prev = now;
            now = ks.IsKeyDown(key);
            if(now) {
                pressed += gt.ElapsedGameTime;
            } else {
                pressed = new TimeSpan(0);
            }
        }
    }
}
