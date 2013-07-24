using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Utility;
using StickXNAEngine.Input;
using StickXNAEngine.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarosGame {
    public class ADAMenu : IRequireResource {
        private Menu menu;
        private Button item, journal, skill, stat, quit;
        private enum AMenuState {
            NONE, INV, STAT, SKILL
        }
        private AMenuState currState = AMenuState.NONE;

        private StaticSprite currencyBack;
        private StaticSprite invBack, invMiniBack, invBlock, invMiniBlock, invTab;
        private StaticSprite itemBack, itemDescBack;
        private StaticSprite skillBack, skillDescBack;

        public ADAMenu() {
            PostProcessing.Add((IRequireResource)this);

            item = new Button();
            item.Area = new Microsoft.Xna.Framework.Rectangle(14, 362, 158, 42);
            item.OnMouseUp = delegate {
                currState = AMenuState.INV;
            };

            stat = new Button();
            stat.Area = new Microsoft.Xna.Framework.Rectangle(14, 409, 158, 42);
            stat.OnMouseUp = delegate {
                currState = AMenuState.STAT;
            };

            journal = new Button();
            journal.Area = new Microsoft.Xna.Framework.Rectangle(14, 456, 158, 42);
            journal.OnMouseUp = delegate {

            };

            skill = new Button();
            skill.Area = new Microsoft.Xna.Framework.Rectangle(14, 503, 158, 42);
            skill.OnMouseUp = delegate {
                currState = AMenuState.SKILL;
            };

            quit = new Button();
            quit.Area = new Rectangle(14, 552, 158, 42);
            quit.OnMouseUp = delegate {
                StaticVars.inst.Exit();
            };

            menu = new Menu();
            menu.Add(item);
            menu.Add(journal);
            menu.Add(skill);
            menu.Add(stat);
            menu.Add(quit);
        }

        public void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            currencyBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Top Menu Currency Block"));

            invBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Inventory Menu"));
            invMiniBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Inventory Mini Menu"));
            invBlock = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Item Block"));
            invMiniBlock = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Mini Item Block"));
            invTab = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Tab"));

            itemBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Items"), new Point(0, 0), new Rectangle(0, 0, 119, 86));
            itemDescBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Items"), new Point(0, 0), new Rectangle(167, 5, 370, 80));

            skillBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Arms, Magick"), new Point(0, 0), new Rectangle(0, 0, 370, 79));
            skillDescBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Arms, Magick"), new Point(0, 0), new Rectangle(395, 0, 372, 79));

            Point origin = new Point(8, 9);
            foreach(Pair<Button, string> alpha in new Pair<Button, string>[] { new Pair<Button, string>(item, "Items"), new Pair<Button, string>(journal, "Journal"), new Pair<Button, string>(stat, "Stats"), new Pair<Button, string>(skill, "Skills"), new Pair<Button, string>(quit, "Quit") }) {
                Texture2D tex = cm.Load<Texture2D>("Menu/Top Menu/Top Menu - " + alpha.Item2);

                alpha.Item1.Hover = new StaticSprite(tex, origin);
                alpha.Item1.Idle = new StaticSprite(tex, origin);
                alpha.Item1.Idle.Tint = new Color(200, 200, 200);
                alpha.Item1.Press = new StaticSprite(tex, origin);
                alpha.Item1.Press.Tint = new Color(175, 175, 175);
            }
        }

        public void Update(GameTime gt) {
            menu.Update(gt);
            StaticVars.player.Ctrls.Update(gt);

            if(StaticVars.player.Ctrls.LeavingADA) {
                StaticVars.currState = GameState.FROMADA;
            }
        }

        public void Draw(SpriteBatch sb) {
            menu.Draw(sb);
            switch(currState) {
                case AMenuState.NONE:
                    foreach(int alpha in new int[] { 463, 508, 554 }) {
                        currencyBack.Draw(sb, new Point(549, alpha));
                    }
                    break;
                case AMenuState.INV:
                case AMenuState.SKILL:
                case AMenuState.STAT:
                    break;
            }
        }

        public void Draw(SpriteBatch sb, float trans) {
            foreach(Button alpha in new Button[] { item, stat, journal, skill, quit }) {
                alpha.Idle.Draw(sb, new Point(14 - (int)(182f * (1f - trans)), alpha.Area.Y));
            }
            foreach(int alpha in new int[] { 463, 508, 554 }) {
                currencyBack.Draw(sb, new Point(549 + (int)(254f * (1f - trans)), alpha));
            }
        }
    }
}
