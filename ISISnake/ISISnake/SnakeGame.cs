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
        Sprite pomme;

        double elapsedTime = 0;
        const float INTERVALLE = 0.5f;

        KeyboardState keyboardState;
        int orientation = 0;
        bool s = false;

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

            Random rnd = new Random();
            pomme = new Sprite("Pomme", new Vector2(rnd.Next(graphics.PreferredBackBufferWidth / 25) * 25.0f, rnd.Next(graphics.PreferredBackBufferHeight / 25) * 25.0f));

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
            textures.Add("Pomme",           Content.Load<Texture2D>("pomme"));
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

            if(!s)
            {
                if (keyboardState.IsKeyDown(Keys.Right) && orientation != 2)
                {
                    orientation = 0;
                    s = true;
                }
                if (keyboardState.IsKeyDown(Keys.Down) && orientation != 3)
                {
                    orientation = 1;
                    s = true;
                }
                if (keyboardState.IsKeyDown(Keys.Left) && orientation != 0)
                {
                    orientation = 2;
                    s = true;
                }
                if (keyboardState.IsKeyDown(Keys.Up) && orientation != 1)
                {
                    orientation = 3;
                    s = true;
                }
            }
            

            if (elapsedTime >= INTERVALLE)
            {
                serpent.Update(gameTime, orientation);
                elapsedTime = 0;
                s = false;
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
            pomme.Draw(spriteBatch, gameTime, textures);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
