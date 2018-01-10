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
        public string TextureName { get; set; }
        public Vector2 Position { get; set; }
        public int Orientation { get; set; }

        public Sprite(string textureName, Vector2 position)
        {
            TextureName = textureName;
            Position = position;
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SortedList<string, Texture2D> listeTexture)
        {
            Texture2D texture = listeTexture[TextureName];
            spriteBatch.Draw(texture, Position + new Vector2(texture.Width / 2, texture.Height / 2), null, Color.White, MathHelper.ToRadians(Orientation * 90.0f), new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 0);
        }

    }
}
