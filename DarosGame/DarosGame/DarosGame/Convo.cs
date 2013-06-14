using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;
using StickXNAEngine.Utility;

namespace DarosGame {
    namespace Convo {
        public static class Conversation {
            public static Blurb curr;

            public static void Draw(SpriteBatch sb) {
                curr.Draw(sb);
            }

            public static Dictionary<string, Sprite> ports = new Dictionary<string, Sprite>();

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

            private string show;
            private TimeSpan timer = new TimeSpan(0);

            public Blurb() {
                PostProcessing.Add((IUpdating)this);
            }

            public void Draw(SpriteBatch sb) {

            }

            public void Update(Microsoft.Xna.Framework.GameTime gt) {
                timer += gt.ElapsedGameTime;
                if(timer > EZTweakVars.CharDelay) {
                    timer -= EZTweakVars.CharDelay;

                    if(show.Length != message.Length) {
                        show += message.ElementAt<char>(show.Length);
                    }
                }
            }
        }

        public abstract class LinearBlurb : Blurb, INeedMoreInit {
            protected Blurb next;

            public LinearBlurb() {
                PostProcessing.Add((INeedMoreInit)this);
            }

            public void FinalizeInit();
        }

        public abstract class BranchingBlurb : Blurb, INeedMoreInit {
            protected Dictionary<string, Blurb> choice = new Dictionary<string,Blurb>();

            public BranchingBlurb() {
                PostProcessing.Add((INeedMoreInit)this);
            }

            public void FinalizeInit();
        }
    }
}
