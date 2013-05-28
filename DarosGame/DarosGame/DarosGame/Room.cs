using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using StickXNAEngine.Graphic;
using Microsoft.Xna.Framework;
using StickXNAEngine.Utility;

namespace DarosGame {
    public abstract class Room : IRequireResource, INeedMoreInit {
        protected StaticSprite background;
        protected List<GameObject> objs = new List<GameObject>();
        protected List<Rectangle> walls = new List<Rectangle>();

        protected Dictionary<Rectangle, Pair<Room, Point>> exits = new Dictionary<Rectangle, Pair<Room, Point>>();

        public Room() {
            PostProcessing.Add((IRequireResource)this);
            PostProcessing.Add((INeedMoreInit)this);
        }

        /// <summary>
        /// Draws the room.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw with</param>
        /// <param name="camera">A point defining the top left pixel of where the camera is</param>
        public void Draw(SpriteBatch sb, Point camera) {
            background.Draw(sb, new Point(-camera.X, -camera.Y));
        }

        public void Add(GameObject go) {
            objs.Add(go);
        }

        public bool CollidingWithWall(GameObject go) {
            foreach(Rectangle alpha in walls) {
                if(go.CollisionBox.Intersects(alpha)) {
                    return true;
                }
            }
            return false;
        }

        public Pair<Room, Point> Exit(GameObject go) {
            foreach(Rectangle alpha in exits.Keys) {
                if(go.CollisionBox.Intersects(alpha)) {
                    return exits[alpha];
                }
            }
            return null;
        }

        public abstract void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm);
        public abstract void FinalizeInit();



        private static Dictionary<string, Room> roomRegistry = new Dictionary<string, Room>();

        public static void RegisterRoom(String name, Room room) {
            roomRegistry[name] = room;
        }

        public static Room GetRoom(String name) {
            try {
                return roomRegistry[name];
            } catch(KeyNotFoundException) {
                return null;
            }
        }


        /* // Nowhere close to done.;
        public static List<Rectangle> MakeCurve(Rectangle loc, Direction corner) {
            List<Rectangle> rtn = new List<Rectangle>();
            
            loc.Width = loc.Height = Math.Max(loc.Width, loc.Height);

            int x = loc.X, y = loc.Y, width = loc.Width, height = loc.Height / 10;
            int x2 = loc.X + loc.Width - height;
            while(rtn.Count < 7) {
                rtn.Add(new Rectangle(x, y, width, height));
                rtn.Add(new Rectangle(x2, y + width, height, width - height));

            }

            return rtn;
        }
        */
    }

    public class TestRoom : Room {

        public TestRoom() : base() {
            // Registers the room as valid in Room Registry
            RegisterRoom("Test Room", this);

            // Main walls
            walls.Add(new Rectangle(1023, 0, 377, 1300));
            walls.Add(new Rectangle(952, 1111, 71, 189));
            walls.Add(new Rectangle(824, 1127, 128, 173));
            walls.Add(new Rectangle(0, 1111, 824, 189));
            walls.Add(new Rectangle(0, 0, 374, 1111));
            walls.Add(new Rectangle(373, 0, 162, 582));
            walls.Add(new Rectangle(535, 0, 97, 476));
            walls.Add(new Rectangle(632, 0, 391, 582));

            // BL big corner
            walls.Add(new Rectangle(374, 1077, 3, 34));
            walls.Add(new Rectangle(377, 1087, 5, 24));
            walls.Add(new Rectangle(382, 1095, 5, 16));
            walls.Add(new Rectangle(387, 1108, 21, 3));
            walls.Add(new Rectangle(387, 1104, 11, 4));
            walls.Add(new Rectangle(387, 1100, 5, 4));

            // TR big corner
            walls.Add(new Rectangle(989, 582, 34, 3));
            walls.Add(new Rectangle(1020, 585, 3, 31));
            walls.Add(new Rectangle(997, 585, 23, 5));
            walls.Add(new Rectangle(1015, 590, 5, 18));
            walls.Add(new Rectangle(1002, 590, 13, 7));
            walls.Add(new Rectangle(1008, 597, 7, 6));
        }

        public override void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            // Define the resource used as background
            background = new StaticSprite(cm.Load<Texture2D>("test/floor"));
        }

        public override void FinalizeInit() {
            throw new NotImplementedException();
        }
    }
}
