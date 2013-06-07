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
                Console.WriteLine("Sign interacted with!"); // Test Code
            }

            public bool RightFacing(Direction dir) {
                return dir == Direction.NORTH || dir == Direction.NORTHEAST || dir == Direction.NORTHWEST;
            }
        }
    }
}
