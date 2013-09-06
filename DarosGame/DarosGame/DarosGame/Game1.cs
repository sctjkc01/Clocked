using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using StickXNAEngine.Utility;
using StickXNAEngine.Graphic;

namespace DarosGame {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D whitePixel;
        byte opacity = 0;
        TimeSpan fade = new TimeSpan(150000), timer = new TimeSpan(0);
        float adashow = 0.0f;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            Room tr = new TestRoom();
            StaticVars.CurrRoom = tr;
            StaticVars.player = new Protagonist();
            StaticVars.player.Loc = new Point(588, 696);
            StaticVars.adamenu = new ADAMenu();
            new DerelictLaboratory.Room1();

            StaticVars.inst = this;

            PostProcessing.Init();

            Console.WriteLine("This Console Box is here for debug testing purposes ONLY!\n");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.InitResources(Content);

            whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            whitePixel.SetData<Color>(new Color[] { Color.White });

            PostProcessing.Res(Content);
            Convo.Conversation.LoadRes(Content);

            StickXNAEngine.Audio.Song.Repeat = true;
            MediaPlayer.Volume = 0.8f;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if(!Resources.songs["handylass"].Playing) Resources.songs["handylass"].Playing = true;

            // TODO: Add your update logic here
            if(StaticVars.currState == GameState.GAME) {
                PostProcessing.Update(gameTime);
                if(StaticVars.Exit != null) {
                    StaticVars.currState = GameState.FADEOUT;
                    timer = new TimeSpan(0);
                }
            } else if(StaticVars.currState == GameState.FADEOUT) {
                timer += gameTime.ElapsedGameTime;
                if(timer > fade) {
                    timer -= fade;
                    if(opacity + 15 > 255) {
                        opacity = 255;
                        StaticVars.CurrRoom = StaticVars.Exit.Item1;
                        StaticVars.player.Loc = StaticVars.Exit.Item2;
                        timer = new TimeSpan(0);
                        StaticVars.currState = GameState.FADEIN;
                    } else {
                        opacity += 15;
                    }
                }
            } else if(StaticVars.currState == GameState.FADEIN) {
                timer += gameTime.ElapsedGameTime;
                if(timer > fade) {
                    timer -= fade;
                    if(opacity - 15 < 0) {
                        opacity = 0;
                        StaticVars.Exit = null;
                        StaticVars.currState = GameState.GAME;
                    } else {
                        opacity -= 15;
                    }
                }
            } else if(StaticVars.currState == GameState.TOADA) {
                IsMouseVisible = true;
                adashow = Math.Min(adashow + 0.0715f, 1f);
                if(adashow == 1f) {
                    StaticVars.currState = GameState.ADA;
                }
            } else if(StaticVars.currState == GameState.FROMADA) {
                adashow = Math.Max(adashow - 0.0715f, 0f);
                if(adashow == 0f) {
                    StaticVars.currState = GameState.GAME;
                    IsMouseVisible = false;
                }
            } else if(StaticVars.currState == GameState.ADA) {
                StaticVars.adamenu.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            int x = StaticVars.player.Loc.X - 400;
            int y = StaticVars.player.Loc.Y - 350;

            x = Math.Max(0, Math.Min(StaticVars.CurrRoom.Size.Width - 800, x));
            y = Math.Max(0, Math.Min(StaticVars.CurrRoom.Size.Height - 600, y));

            StaticVars.Camera.X = x;
            StaticVars.Camera.Y = y;

            StaticVars.CurrRoom.Draw(spriteBatch);

            bool pDrawn = false;
            foreach(GameObject obj in StaticVars.CurrRoom.Objects) {
                if(obj.Loc.Y > StaticVars.player.Loc.Y && !pDrawn) {
                    StaticVars.player.Draw(spriteBatch);
                    pDrawn = true;
                }
                obj.Draw(spriteBatch);
            }
            if(!pDrawn) StaticVars.player.Draw(spriteBatch);

            if(StaticVars.currState == GameState.FADEOUT || StaticVars.currState == GameState.FADEIN) {
                spriteBatch.Draw(whitePixel, new Vector2(0, 0), null, new Color(0, 0, 0, opacity), 0f, Vector2.Zero, new Vector2(800, 600), SpriteEffects.None, 0);
            } else if(StaticVars.currState == GameState.TOADA || StaticVars.currState == GameState.FROMADA) {
                StaticVars.adamenu.Draw(spriteBatch, adashow);
            } else if(StaticVars.currState == GameState.ADA) {
                StaticVars.adamenu.Draw(spriteBatch);
            }

            Convo.Conversation.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void FullToggle() {
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();
        }
    }
}
