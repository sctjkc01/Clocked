using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace DarosGame {
    namespace SceneryGameObjects {
        public class Sign : SimpleGameObject, IConversable, ISpecificFacing {
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
                Convo.Conversation.curr = new Convo.VolatileBlurb("This is a sign.  Having me have to tell you that is pretty redundant.");
                Convo.Conversation.curr.Next = new Convo.VolatileBlurb("Then again, this is testing, after all.  What're you gonna do about it?");
            }

            public bool RightFacing(Direction dir) {
                return dir == Direction.NORTH || dir == Direction.NORTHEAST || dir == Direction.NORTHWEST;
            }
        }
    }
}
