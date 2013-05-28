using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;
using StickXNAEngine.Utility;
using Microsoft.Xna.Framework.Content;

namespace DarosGame {
    public abstract class GameObject : IUpdating, IRequireResource {
        protected Point location;
        protected Rectangle collisionBox;

        public Rectangle CollisionBox {
            get { return collisionBox; }
        }

        public abstract void Draw(SpriteBatch sb);

        public abstract void Update(GameTime gt);
        public abstract void LoadRes(ContentManager cm);
    }

    public abstract class SimpleGameObject : GameObject {
        protected StaticSprite sprite;

        public override void Draw(SpriteBatch sb) {
            sprite.Draw(sb, location);
        }
    }

    public abstract class MultistateGameObject : GameObject {
        protected StaticSprite currentSprite;

        public override void Draw(SpriteBatch sb) {
            currentSprite.Draw(sb, location);
        }
    }

    public interface IInteractive {
        void Interact();
    }
}
