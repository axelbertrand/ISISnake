using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISISnake
{
    class Serpent
    {
        public List<Sprite> corps = new List<Sprite>();

        public void AddSprite(Sprite sprite)
        {
            corps.Add(sprite);
        }

        public Sprite GetSprite(int index)
        {
            return corps[index];
        }

        public void Update(GameTime gameTime)
        {
            Vector2 nouvPosTete = corps[0].Position + new Vector2(25.0f, 0);

            for(int i = corps.Count - 1; i > 0; i--)
            {
                corps[i].Position = corps[i - 1].Position;
            }

            corps[0].Position = nouvPosTete;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(Sprite sprite in corps)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
