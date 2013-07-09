using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarosGame {
    namespace Enemy {
        public class EnemyType {
            private int mhp, def;

            public delegate void TakeTurn(Battle.BattleInstance bi);
            public TakeTurn tt;

            public int MaxHP {
                get { return mhp; }
            }
            public int Def {
                get { return def; }
            }

            public EnemyType(int mhp, int def) {
                this.mhp = mhp;
                this.def = def;
            }

            public void Turn(Battle.BattleInstance bi) {
                tt(bi);
            }

        }

        public class EnemyInstance : Battle.BattleMob {
            private int hp;
            private EnemyType et;
            public EnemyInstance(EnemyType type) {
                et = type;
                hp = et.MaxHP;
            }

            public override void Update(Battle.BattleInstance bi, Microsoft.Xna.Framework.GameTime gt) {
                if(HasTurn) {
                    et.Turn(bi);
                }
            }
        }
    }
}
