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

        private ProtagStats stats;

        public ProtagStats Stats {
            get { return stats; }
        }

        public Controls Ctrls {
            get { return ctrls; }
        }

        public Protagonist() {
            ctrls = new Controls();

            stats = new ProtagStats();

            Stats.MaxHP = 150;
            Stats.MaxMP = 75;
            Stats.HP = 150;
            Stats.MP = 75;

            //Testing code!
            Stats.Inv.AddPocket();
            //</test>

            width = 55; height = 10;
        }

        public override void Update(GameTime gt) {
            ctrls.Update(gt);

            if(Convo.Conversation.curr == null) { // <noconv>
                Direction dir = ctrls.Movement;
                if(dir != Direction.DENNIS) {
                    facing = dir;
                    walking = true;
                } else {
                    walking = false;
                }

                if(walking) {
                    int speed = 3;
                    Boolean n = false, s = false, e = false, w = false;
                    if(facing == Direction.NORTH || facing == Direction.NORTHEAST || facing == Direction.NORTHWEST) {
                        location.Y -= speed;
                        n = true;
                    }
                    if(facing == Direction.SOUTH || facing == Direction.SOUTHEAST || facing == Direction.SOUTHWEST) {
                        location.Y += speed;
                        s = true;
                    }

                    if(facing == Direction.WEST || facing == Direction.NORTHWEST || facing == Direction.SOUTHWEST) {
                        location.X -= speed;
                        w = true;
                    }
                    if(facing == Direction.EAST || facing == Direction.NORTHEAST || facing == Direction.SOUTHEAST) {
                        location.X += speed;
                        e = true;
                    }

                    //Check collisions with walls.  If we're colliding, Undo!
                    bool colliding = StaticVars.CurrRoom.CollidingWithWall(this);
                    if(colliding) {
                        if(colliding && n) {
                            location.Y += speed;
                        }
                        if(colliding && s) {
                            location.Y -= speed;
                        }
                        colliding = StaticVars.CurrRoom.CollidingWithWall(this);
                        if(colliding && n) {
                            location.Y -= speed;
                        }
                        if(colliding && s) {
                            location.Y += speed;
                        }
                        if(colliding && e) {
                            location.X -= speed;
                        }
                        if(colliding && w) {
                            location.X += speed;
                        }
                        colliding = StaticVars.CurrRoom.CollidingWithWall(this);
                        if(colliding && n) {
                            location.Y += speed;
                        }
                        if(colliding && s) {
                            location.Y -= speed;
                        }
                    }

                    //Check if in an exit.
                    Pair<Room, Point> exit = StaticVars.CurrRoom.Exit(this);
                    if(exit != null) {
                        StaticVars.Exit = exit;
                    }

                    foreach(Direction alpha in walk.Keys) {
                        ((AnimateSprite)walk[alpha]).Update(gt);
                    }
                } else if(ctrls.Interact) {
                    Rectangle range = this.CollisionBox;

                    range.Y -= 15;
                    range.Height += 30;

                    if(facing == Direction.NORTH || facing == Direction.NORTHEAST || facing == Direction.NORTHWEST) {
                        range.Y -= EZTweakVars.PlayerInteractRange;
                        range.Height += EZTweakVars.PlayerInteractRange;
                    }
                    if(facing == Direction.SOUTH || facing == Direction.SOUTHEAST || facing == Direction.SOUTHWEST) {
                        range.Height += EZTweakVars.PlayerInteractRange;
                    }

                    if(facing == Direction.WEST || facing == Direction.NORTHWEST || facing == Direction.SOUTHWEST) {
                        range.X -= EZTweakVars.PlayerInteractRange;
                        range.Width += EZTweakVars.PlayerInteractRange;
                    }
                    if(facing == Direction.EAST || facing == Direction.NORTHEAST || facing == Direction.SOUTHEAST) {
                        range.Width += EZTweakVars.PlayerInteractRange;
                    }

                    foreach(GameObject alpha in StaticVars.CurrRoom.Objects) {
                        if(alpha is IInteractive) {
                            if(!(alpha is ISpecificFacing) || ((ISpecificFacing)alpha).RightFacing(facing)) {
                                if(range.Contains(alpha.Loc)) {
                                    ((IInteractive)alpha).Interact();
                                    break;
                                }
                            }
                        }
                    }
                } else if(ctrls.EnteringADA) {
                    StaticVars.currState = GameState.TOADA;
                } else if(ctrls.Fullscreen) {
                    StaticVars.inst.FullToggle();
                }
                // </noconv>
            } else { // <conv>
                Convo.Blurb curr = Convo.Conversation.curr;
                if(ctrls.ConvoNext) {
                    if(!curr.ShowingAll) {
                        curr.ShowAll();
                    } else {
                        if(curr is Convo.MethodBlurb && ((Convo.MethodBlurb)curr).method != null) {
                            ((Convo.MethodBlurb)curr).method.DynamicInvoke();
                        }
                        Convo.Conversation.curr = curr.Next;
                    }
                }
                if(curr is Convo.BranchingBlurb) {

                }
                // </conv>
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

                Texture2D stand = cm.Load<Texture2D>("protag/Overworld/arms/stand/Stand - " + s);
                Texture2D walkL = cm.Load<Texture2D>("protag/Overworld/arms/walk/" + s + " L"), walkR = cm.Load<Texture2D>("protag/Overworld/arms/walk/" + s + " R");

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

    public class ProtagStats {
        private int mhp, mmp, hp, mp;
        private Inventory inv;

        public int MaxHP {
            get { return mhp; }
            set { mhp = value; }
        }

        public int MaxMP {
            get { return mmp; }
            set { mmp = value; }
        }

        public int HP {
            get { return hp; }
            set { hp = value; }
        }

        public int MP {
            get { return mp; }
            set { mp = value; }
        }

        public Inventory Inv {
            get { return inv; }
        }

        public ProtagStats() {
            inv = new Inventory();
        }
    }

    public class Inventory {
        public class Pocket {
            private Item.Item[] content;

            public Pocket() {
                content = new Item.Item[20];
            }

            public Item.Item[] Content {
                get { return content; }
            }

            public Item.Item this[int index] {
                get { return content[index]; }
                set { content[index] = value; }
            }

            public void Add(Item.Item alpha) {
                for(int i = 0; i < 20; i++) {
                    if(content[i] == null) { content[i] = alpha; return; }
                }
                throw new Exception(); // Cannot add specified item; not enough space
            }
        }

        private List<Pocket> pkts;

        public Inventory() {
            pkts = new List<Pocket>();
        }

        public void AddPocket() {
            AddPockets(1);
        }

        public void AddPockets(int num) {
            for(int i = 0; i < num; i++) {
                pkts.Add(new Pocket());
            }
        }

        public List<Pocket> Pockets {
            get { return pkts; }
        }

        public int FreeSpace {
            get {
                int rtn = 0;

                foreach(Pocket alpha in pkts) {
                    foreach(Item.Item beta in alpha.Content) {
                        if(beta == null) rtn++;
                    }
                }

                return rtn;
            }
        }

        public void Add(Item.Item alpha) {
            foreach(Pocket beta in pkts) {
                try {
                    beta.Add(alpha);
                    return; // Added item; can finish
                } catch(Exception) { // Cannot add item; not enough space
                    // Try next pocket
                }
            }
            throw new Exception();  // Cannot add item; no space in any pocket
        }
    }
}
