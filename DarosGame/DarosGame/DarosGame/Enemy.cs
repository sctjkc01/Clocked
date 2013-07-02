using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarosGame {
    namespace Enemy {
        public abstract class EnemyType {
            public static abstract int MaxHP {
                get;
            }

            public static abstract int Def {
                get;
            }

            public static abstract EnemyInstance CreateInstance(Battle.BattleInstance bi);
        }

        public class EnemyInstance {
            private int hp;
            public EnemyInstance() {
            }
        }
    }
}
