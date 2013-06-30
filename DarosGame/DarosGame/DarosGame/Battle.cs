using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarosGame {
    namespace Battle {
        public class BattleInstance {
            private Song theme;

            private List<BattleMob> turnOrder;

            public BattleInstance(List<BattleMob> turnOrder) {
                this.turnOrder = turnOrder;
                
            }

            public void Update(GameTime gt) {

            }

            public void Draw(SpriteBatch sb) {

            }
        }

        public abstract class BattleMob {
            private int x, y;
        }

        public class BattleProtag : BattleMob {
            private ProtagStats stats;

            public BattleProtag(ProtagStats stats) {
                this.stats = stats;
            }
        }
    }
}
