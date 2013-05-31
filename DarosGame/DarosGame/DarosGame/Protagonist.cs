using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using StickXNAEngine.Utility;
using StickXNAEngine.Graphic;

namespace DarosGame {
    public class Protagonist : Mob {
        private Controls ctrls;

        public Protagonist() {
            ctrls = new Controls();

            location = new Point(588, 696);
        }

        public override void Update(GameTime gt) {
            ctrls.Update(gt);

            Direction dir = ctrls.Movement;
            if(dir != Direction.DENNIS) {
                facing = dir;
                walking = true;
            } else {
                walking = false;
            }

            if(walking) {
                int speed = 3;
                if(facing == Direction.NORTH || facing == Direction.NORTHEAST || facing == Direction.NORTHWEST) {
                    location.Y -= speed;
                }
                if(facing == Direction.SOUTH || facing == Direction.SOUTHEAST || facing == Direction.SOUTHWEST) {
                    location.Y += speed;
                }

                if(facing == Direction.WEST || facing == Direction.NORTHWEST || facing == Direction.SOUTHWEST) {
                    location.X -= speed;
                }
                if(facing == Direction.EAST || facing == Direction.NORTHEAST || facing == Direction.SOUTHEAST) {
                    location.X += speed;
                }
            }

            foreach(Direction alpha in walk.Keys) {
                ((AnimateSprite)walk[alpha]).Update(gt);
            }
        }

        public override void LoadRes(ContentManager cm) {
            TimeSpan delay = new TimeSpan(1050000);

            Dictionary<String, Direction> dirs = new Dictionary<string, Direction>();
            dirs["N"] = Direction.NORTH;
            dirs["NE"] = Direction.NORTHEAST;
            dirs["E"] = Direction.EAST;
            dirs["SE"] = Direction.SOUTHEAST;
            dirs["S"] = Direction.SOUTH;
            dirs["SW"] = Direction.SOUTHWEST;
            dirs["W"] = Direction.WEST;
            dirs["NW"] = Direction.NORTHWEST;

            foreach(string s in dirs.Keys) {
                AnimateSprite walker = new AnimateSprite(delay);

                Texture2D stand = cm.Load<Texture2D>("protag\Overworld\arms\stand - " + s);
                Texture2D walkL = cm.Load<Texture2D>("protag\Overworld\arms\walk" + s + " L"), walkR = cm.Load<Texture2D>("protag\Overworld\arms\walk" + s + " R");

                walker.Add(new StaticSprite(stand, new Point(35, 102)));
                walker.Add(new StaticSprite(walkL, new Point(35, 103)));
                walker.Add(new StaticSprite(walkL, new Point(35, 101)));
                walker.Add(new StaticSprite(stand, new Point(35, 102)));
                walker.Add(new StaticSprite(walkR, new Point(35, 103)));
                walker.Add(new StaticSprite(walkR, new Point(35, 101)));

                this.walk[dirs[s]] = walker;
                this.stand[dirs[s]] = new StaticSprite(stand, new Point(35, 102));
            }
        }
    }
}
