using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;

namespace DarosGame {
    namespace Convo {
        public class Conversation {
            Blurb curr;

            public void Draw(SpriteBatch sb) {

            }


        }

        public abstract class Blurb {
            /// <summary>
            /// A resource path to the portrait of the speaker of this blurb.
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

            public void Draw(SpriteBatch sb) {

            }
        }

        public class LinearBlurb : Blurb {
            Blurb next;
        }

        public class BranchingBlurb : Blurb { }
    }
}
