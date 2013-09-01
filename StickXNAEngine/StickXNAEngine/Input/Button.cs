using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace StickXNAEngine.Input {
    public class Button {
        public delegate void MouseEvent();

        private Sprite idle, hover, press;
        private bool visible = true, active = true;
        private MouseEvent omd, held, omu;
        private bool mhover = false, mclick = false;
        private Rectangle area;
        private bool fhover = false, fclick = false;

        /// <summary>
        /// The Sprite to use while the button's idling - not clicked, not hovered over.
        /// </summary>
        public Sprite Idle {
            get { return idle; }
            set { idle = value; }
        }

        /// <summary>
        /// The Sprite to use while the button's hovered over.
        /// </summary>
        public Sprite Hover {
            get { return hover; }
            set { hover = value; }
        }

        /// <summary>
        /// The Sprite to use while the button's being pressed.
        /// </summary>
        public Sprite Press {
            get { return press; }
            set { press = value; }
        }

        /// <summary>
        /// Is this button visible?
        /// </summary>
        public bool Visible {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Is the button active, and can thus be triggered?
        /// </summary>
        public bool Active {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// The method to run when the button is first pressed.
        /// </summary>
        public MouseEvent OnMouseDown {
            get { return omd; }
            set { omd = value; }
        }

        /// <summary>
        /// The method to run repeatedly while the button is held down.
        /// </summary>
        public MouseEvent WhileHeld {
            get { return held; }
            set { held = value; }
        }

        /// <summary>
        /// The method to run when the button is released.
        /// </summary>
        public MouseEvent OnMouseUp {
            get { return omu; }
            set { omu = value; }
        }

        /// <summary>
        /// Is the mouse hovering over this button now?
        /// </summary>
        public bool IsHovering {
            get { return mhover; }
            set { fhover = value; }
        }

        /// <summary>
        /// Is the mouse pressing this button now?
        /// </summary>
        public bool IsPressed {
            get { return mclick; }
            set {
                if(!mclick && value) {
                    if(active && omd != null) omd();
                }
                if(mclick && !value) {
                    if(active && omu != null) omu();
                }
                mclick = value;
            }
        }

        /// <summary>
        /// Force a display of the "pressed" sprite?
        /// </summary>
        public bool ForcePressed {
            get { return fclick; }
            set { fclick = value; }
        }

        /// <summary>
        /// On what area is this button active?
        /// </summary>
        public Rectangle Area {
            get { return area; }
            set { area = value; }
        }

#if XBOX

        public void Update(GameTime gt) {
            if(mclick && held != null) held();
        }

#elif WINDOWS

        public void Update(GameTime gt) {
            Update(Mouse.GetState(), gt);
        }

        public void Update(MouseState ms, GameTime gt) {
            bool hovering = area.Contains(new Point(ms.X, ms.Y));
            if(ms.LeftButton == ButtonState.Pressed) {
                if(hovering) {
                    mhover = true;
                    if(!mclick) {
                        mclick = true;
                        if(active && omd != null) omd();
                    } else {
                        if(active && held != null) held();
                    }
                } else {
                    mhover = false;
                    mclick = false;
                }
            } else {
                if(hovering) {
                    mhover = true;
                    if(mclick) {
                        mclick = false;
                        if(active && omu != null) omu();
                    }
                } else {
                    mhover = false;
                    mclick = false;
                }
            }
            if(press is AnimateSprite) ((AnimateSprite)press).Update(gt);
            if(hover is AnimateSprite) ((AnimateSprite)hover).Update(gt);
            if(idle is AnimateSprite) ((AnimateSprite)idle).Update(gt);
        }

#endif

        public void Draw(SpriteBatch sb) {
            if(visible) {
                if(active && (mhover || fhover || fclick)) {
                    if(mclick || fclick) {
                        press.Draw(sb, new Point(area.X, area.Y));
                    } else {
                        hover.Draw(sb, new Point(area.X, area.Y));
                    }
                } else {
                    idle.Draw(sb, new Point(area.X, area.Y));
                }
            }
        }
    }
}