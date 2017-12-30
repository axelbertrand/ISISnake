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

namespace ISISnake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SnakeGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SortedList<string, Texture2D> textures = new SortedList<string, Texture2D>();
        Serpent serpent = new Serpent();

        double elapsedTime = 0;
        const float INTERVALLE = 1.0f;

        KeyboardState keyboardState;
        int orientation = 0;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            serpent.AddSprite(new Sprite("SerpentTete", new Vector2(75, 25)));
            serpent.AddSprite(new Sprite("SerpentCorps", new Vector2(50, 25)));
            serpent.AddSprite(new Sprite("SerpentQueue", new Vector2(25, 25)));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textures.Add("SerpentTete",     Content.Load<Texture2D>("serpent_tete"));
            textures.Add("SerpentCorps",    Content.Load<Texture2D>("serpent_corps"));
            textures.Add("SerpentVirage",   Content.Load<Texture2D>("serpent_virage"));
            textures.Add("SerpentQueue",    Content.Load<Texture2D>("serpent_queue"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (orientation != 2 && keyboardState.IsKeyDown(Keys.Right))
            {
                orientation = 0;
            }
            if (orientation != 3 && keyboardState.IsKeyDown(Keys.Down))
            {
                orientation = 1;
            }
            if (orientation != 0 && keyboardState.IsKeyDown(Keys.Left))
            {
                orientation = 2;
            }
            if (orientation != 1 && keyboardState.IsKeyDown(Keys.Up))
            {
                orientation = 3;
            }

            if (elapsedTime >= INTERVALLE)
            {
                serpent.Update(gameTime, orientation);
                elapsedTime = 0;
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            serpent.Draw(spriteBatch, gameTime, textures);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
