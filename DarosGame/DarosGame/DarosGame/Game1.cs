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

        AnimateSprite walker;

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

            Window.AllowUserResizing = true;

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
            //PostProcessing.Res(Content);

            // TODO: use this.Content to load your game content here

            walker = new AnimateSprite(new TimeSpan(1050000));

            Texture2D stand = Content.Load<Texture2D>("protag/arms/stand/Stand - S");
            Texture2D walkL = Content.Load<Texture2D>("protag/arms/walk/S L"), walkR = Content.Load<Texture2D>("protag/arms/walk/S R");

            walker.Add(new StaticSprite(stand, new Point(35, 102)));
            walker.Add(new StaticSprite(walkL, new Point(35, 102)));
            walker.Add(new StaticSprite(walkL, new Point(35, 101)));
            walker.Add(new StaticSprite(stand, new Point(35, 102)));
            walker.Add(new StaticSprite(walkR, new Point(35, 102)));
            walker.Add(new StaticSprite(walkR, new Point(35, 101)));
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
            walker.Update(gameTime);

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

            spriteBatch.DrawString(Resources.font, Window.ClientBounds.Width + ", " + Window.ClientBounds.Height, new Vector2(10, 10), Color.White);
            walker.Draw(spriteBatch, new Point(50, 125));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
