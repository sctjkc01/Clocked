using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using StickXNAEngine.Audio;

namespace DarosGame {
    public class Resources {
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static Dictionary<string, Song> songs = new Dictionary<string, Song>();
        public static MultiPieceSong title;

        public static StickXNAEngine.Graphic.StaticSprite curs, cursClick, cursDrag;
        public static StickXNAEngine.Graphic.AnimateSprite adaOut, adaAway;

        public static void InitResources(ContentManager cm) {
            fonts["04b03m"] = cm.Load<SpriteFont>("04b03");
            fonts["04b03s"] = cm.Load<SpriteFont>("04b03small");
            fonts["04b03l"] = cm.Load<SpriteFont>("04b03large");

            fonts["8bits"] = cm.Load<SpriteFont>("8BitWondersmall");
            fonts["8bitm"] = cm.Load<SpriteFont>("8BitWonder");
            fonts["8bitl"] = cm.Load<SpriteFont>("8BitWonderlarge");

            // --------------------- //

            songs["handylass"] = new Song("Music/A Handy Lass");
            songs["worndown"] = new Song("Music/Worn Down Science");
            songs["genbattle"] = new Song("Music/It's Time to Throw Down, Boys!");

            // title = new MultiPieceSong("Music/Clocked_woosh", "Music/Clocked"); RESOURCES MISSING!

            // --------------------- //

            curs = new StickXNAEngine.Graphic.StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Cursor"), new Microsoft.Xna.Framework.Point(4, 3));
            cursClick = new StickXNAEngine.Graphic.StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Cursor"), new Microsoft.Xna.Framework.Point(4, 3));
            cursDrag = new StickXNAEngine.Graphic.StaticSprite(cm.Load<Texture2D>("Menu/Top Menu/Cursor"), new Microsoft.Xna.Framework.Point(4, 3));

            StickXNAEngine.Graphic.StaticSprite[] adaAnim = new StickXNAEngine.Graphic.StaticSprite[14];
            for(int i = 1; i < 15; i++) {
                adaAnim[i-1] = new StickXNAEngine.Graphic.StaticSprite(cm.Load<Texture2D>("protag/Overworld/arms/Check Menu/Check Menu " + i), new Microsoft.Xna.Framework.Point(35, 102));
            }
            adaOut = new StickXNAEngine.Graphic.AnimateSprite(new TimeSpan(400000));
            adaAway = new StickXNAEngine.Graphic.AnimateSprite(new TimeSpan(400000));
            for(int i = 0; i < 14; i++) {
                adaOut.Add(adaAnim[i]);
                adaAway.Add(adaAnim[13 - i]);
            }
        }
    }
}
