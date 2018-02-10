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
        SoundEffect pickupSound;
        SoundEffect gameoverSound;
        Song music;
        Serpent serpent = new Serpent();
        Sprite pomme;
        int score = 0;
        SpriteFont font;
        bool pause = false;
        bool finPartie = false;

        double elapsedTime = 0;
        const float INTERVALLE = 0.5f;
        const float VITESSE_INI = 1.5f;
        const float VITESSE_MAX = 2.5f;
        float vitesse = VITESSE_INI;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        int orientation = 0;
        bool s = false;
        Random rnd = new Random();

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        public Vector2 RandomGridPosition()
        {
            return new Vector2(rnd.Next(graphics.PreferredBackBufferWidth / 25) * 25.0f, rnd.Next(graphics.PreferredBackBufferHeight / 25) * 25.0f);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            pomme = new Sprite("Pomme", RandomGridPosition());
            MediaPlayer.IsRepeating = true;

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

            pickupSound = Content.Load<SoundEffect>("pickup_sound");
            gameoverSound = Content.Load<SoundEffect>("gameover_sound");

            music = Content.Load<Song>("music");

            MediaPlayer.Play(music);

            font = Content.Load<SpriteFont>("arial");
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

            if (!s)
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

            if (keyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                pause = !pause;
                if (pause)
                    MediaPlayer.Pause();
                else
                    MediaPlayer.Resume();
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && finPartie)
            {
                serpent = new Serpent();
                pomme.Position = RandomGridPosition();
                score = 0;
                vitesse = VITESSE_INI;
                finPartie = false;
                orientation = 0;
                s = false;
                MediaPlayer.Play(music);
            }

            if (!finPartie && !pause)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (elapsedTime >= INTERVALLE / vitesse)
                {
                    serpent.Update(gameTime, orientation);
                    if (serpent.EstSurSerpent(serpent.Get(0).Position, 1) || serpent.EstHorsEcran(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)))
                    {
                        gameoverSound.Play();
                        MediaPlayer.Stop();
                        finPartie = true;
                    }

                    if (pomme.Position == serpent.Get(0).Position)
                    {
                        SoundEffectInstance pickupInstance = pickupSound.CreateInstance();
                        pickupInstance.Volume = 0.1f;
                        pickupInstance.Play();
                        serpent.Add();
                        Vector2 nouvPos;
                        do
                        {
                            nouvPos = RandomGridPosition();
                        }
                        while (serpent.EstSurSerpent(nouvPos));

                        pomme.Position = nouvPos;
                        score += 100;
                        if(vitesse < VITESSE_MAX)
                        {
                            vitesse += 0.25f;
                        }
                    }
                    elapsedTime = 0;
                    s = false;
                }
            }

            oldKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            string scoreStr = "Score : " + score;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, scoreStr, new Vector2(graphics.PreferredBackBufferWidth - font.MeasureString(scoreStr).X - 10, 20), Color.Black);
            serpent.Draw(spriteBatch, gameTime, textures);
            pomme.Draw(spriteBatch, gameTime, textures);
            if(pause)
            {
                string message = "PAUSE";
                spriteBatch.DrawString(font, message, new Vector2(graphics.PreferredBackBufferWidth - font.MeasureString(message).X, graphics.PreferredBackBufferHeight - font.MeasureString(message).Y) / 2, Color.Black);
            }
            if (finPartie)
            {
                string message = "Perdu ! (Appuyer sur Entrée pour recommencer)";
                spriteBatch.DrawString(font, message, new Vector2(graphics.PreferredBackBufferWidth - font.MeasureString(message).X, graphics.PreferredBackBufferHeight - font.MeasureString(message).Y) / 2, Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
