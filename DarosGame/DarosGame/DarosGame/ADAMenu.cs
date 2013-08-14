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

        private float currSlide = 1.00f, invSlide = 0.00f, statSlide = 0.00f, skillSlide = 0.00f;

        public ADAMenu() {
            PostProcessing.Add((IRequireResource)this);

            item = new Button();
            item.Area = new Microsoft.Xna.Framework.Rectangle(14, 362, 158, 42);
            item.OnMouseUp = delegate {
                if(currState == AMenuState.INV) {
                    currState = AMenuState.NONE;
                } else {
                    currState = AMenuState.INV;
                }
            };

            stat = new Button();
            stat.Area = new Microsoft.Xna.Framework.Rectangle(14, 409, 158, 42);
            stat.OnMouseUp = delegate {
                if(currState == AMenuState.STAT) {
                    currState = AMenuState.NONE;
                } else {
                    currState = AMenuState.STAT;
                }
            };

            journal = new Button();
            journal.Area = new Microsoft.Xna.Framework.Rectangle(14, 456, 158, 42);
            journal.OnMouseUp = delegate {

            };

            skill = new Button();
            skill.Area = new Microsoft.Xna.Framework.Rectangle(14, 503, 158, 42);
            skill.OnMouseUp = delegate {
                if(currState == AMenuState.SKILL) {
                    currState = AMenuState.NONE;
                } else {
                    currState = AMenuState.SKILL;
                }
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

            Point origin = new Point(22, 3);
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
                currState = AMenuState.NONE;
                currSlide = 1f; statSlide = 0f; invSlide = 0f; skillSlide = 0f;
                StaticVars.currState = GameState.FROMADA;
            } else {
                float dCurr = -0.05f, dInv = -0.05f, dStat = -0.05f, dSkill = -0.05f;

                switch(currState) {
                    case AMenuState.NONE:
                        dCurr *= -1;
                        break;
                    case AMenuState.INV:
                        dInv *= -1;
                        break;
                    case AMenuState.SKILL:
                        dSkill *= -1;
                        break;
                    case AMenuState.STAT:
                        dStat *= -1;
                        break;
                }

                currSlide = Math.Max(Math.Min(currSlide + dCurr, 1f), 0f);
                invSlide = Math.Max(Math.Min(invSlide + dInv, 1f), 0f);
                skillSlide = Math.Max(Math.Min(skillSlide + dSkill, 1f), 0f);
                statSlide = Math.Max(Math.Min(statSlide + dStat, 1f), 0f);
            }
        }

        public void Draw(SpriteBatch sb) {
            menu.Draw(sb);
            if(currSlide != 0.00f) {
                foreach(int alpha in new int[] { 463, 508, 554 }) {
                    currencyBack.Draw(sb, new Point(800 - (int)(251f * currSlide), alpha));
                }
            }

            if(invSlide != 0.00f) {
                invBack.Draw(sb, new Point(800 - (int)(600f * invSlide), -3));
            }

            if(statSlide != 0.00f) {
                // N/A
            }

            if(skillSlide != 0.00f) {
                invBack.Draw(sb, new Point(800 - (int)(600f * skillSlide), -3));
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
