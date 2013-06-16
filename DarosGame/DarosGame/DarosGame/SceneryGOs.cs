using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace DarosGame {
    namespace SceneryGameObjects {
        public class Sign : SimpleGameObject, IConversable, ISpecificFacing {
<<<<<<< HEAD
            private Convo.SimpleBlurb conv = new Convo.SimpleBlurb("This is a sign.  Whoop-de-friggin'-do.  Redundancy check for the win.  Gimme some other random bullshit right now.  Lorem ipsum or something like that.  I don't even know.  This is all just testing.");
=======
            private Convo.SimpleBlurb conv = new Convo.SimpleBlurb("This is a sign. Have me have to tell you that is pretty redundant.");
>>>>>>> c89fe563b50de36d4d2d6f7948c809554ba8a8f3

            public Sign(Point loc) {
                location = loc;
                width = 50;
                height = 30;
            }

            public override void Update(GameTime gt) {
                // Does not update.
            }

            public override void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
                sprite = new StickXNAEngine.Graphic.StaticSprite(cm.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("Scenery/Signs/sign 1"), new Point(34, 58));
            }

            public void Interact() {
                Convo.Conversation.curr = conv;
            }

            public bool RightFacing(Direction dir) {
                return dir == Direction.NORTH || dir == Direction.NORTHEAST || dir == Direction.NORTHWEST;
            }
        }
    }
}
