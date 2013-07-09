using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;

namespace DarosGame {
    namespace Battle {
        public class BattleInstance {
            private Song theme;
            private Sprite background;
            private List<BattleMob> turnOrder;
            private int curr = 0;

            private BattleProtag bp;

            public BattleProtag Protag {
                get { return bp; }
            }

            public BattleInstance(List<BattleMob> turnOrder, Sprite background)
                : this(turnOrder, background, Resources.songs["genbattle"]) { }

            public BattleInstance(List<BattleMob> turnOrder, Sprite background, Song theme) {
                this.turnOrder = turnOrder;
                this.background = background;
                this.theme = theme;

                foreach(BattleMob alpha in turnOrder) {
                    if(alpha is BattleProtag) {
                        bp = (BattleProtag)alpha;
                    }
                }
                if(bp == null) {
                    throw new Exception("Protagonist not in turn order!");
                }
            }

            public void Update(GameTime gt) {
                if(!theme.Playing) theme.Playing = true;
                if(background is AnimateSprite) ((AnimateSprite)background).Update(gt);
                
            }

            public void NextTurn() {
                turnOrder[curr].HasTurn = false;
                curr++;
                turnOrder[curr].HasTurn = true;
            }

            public void Draw(SpriteBatch sb) {
                background.Draw(sb, new Point(0, 0));
            }
        }

        public abstract class BattleMob {
            protected int x, y;
            private bool hasTurn = false;

            public bool HasTurn {
                get { return hasTurn; }
                set { hasTurn = value; }
            }

            public abstract void Update(BattleInstance bi, GameTime gt);
        }

        public class BattleProtag : BattleMob {
            private ProtagStats stats;

            public BattleProtag(ProtagStats stats) {
                this.stats = stats;
            }

            public override void Update(BattleInstance bi, GameTime gt) {
                
            }
        }
    }
}
