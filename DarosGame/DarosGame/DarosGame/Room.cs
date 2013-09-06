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
        protected Texture2D wallDef;

        protected Dictionary<Rectangle, Pair<Room, Point>> exits = new Dictionary<Rectangle, Pair<Room, Point>>();

        public Rectangle Size {
            get { return background.Size; }
        }

        public List<GameObject> Objects {
            get { return objs; }
        }

        public Room() {
            PostProcessing.Add((IRequireResource)this);
            PostProcessing.Add((INeedMoreInit)this);
        }

        /// <summary>
        /// Draws the room.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw with</param>
        public void Draw(SpriteBatch sb) {
            background.Draw(sb, new Point(-StaticVars.Camera.X, -StaticVars.Camera.Y));
        }

        public void Add(GameObject go) {
            objs.Add(go);
        }

        public bool CollidingWithWall(GameObject go) {
            if(!new Rectangle(0, 0, wallDef.Width, wallDef.Height).Contains(go.CollisionBox)) {
                return true;
            }
            Color[] pixels = new Color[go.CollisionBox.Width * go.CollisionBox.Height];
            wallDef.GetData<Color>(0, go.CollisionBox, pixels, 0, pixels.Length);
            foreach(Color alpha in pixels) {
                if(alpha == Color.White) {
                    return true;
                }
            }
            foreach(GameObject alpha in objs) {
                if(go != alpha && go.CollisionBox.Intersects(alpha.CollisionBox)) {
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
    }

    public class TestRoom : Room {

        public TestRoom() : base() {
            // Registers the room as valid in Room Registry
            RegisterRoom("Test Room", this);

            // Adding Game Objects to room
            Add(new SceneryGameObjects.Sign(new Point(749,601)));

            // Have Game Objects in room sorted by Y value
            objs.Sort();
        }

        public override void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            // Define the resource used as background
            background = new StaticSprite(cm.Load<Texture2D>("test/floor"));
            // Defines the Tex2D that is used to check for walls
            wallDef = cm.Load<Texture2D>("test/Test Room Red");
        }

        public override void FinalizeInit() {
            // Define the exits of this room
            exits.Add(new Rectangle(535, 538, 97, 44), new Pair<Room, Point>(Room.GetRoom("DLRoom1"), new Point(593, 884)));
        }
    }

    namespace DerelictLaboratory {
        public class Room1 : Room {
            public Room1() : base() {
                RegisterRoom("DLRoom1", this);

                objs.Sort();
            }

            public override void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
                background = new StaticSprite(cm.Load<Texture2D>("Locations/Derelict Laboratory/Room 1"));
                wallDef = cm.Load<Texture2D>("Locations/Derelict Laboratory/Room 1 Walls");
            }

            public override void FinalizeInit() {
                exits.Add(new Rectangle(516, 918, 169, 41), new Pair<Room, Point>(Room.GetRoom("Test Room"), new Point(582, 598)));
            }
        }
    }
}



/* //TL = Top Left. BR = Bottom Right.

new Rectangle(0,0,1184,317)
new Rectangle(780,316,404,26)
new Rectangle(889,342,295,7)
new Rectangle(933,349,251,8)
new Rectangle(1004,357,180,198)
new Rectangle(1166,555,18,8)
new Rectangle(1175,563,9,19)
new Rectangle(938,357,66,6)
new Rectangle(974,363,30,12)
TL 982,376 BR 
TL 997,385 BR 
TL 1006,397 BR 
TL 1024,408 BR 
TL 1034,417 BR 
TL 1049.425 BR 
TL 1057,343 BR 
TL 1074,446 BR 



*/