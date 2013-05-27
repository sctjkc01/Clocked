using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace StickXNAEngine.Utility {
    public static class PostProcessing {
        public static List<INeedMoreInit> moreInit = new List<INeedMoreInit>();
        public static List<IRequireResource> reqRes = new List<IRequireResource>();

        public static void Res(ContentManager cm) {
            foreach(IRequireResource alpha in reqRes) {
                alpha.LoadRes(cm);
            }
        }

        public static void Init() {
            foreach(INeedMoreInit alpha in moreInit) {
                alpha.FinalizeInit();
            }
        }
    }
}
