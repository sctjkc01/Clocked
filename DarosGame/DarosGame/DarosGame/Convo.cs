using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;
using StickXNAEngine.Utility;

namespace DarosGame {
    namespace Convo {
        public static class Conversation {
            public static Blurb curr = null;

            public static uint nextId = 0;

            public static void Draw(SpriteBatch sb) {
                if(curr != null) {
                    curr.Draw(sb);
                }
            }

            public static uint RegisterBlurb(Blurb alpha) {
                blurbs[nextId] = alpha;
                nextId++;
                return nextId - 1;
            }

            public static Dictionary<string, Sprite> ports = new Dictionary<string, Sprite>();
            public static Dictionary<uint, Blurb> blurbs = new Dictionary<uint, Blurb>();
            public static StaticSprite top, bottom, nameplate;

            public static void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {

                // Protagonist: Idle
                AnimateSprite atemp = new AnimateSprite(new TimeSpan(1050000));
                for(int i = 1; i < 5; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/Protag - Arms/Talk Portrait - Protag - Arms - Idle " + i));
                    atemp.Add(stemp);
                }
                for(int i = 0; i < 20; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/Protag - Arms/Talk Portrait - Protag - Arms - Idle 4"));
                    atemp.Add(stemp);
                }
                ports["pIdle"] = atemp;

                // Protagonist: Happy
                atemp = new AnimateSprite(new TimeSpan(1050000));
                for(int i = 1; i < 9; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/Protag - Arms/Talk Portrait - Protag - Arms - Talk Happy " + i));
                    atemp.Add(stemp);
                }
                ports["pHappy"] = atemp;

                // Flat Lizard: Idle
                atemp = new AnimateSprite(new TimeSpan(1050000));
                for(int i = 1; i < 6; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Flat Lizard - Idle " + i));
                    atemp.Add(stemp);
                }
                for(int i = 0; i < 20; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Flat Lizard - Idle 5"));
                    atemp.Add(stemp);
                }
                ports["flatlizIdle"] = atemp;

                // Flat Lizard: Talk
                atemp = new AnimateSprite(new TimeSpan(1050000));
                atemp.Add(new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Flat Lizard - Idle 1")));
                for(int i = 1; i < 4; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Flat Lizard - Talk " + i));
                    atemp.Add(stemp);
                }
                atemp.Add(new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Flat Lizard - Talk 2")));
                ports["flatlizTalk"] = atemp;

                // Lumisnail 1 & 2
                for(int n = 1; n < 3; n++) {
                    atemp = new AnimateSprite(new TimeSpan(1050000));
                    for(int i = 1; i < 5; i++) {
                        StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Lumisnail " + n + " - All " + i));
                        atemp.Add(stemp);
                    }
                    ports["lumisnail" + n] = atemp;
                }

                // Mantis
                atemp = new AnimateSprite(new TimeSpan(1050000));
                for(int i = 1; i < 5; i++) {
                    StaticSprite stemp = new StaticSprite(cm.Load<Texture2D>("Talking/NPCs/Tiny Animals/Talk Portrait - NPC - Mantis - All " + i));
                    atemp.Add(stemp);
                }
                ports["mantis"] = atemp;

                top = new StaticSprite(cm.Load<Texture2D>("Talking/Talk Box U"));
                bottom = new StaticSprite(cm.Load<Texture2D>("Talking/Talk Box D"));
                nameplate = new StaticSprite(cm.Load<Texture2D>("Talking/Talk Nameplate"));
            }
        }

        public abstract class Blurb : IUpdating {
            /// <summary>
            /// The key of which Sprite to use for the portrait.
            /// </summary>
            protected string img;
            /// <summary>
            /// The speaker's name - will not show if empty ("") or null, or if player does not have necessary tool.
            /// </summary>
            protected string name;
            /// <summary>
            /// The message being said.
            /// </summary>
            protected string message;
            /// <summary>
            /// Is the message on the top or the bottom?
            /// </summary>
            protected bool top = false;
            /// <summary>
            /// Should the message be shown all at once, immediately?
            /// </summary>
            protected bool showAll = false;

            private string show = "";
            private TimeSpan timer = new TimeSpan(0);

            /// <summary>
            /// Show the nameplate if speaker has a name (not null, not empty), and the player has the ADA.
            /// </summary>
            private Boolean ShowNameplate {
                get { return name != null && name != "" && StaticVars.HaveADA; }
            }

            public Boolean ShowingAll {
                get { return show.Length == message.Length; }
            }

            private String Image { set { img = value; } }
            private String Name { set { name = value; } }
            private String Message { set { message = value; } }
            private Boolean IsTop { set { top = value; } }
            private Boolean DisplayAll { set { showAll = value; } }

            public abstract Blurb Next {
                get;
            }

            public Blurb() {
                PostProcessing.Add((IUpdating)this);
                if(showAll) ShowAll();
            }

            public virtual void Draw(SpriteBatch sb) {
                Vector2 pos = DrawBack(sb);

                List<String> words = show.Split(' ').ToList<String>();
                float space = Resources.font.MeasureString(" ").X;
                foreach(String word in words) {
                    pos.X += Resources.font.MeasureString(word).X;
                    if(pos.X > 770) {
                        pos.X = 205 + Resources.font.MeasureString(word).X + space;
                        pos.Y += Resources.font.LineSpacing;
                        sb.DrawString(Resources.font, word, new Vector2(205, pos.Y), Color.White);
                    } else {
                        sb.DrawString(Resources.font, word, new Vector2(Math.Max(pos.X - Resources.font.MeasureString(word).X, 205), pos.Y), Color.White);
                        pos.X += space;
                    }
                }
            }

            protected Vector2 DrawBack(SpriteBatch sb) {
                Vector2 pos = new Vector2(-4, 0);
                if(top) {
                    Conversation.top.Draw(sb, new Point(-4, 0));
                    try { Conversation.ports[img].Draw(sb, new Point(19, 43)); } catch(KeyNotFoundException) { }

                    if(ShowNameplate) {
                        Conversation.nameplate.Mirror = SpriteEffects.None;
                        Conversation.nameplate.Draw(sb, new Point(0, 191));
                        sb.DrawString(Resources.font, name, new Vector2(33, 202), Color.White);
                    }
                } else {
                    Conversation.bottom.Draw(sb, new Point(-4, 403));
                    try { Conversation.ports[img].Draw(sb, new Point(19, 446)); } catch(KeyNotFoundException) { }
                    pos += new Vector2(0, 403);

                    if(ShowNameplate) {
                        Conversation.nameplate.Mirror = SpriteEffects.FlipVertically;
                        Conversation.nameplate.Draw(sb, new Point(0, 365));
                        sb.DrawString(Resources.font, name, new Vector2(33, 376), Color.White);
                    }
                }
                return pos + new Vector2(205, 42);
            }

            public void Update(GameTime gt) {
                if(Conversation.curr != this && !showAll) {
                    show = "";
                }
                if(Conversation.curr == this && show.Length != message.Length) {
                    timer += gt.ElapsedGameTime;
                    if(timer > EZTweakVars.CharDelay) {
                        timer -= EZTweakVars.CharDelay;
                        show += message.ElementAt<char>(show.Length);
                    }
                }
            }

            public void ShowAll() {
                show = message;
            }
        }

        public abstract class LinearBlurb : Blurb, INeedMoreInit {
            protected Blurb next = null;

            public LinearBlurb() {
                PostProcessing.Add((INeedMoreInit)this);
            }

            public override Blurb Next {
                get { return next; }
            }

            public abstract void FinalizeInit();
        }

        public class SimpleBlurb : LinearBlurb {
            uint nextid = 0;

            public SimpleBlurb(string msg) : this("", msg, null, false, false, 0) { }

            public SimpleBlurb(string img, string msg, string name) : this(img, msg, name, false, false, 0) { }

            public SimpleBlurb(string img, string msg, string name, bool top, bool showall) : this(img, msg, name, top, showall, 0) { }

            public SimpleBlurb(string img, string msg, string name, bool top, bool showall, uint nextid) {
                this.img = img;
                this.message = msg;
                this.name = name;
                this.top = top;
                this.showAll = showall;
                this.nextid = nextid;
            }

            public override void FinalizeInit() {
                if(nextid != 0) {
                    next = Conversation.blurbs[nextid];
                }
            }
        }

        public abstract class BranchingBlurb : Blurb, INeedMoreInit {
            protected Dictionary<string, Blurb> choice = new Dictionary<string,Blurb>();

            public BranchingBlurb() {
                PostProcessing.Add((INeedMoreInit)this);
            }

            public override Blurb Next {
                get { throw new NotImplementedException(); }
            }

            public abstract void FinalizeInit();
        }
    }
}
