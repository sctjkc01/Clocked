using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Input;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Utility;
using Microsoft.Xna.Framework;
using StickXNAEngine.Graphic;

namespace DarosGame {
    public class BattleMenu : IRequireResource {
        private Menu menu;
        private Button punch, jump, arms, magick, items, run;
        private enum BMenuState {
            MINI, SEMI, ARMS, SKILL, ITEM
        }
        private StaticSprite menubg, skill, item;
        private BMenuState currState = BMenuState.SEMI;

        public BattleMenu() {
            PostProcessing.Add((IRequireResource)this);

            punch = new Button();
            punch.Area = new Rectangle(19, 515, 125, 61);
            punch.OnMouseUp = delegate {

            };

            jump = new Button();
            jump.Area = new Rectangle(147, 515, 125, 61);
            jump.OnMouseUp = delegate {

            };

            arms = new Button();
            arms.Area = new Rectangle(274, 515, 125, 61);
            arms.OnMouseUp = delegate {
                currState = BMenuState.ARMS;
            };

            magick = new Button();
            magick.Area = new Rectangle(402, 515, 125, 61);
            magick.OnMouseUp = delegate {
                currState = BMenuState.SKILL;
            };

            items = new Button();
            items.Area = new Rectangle(529, 515, 125, 61);
            items.OnMouseUp = delegate {
                currState = BMenuState.ITEM;
            };

            run = new Button();
            run.Area = new Rectangle(657, 515, 125, 61);
            run.OnMouseUp = delegate {

            };

            menu = new Menu();
            menu.Add(punch);
            menu.Add(jump);
            menu.Add(arms);
            menu.Add(magick);
            menu.Add(items);
            menu.Add(run);
        }

        public void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            menubg = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Core"));
            skill = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Arms, Magick"));
            item = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Items"));

            int i = 1;
            foreach(Button alpha in new Button[] { punch, jump, arms, magick, items, run }) {
                Texture2D tex = cm.Load<Texture2D>("Menu/Battle Menu/Battle buttons " + i);

                alpha.Hover = new StaticSprite(tex);
                alpha.Idle = new StaticSprite(tex);
                alpha.Idle.Tint = new Color(240, 240, 240);
                alpha.Press = new StaticSprite(tex);
                alpha.Press.Tint = new Color(205, 205, 205);

                i++;
            }
        }

        public void Update(GameTime gt) {
            menu.Update(gt);
            if(currState == BMenuState.MINI) {
                menu.Hide();
            } else {
                foreach(Button alpha in new Button[] { punch, jump, arms, magick, items, run }) {
                    Rectangle area = alpha.Area;
                    area.Y = (currState == BMenuState.SEMI) ? 515 : 185;
                    alpha.Area = area;
                }
                menu.Show();
            }
        }

        public void Draw(SpriteBatch sb) {
            switch(currState) {
                case BMenuState.MINI:
                    menubg.Draw(sb, new Point(0, 520));
                    break;
                case BMenuState.SEMI:
                    menubg.Draw(sb, new Point(0, 436));
                    menu.Draw(sb);
                    break;
                case BMenuState.ARMS:
                case BMenuState.SKILL:
                    menubg.Draw(sb, new Point(0, 105));
                    menu.Draw(sb);
                    skill.Draw(sb, new Point(17, 513));
                    break;
                case BMenuState.ITEM:
                    menubg.Draw(sb, new Point(0, 105));
                    menu.Draw(sb);
                    item.Draw(sb, new Point(130, 508));
                    break;
            }
        }
    }
}
