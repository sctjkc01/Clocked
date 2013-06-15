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
    public enum GameState {
        MENU, GAME, FADEOUT, FADEIN
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D whitePixel;
        byte opacity = 0;
        TimeSpan fade = new TimeSpan(150000), timer = new TimeSpan(0);

        public static GameState currState = GameState.GAME;

        Protagonist p;

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
            p = new Protagonist();
            p.Loc = new Point(588, 696);

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
            // Allows the game to exit
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                this.Exit();

            // TODO: Add your update logic here
            if(currState == GameState.GAME) {
                PostProcessing.Update(gameTime);
                if(StaticVars.Exit != null) {
                    currState = GameState.FADEOUT;
                    timer = new TimeSpan(0);
                }
            } else if(currState == GameState.FADEOUT) {
                timer += gameTime.ElapsedGameTime;
                if(timer > fade) {
                    timer -= fade;
                    if(opacity + 15 > 255) {
                        opacity = 255;
                        StaticVars.CurrRoom = StaticVars.Exit.Item1;
                        p.Loc = StaticVars.Exit.Item2;
                        timer = new TimeSpan(0);
                        currState = GameState.FADEIN;
                    } else {
                        opacity += 15;
                    }
                }
            } else if(currState == GameState.FADEIN) {
                timer += gameTime.ElapsedGameTime;
                if(timer > fade) {
                    timer -= fade;
                    if(opacity - 15 < 0) {
                        opacity = 0;
                        StaticVars.Exit = null;
                        currState = GameState.GAME;
                    } else {
                        opacity -= 15;
                    }
                }
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

            int x = p.Loc.X - 400;
            int y = p.Loc.Y - 350;

            x = Math.Max(0, Math.Min(StaticVars.CurrRoom.Size.Width - 800, x));
            y = Math.Max(0, Math.Min(StaticVars.CurrRoom.Size.Height - 600, y));

            StaticVars.Camera.X = x;
            StaticVars.Camera.Y = y;

            StaticVars.CurrRoom.Draw(spriteBatch);

            bool pDrawn = false;
            foreach(GameObject obj in StaticVars.CurrRoom.Objects) {
                if(obj.Loc.Y > p.Loc.Y && !pDrawn) {
                    p.Draw(spriteBatch);
                    pDrawn = true;
                }
                obj.Draw(spriteBatch);
            }
            if(!pDrawn) p.Draw(spriteBatch);

            if(currState == GameState.FADEOUT || currState == GameState.FADEIN) {
                spriteBatch.Draw(whitePixel, new Vector2(0, 0), null, new Color(0, 0, 0, opacity), 0f, Vector2.Zero, new Vector2(800, 600), SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
