using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StickXNAEngine.Utility {
    public class Pair<T1, T2> {
        T1 i1; T2 i2;

        public T1 Item1 {
            get { return i1; }
            set { i1 = value; }
        }

        public T2 Item2 {
            get { return i2; }
            set { i2 = value; }
        }

        public Pair(T1 i1, T2 i2) {
            this.i1 = i1;
            this.i2 = i2;
        }
    }
}
