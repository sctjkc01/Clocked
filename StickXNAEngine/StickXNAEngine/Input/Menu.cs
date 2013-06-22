using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;
using Microsoft.Xna.Framework.Input;

namespace StickXNAEngine.Input {
    public class Menu : IUpdating {
        private List<Button> btns = new List<Button>();

        public Menu() {
            PostProcessing.Add(this);
        }

        public void Add(Button btn) {
            btns.Add(btn);
        }

        public void Update(GameTime gt) {
            MouseState ms = Mouse.GetState();
            if(btns.Count > 0) {
                foreach(Button alpha in btns) {
                    alpha.Update(ms, gt);
                }
            }
        }

        public void Hide() {
            if(btns.Count > 0) {
                foreach(Button alpha in btns) {
                    alpha.Visible = false;
                    alpha.Active = false;
                }
            }
        }

        public void Show() {
            if(btns.Count > 0) {
                foreach(Button alpha in btns) {
                    alpha.Visible = true;
                    alpha.Active = true;
                }
            }
        }
    }
}
