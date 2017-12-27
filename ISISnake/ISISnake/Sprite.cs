using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISISnake
{
    class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Orientation { get; set; }

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + new Vector2(25.0f / 2, 25.0f / 2), null, Color.White, MathHelper.ToRadians(Orientation), new Vector2(Texture.Width / 2, Texture.Height / 2), 1, SpriteEffects.None, 0);
        }

    }
}
