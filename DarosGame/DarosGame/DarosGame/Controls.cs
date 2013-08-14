using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using StickXNAEngine.Input;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace DarosGame {
    public class Controls {

        private Dictionary<Keys, KeyboardKey> keys = new Dictionary<Keys, KeyboardKey>();
        private Dictionary<Buttons, GamePadButton> btns = new Dictionary<Buttons, GamePadButton>();

        public Controls() {
            keys[Keys.W] = new KeyboardKey(Keys.W);
            keys[Keys.A] = new KeyboardKey(Keys.A);
            keys[Keys.S] = new KeyboardKey(Keys.S);
            keys[Keys.D] = new KeyboardKey(Keys.D);
            keys[Keys.R] = new KeyboardKey(Keys.R);
            keys[Keys.Escape] = new KeyboardKey(Keys.Escape);
            keys[Keys.F] = new KeyboardKey(Keys.F);
            keys[EZTweakVars.InteractKey] = new KeyboardKey(EZTweakVars.InteractKey);
            keys[Keys.Space] = new KeyboardKey(Keys.Space);

            btns[Buttons.LeftThumbstickUp] = new GamePadButton(Buttons.LeftThumbstickUp);
            btns[Buttons.LeftThumbstickLeft] = new GamePadButton(Buttons.LeftThumbstickLeft);
            btns[Buttons.LeftThumbstickRight] = new GamePadButton(Buttons.LeftThumbstickRight);
            btns[Buttons.LeftThumbstickDown] = new GamePadButton(Buttons.LeftThumbstickDown);
            btns[Buttons.Y] = new GamePadButton(Buttons.Y);
            btns[Buttons.B] = new GamePadButton(Buttons.B);
            btns[Buttons.A] = new GamePadButton(Buttons.A);
        }

        public void Update(GameTime gt) {
            GamePadState gs = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            KeyboardState ks = Keyboard.GetState();

            foreach(Keys key in keys.Keys) {
                keys[key].Update(ks, gt);
            }
            foreach(Buttons btn in btns.Keys) {
                btns[btn].Update(gs, gt);
            }
        }



        private Boolean MovingUp {
            get { return keys[Keys.W].IsPressed || btns[Buttons.LeftThumbstickUp].IsPressed; }
        }

        private Boolean MovingDown {
            get { return keys[Keys.S].IsPressed || btns[Buttons.LeftThumbstickDown].IsPressed; }
        }

        private Boolean MovingLeft {
            get { return keys[Keys.A].IsPressed || btns[Buttons.LeftThumbstickLeft].IsPressed; }
        }

        private Boolean MovingRight {
            get { return keys[Keys.D].IsPressed || btns[Buttons.LeftThumbstickRight].IsPressed; }
        }

        public Boolean EnteringADA {
            get { return keys[Keys.R].JustPressed || btns[Buttons.Y].JustPressed; }
        }

        public Boolean LeavingADA {
            get { return keys[Keys.R].JustPressed || keys[Keys.Escape].JustPressed || btns[Buttons.B].JustPressed; }
        }

        public Boolean Fullscreen {
            get { return keys[Keys.F].JustPressed; }
        }

        public Boolean Interact {
            get { return keys[EZTweakVars.InteractKey].JustPressed || btns[Buttons.B].JustPressed; }
        }

        public Direction Movement {
            get {
                // "Garbage input" checking
                int count = 0;
                if(MovingUp) count++;
                if(MovingDown) count++;
                if(MovingLeft) count++;
                if(MovingRight) count++;
                if(count > 2) return Direction.DENNIS;

                if(count == 2) {
                    if(MovingUp && MovingLeft) return Direction.NORTHWEST;
                    if(MovingUp && MovingRight) return Direction.NORTHEAST;
                    if(MovingDown && MovingLeft) return Direction.SOUTHWEST;
                    if(MovingDown && MovingRight) return Direction.SOUTHEAST;
                    return Direction.DENNIS;
                } else {
                    if(MovingUp) return Direction.NORTH;
                    if(MovingDown) return Direction.SOUTH;
                    if(MovingLeft) return Direction.WEST;
                    if(MovingRight) return Direction.EAST;
                    return Direction.DENNIS;
                }
            }
        }

        public Boolean ConvoNext {
            get { return keys[Keys.E].JustPressed || btns[Buttons.A].JustPressed; }
        }
    }
}
