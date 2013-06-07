using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DarosGame {
    namespace SceneryGameObjects {
        public class Sign : SimpleGameObject, IConversable {

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
                // Interaction to come soon
            }
        }
    }
}
