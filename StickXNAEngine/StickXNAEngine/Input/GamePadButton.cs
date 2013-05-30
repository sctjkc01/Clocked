using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace StickXNAEngine.Input {
    public class GamePadButton {
        TimeSpan pressed = new TimeSpan(0);
        bool prev = false, now = false;
        Buttons btn;

        public GamePadButton(Buttons btn) {
            this.btn = btn;
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

        public void Update(GamePadState gs, GameTime gt) {
            prev = now;
            now = gs.IsButtonDown(btn);
            if(now) {
                pressed += gt.ElapsedGameTime;
            } else {
                pressed = new TimeSpan(0);
            }
        }
    }
}
