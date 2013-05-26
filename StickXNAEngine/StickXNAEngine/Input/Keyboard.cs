using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace StickXNAEngine.Input {
    public class Keyboard {
        Dictionary<Keys, KeyboardKey> keyDict = new Dictionary<Keys, KeyboardKey>();

        public KeyboardKey this[Keys item] {
            get {
                return keyDict[item];
            }
        }

        public bool IsShifting {
            get { return keyDict[Keys.LeftShift].IsPressed || keyDict[Keys.RightShift].IsPressed; }
        }

        public bool IsAlting {
            get { return keyDict[Keys.LeftAlt].IsPressed || keyDict[Keys.RightAlt].IsPressed; }
        }

        public bool IsCtrling {
            get { return keyDict[Keys.LeftControl].IsPressed || keyDict[Keys.RightControl].IsPressed; }
        }

        public Keyboard() {
            Keys[] keys = new Keys[] {
                Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
                Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K,
                Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V,
                Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.LeftShift, Keys.RightShift, Keys.LeftAlt, Keys.RightAlt,
                Keys.LeftControl, Keys.RightControl,

                Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Back, Keys.Insert, Keys.Delete, Keys.Home, Keys.PageUp,
                Keys.PageDown, Keys.End, Keys.Enter,

                Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10,
                Keys.F11, Keys.F12,

                Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5,
                Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, 

                Keys.OemMinus, Keys.OemPlus,
                Keys.OemOpenBrackets, Keys.OemCloseBrackets, Keys.OemPipe,
                Keys.OemSemicolon, Keys.OemQuotes,
                Keys.OemComma, Keys.OemPeriod, Keys.OemQuestion
            };
            foreach(Keys key in keys) {
                keyDict[key] = new KeyboardKey(key);
            }
        }
    }
}
