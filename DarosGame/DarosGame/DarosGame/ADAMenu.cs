using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Utility;
using StickXNAEngine.Input;
using StickXNAEngine.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DarosGame {
    public class ADAMenu : IRequireResource {
        private Menu menu;
        private Button item, journal, skill, stat, quit;
        private enum AMenuState {
            NONE, INV, STAT, SKILL
        }
        private AMenuState currState = AMenuState.NONE;

        private StaticSprite currencyBack;
        private StaticSprite invBack, invMiniBack, invMiniBlock;
        private StaticSprite itemBack, itemDescBack;
        private StaticSprite skillBack, skillDescBack;

        private float currSlide = 1.00f, invSlide = 0.00f, statSlide = 0.00f, skillSlide = 0.00f;

        private Button[] pktButtons;
        private int currPkt = 0;
        /// <summary>
        /// Pocket Tabs are centered on line after this tab
        /// </summary>
        private int pktCenter = 1;
        private ItemButton[] itemButtons; private int currDragButton = -1;

        public int CDB {
            get { return currDragButton; }
            set { currDragButton = value; }
        }

        public Inventory.Pocket CurrPkt {
            get { return StaticVars.player.Stats.Inv.Pockets[currPkt]; }
        }

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

            pktButtons = new Button[6];
            
            // Scroll Left button
            pktButtons[0] = new Button();
            pktButtons[0].Active = false;
            pktButtons[0].Visible = false;
            pktButtons[0].Area = new Rectangle(231, 55, 13, 19);
            pktButtons[0].OnMouseUp = delegate {
                if(pktCenter > 1) pktCenter--;
                if(pktCenter == 1) {
                    pktButtons[0].Active = false;
                    pktButtons[0].Visible = false;
                }
                pktButtons[5].Active = true;
                pktButtons[5].Visible = true;
            };

            // Actual Tabs
            for(int i = 0; i < 4; i++) {
                pktButtons[i + 1] = new Button();
                pktButtons[i + 1].Active = false;
                pktButtons[i + 1].Visible = false;
                pktButtons[i + 1].Area = new Rectangle(244 + (i * 121), 55, 121, 19);
            }
            pktButtons[1].OnMouseUp = delegate {
                if(currDragButton == -1) {
                    currPkt = pktCenter - 1;
                    StaticVars.player.Stats.Inv.Pockets[currPkt].Condense();
                } else {
                    if(StaticVars.player.Stats.Inv.Pockets[pktCenter - 1].Content.Contains<Item.Item>(null)) { // Contains empty space...
                        Item.Item alpha = StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton];
                        StaticVars.player.Stats.Inv.Pockets[pktCenter - 1].Add(alpha);
                        StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton] = null;
                    }
                }
                currDragButton = -1;
            };
            pktButtons[2].OnMouseUp = delegate {
                if(currDragButton == -1) {
                    currPkt = pktCenter;
                    StaticVars.player.Stats.Inv.Pockets[currPkt].Condense();
                } else {
                    if(StaticVars.player.Stats.Inv.Pockets[pktCenter].Content.Contains<Item.Item>(null)) { // Contains empty space...
                        Item.Item alpha = StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton];
                        StaticVars.player.Stats.Inv.Pockets[pktCenter].Add(alpha);
                        StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton] = null;
                    }
                }
                currDragButton = -1;
            };
            pktButtons[3].OnMouseUp = delegate {
                if(currDragButton == -1) {
                    currPkt = pktCenter + 1;
                    StaticVars.player.Stats.Inv.Pockets[currPkt].Condense();
                } else {
                    if(StaticVars.player.Stats.Inv.Pockets[pktCenter + 1].Content.Contains<Item.Item>(null)) { // Contains empty space...
                        Item.Item alpha = StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton];
                        StaticVars.player.Stats.Inv.Pockets[pktCenter + 1].Add(alpha);
                        StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton] = null;
                    }
                }
                currDragButton = -1;
            };
            pktButtons[4].OnMouseUp = delegate {
                if(currDragButton == -1) {
                    currPkt = pktCenter + 2;
                    StaticVars.player.Stats.Inv.Pockets[currPkt].Condense();
                } else {
                    if(StaticVars.player.Stats.Inv.Pockets[pktCenter + 2].Content.Contains<Item.Item>(null)) { // Contains empty space...
                        Item.Item alpha = StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton];
                        StaticVars.player.Stats.Inv.Pockets[pktCenter + 2].Add(alpha);
                        StaticVars.player.Stats.Inv.Pockets[currPkt][currDragButton] = null;
                    }
                }
                currDragButton = -1;
            };

            // Scroll Right button
            pktButtons[5] = new Button();
            pktButtons[5].Active = false;
            pktButtons[5].Visible = false;
            pktButtons[5].Area = new Rectangle(728, 55, 13, 19);
            pktButtons[5].OnMouseUp = delegate {
                if(pktCenter < StaticVars.player.Stats.Inv.Pockets.Count - 3) pktCenter++;
                if(pktCenter == StaticVars.player.Stats.Inv.Pockets.Count - 3) {
                    pktButtons[5].Active = false;
                    pktButtons[5].Visible = false;
                }
                pktButtons[0].Active = true;
                pktButtons[0].Visible = true;
            };

            itemButtons = new ItemButton[20];

            for(int i = 0; i < 20; i++) {
                itemButtons[i] = new ItemButton(i, this);
            }
        }

        public void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            currencyBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Top Menu Currency Block"));

            invBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Inventory Menu"));
            invMiniBack = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Inventory Mini Menu"));
            invMiniBlock = new StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Mini Item Block"));

            /*
            itemBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Items"), new Point(0, 0), new Rectangle(0, 0, 119, 86));
            itemDescBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Items"), new Point(0, 0), new Rectangle(167, 5, 370, 80));

            skillBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Arms, Magick"), new Point(0, 0), new Rectangle(0, 0, 370, 79));
            skillDescBack = new StaticSprite(cm.Load<Texture2D>("Menu/Battle Menu/Battle HUD - Arms, Magick"), new Point(0, 0), new Rectangle(395, 0, 372, 79));
             */

            Point origin = new Point(22, 3);
            foreach(Pair<Button, string> alpha in new Pair<Button, string>[] { new Pair<Button, string>(item, "Items"), new Pair<Button, string>(journal, "Journal"), new Pair<Button, string>(stat, "Stats"), new Pair<Button, string>(skill, "Skills"), new Pair<Button, string>(quit, "Quit") }) {
                Texture2D tex = cm.Load<Texture2D>("Menu/Top Menu/Top Menu - " + alpha.Item2);

                alpha.Item1.Hover = new StaticSprite(tex, origin);
                alpha.Item1.Idle = new StaticSprite(tex, origin);
                alpha.Item1.Idle.Tint = new Color(200, 200, 200);
                alpha.Item1.Press = new StaticSprite(tex, origin);
                alpha.Item1.Press.Tint = new Color(175, 175, 175);
            }

            Texture2D tabfb = cm.Load<Texture2D>("Menu/Top Menu/TabFwd-Back");
            pktButtons[0].Hover = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(108, 0, 13, 19));
            pktButtons[0].Idle = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(108, 0, 13, 19));
            pktButtons[0].Idle.Tint = new Color(200, 200, 200);
            pktButtons[0].Press = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(108, 0, 13, 19));
            pktButtons[0].Press.Tint = new Color(175, 175, 175);
            Texture2D tab = cm.Load<Texture2D>("Menu/Top Menu/Tab");
            for(int i = 1; i < 5; i++) {
                pktButtons[i].Hover = new StaticSprite(tab);
                pktButtons[i].Idle = new StaticSprite(tab);
                pktButtons[i].Idle.Tint = new Color(200, 200, 200);
                pktButtons[i].Press = new StaticSprite(tab);
                pktButtons[i].Press.Tint = new Color(175, 175, 175);
            }
            pktButtons[5].Hover = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(0, 0, 13, 19));
            pktButtons[5].Idle = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(0, 0, 13, 19));
            pktButtons[5].Idle.Tint = new Color(200, 200, 200);
            pktButtons[5].Press = new StaticSprite(tabfb, new Point(0, 0), new Rectangle(0, 0, 13, 19));
            pktButtons[5].Press.Tint = new Color(175, 175, 175);

            Texture2D block = cm.Load<Texture2D>("Menu/Top Menu/Item Block");
            for(int i = 0; i < 20; i++) {
                itemButtons[i].Btn.Hover = new StaticSprite(block);
                itemButtons[i].Btn.Idle = new StaticSprite(block);
                itemButtons[i].Btn.Idle.Tint = new Color(200, 200, 200);
                itemButtons[i].Btn.Press = new StaticSprite(block);
                itemButtons[i].Btn.Press.Tint = new Color(175, 175, 175);
            }
        }

        public void Update(GameTime gt) {
            menu.Update(gt);
            StaticVars.player.Ctrls.Update(gt);

            MouseState ms = Mouse.GetState();
            foreach(Button alpha in pktButtons) {
                alpha.Update(ms, gt);
            }
            for(int i = 1; i < 5; i++) {
                pktButtons[i].IsHovering = pktCenter + i - 2 == currPkt;
            }
            foreach(ItemButton alpha in itemButtons) {
                alpha.Btn.Update(ms, gt);
            }

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
                if(invSlide == 1f) {
                    int numPkts = StaticVars.player.Stats.Inv.Pockets.Count;
                    if(numPkts > 4 && !(pktButtons[0].Visible || pktButtons[5].Visible)) {
                        pktButtons[5].Visible = true;
                        pktButtons[5].Active = true;
                    }
                    if(numPkts > 3 && !(pktButtons[4].Visible)) {
                        pktButtons[4].Visible = true;
                        pktButtons[4].Active = true;
                    }
                    if(numPkts > 2 && !(pktButtons[3].Visible)) {
                        pktButtons[3].Visible = true;
                        pktButtons[3].Active = true;
                    }
                    if(numPkts > 1 && !(pktButtons[2].Visible)) {
                        pktButtons[2].Visible = true;
                        pktButtons[2].Active = true;
                    }
                    if(numPkts > 0 && !(pktButtons[1].Visible)) {
                        pktButtons[1].Visible = true;
                        pktButtons[1].Active = true;
                    }

                    if(StaticVars.player.Stats.Inv.Pockets[currPkt] != null) {
                        for(int i = 0; i < 20; i++) {
                            itemButtons[i].Btn.Visible = true;
                            itemButtons[i].Btn.Active = true;
                        }
                    }
                } else {
                    foreach(Button alpha in pktButtons) {
                        alpha.Visible = false;
                        alpha.Active = false;
                    }
                    for(int i = 0; i < 20; i++) {
                        itemButtons[i].Btn.Visible = false;
                        itemButtons[i].Btn.Active = false;
                    }
                }

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

            MouseState ms = Mouse.GetState();
            if(ms.LeftButton == ButtonState.Released) {
                Resources.curs.Draw(sb, new Point(ms.X, ms.Y));
            } else if(currDragButton != -1) {
                Resources.cursDrag.Draw(sb, new Point(ms.X, ms.Y));
            } else {
                Resources.cursClick.Draw(sb, new Point(ms.X, ms.Y));
            }

            if(invSlide != 0.00f) {
                invBack.Draw(sb, new Point(800 - (int)(600f * invSlide), -3));

                for(int i = 0; i < 6; i++) {
                    Button alpha = pktButtons[i];
                    if(alpha.Visible) {
                        alpha.Draw(sb);
                        if(i != 0 && i != 5) sb.DrawString(Resources.fonts["04b03s"], "Pocket " + (pktCenter + i - 1), new Vector2(alpha.Area.X + 10, alpha.Area.Y + 4), new Color(200, 200, 200));
                    }
                }

                DarosGame.Inventory.Pocket pkt = StaticVars.player.Stats.Inv.Pockets[currPkt];
                for(int i = 0; i < 20; i++) {
                    Button alpha = itemButtons[i].Btn;
                    alpha.Draw(sb);
                    if(pkt[i] != null && invSlide == 1f) {
                        sb.DrawString(Resources.fonts["04b03s"], i + ": " + pkt[i].Name, new Vector2(alpha.Area.X + 10, alpha.Area.Y + 6), new Color(200, 200, 200));
                    }
                }
            }

            if(statSlide != 0.00f) {
                // N/A
            }

            if(skillSlide != 0.00f) {
                // N/A
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

        private class ItemButton {
            private Button btn;
            private int i;

            public Button Btn {
                get { return btn; }
            }

            public ItemButton(int i, ADAMenu inst) {
                this.i = i;
                btn = new Button();
                btn.Area = new Rectangle(244 + (i > 9 ? 240 : 0), 90 + ((i % 10) * 31), 234, 24);

                btn.OnMouseDown = delegate {
                    if(inst.CDB == -1) {
                        inst.CDB = i;
                    }
                };

                btn.OnMouseUp = delegate {
                    if(inst.CDB != i) {
                        DarosGame.Inventory.Pocket pkt = inst.CurrPkt;
                        Item.Item temp = pkt[i];
                        pkt[i] = pkt[inst.CDB];
                        pkt[inst.CDB] = temp;
                        temp = null;
                    }
                    inst.CDB = -1;
                };
            }
        }
    }
}
