using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarosGame {
    namespace Enemy {
        public abstract class EnemyType {
            public abstract int MaxHP {
                get;
            }

            public abstract int Def {
                get;
            }

            public abstract EnemyInstance CreateInstance(Battle.BattleInstance bi);
        }

        public class EnemyInstance {
            private int hp;
            public EnemyInstance() {
            }
        }
    }
}
