using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace StickXNAEngine.Utility {
    public static class PostProcessing {
        public static List<INeedMoreInit> moreInit = new List<INeedMoreInit>();
        public static List<IRequireResource> reqRes = new List<IRequireResource>();

        public static List<IUpdating> updating = new List<IUpdating>();

        public static void Add(INeedMoreInit alpha) {
            moreInit.Add(alpha);
        }

        public static void Add(IRequireResource alpha) {
            reqRes.Add(alpha);
        }

        public static void Add(IUpdating alpha) {
            updating.Add(alpha);
        }

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

        public static void Update(GameTime gt) {
            foreach(IUpdating alpha in updating) {
                alpha.Update(gt);
            }
        }
    }
}
