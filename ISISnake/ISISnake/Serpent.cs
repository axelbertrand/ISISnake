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

        public void Update(GameTime gameTime, int orientation)
        {
            // orientation == 0 => droite
            // orientation == 1 => bas
            // orientation == 2 => gauche
            // orientation == 3 => haut
            Vector2 direction;
            switch(orientation)
            {
                case 0 :
                    direction = new Vector2(25.0f, 0);
                    break;
                case 1 :
                    direction = new Vector2(0, 25.0f);
                    break;
                case 2 :
                    direction = new Vector2(-25.0f, 0);
                    break;
                case 3 :
                    direction = new Vector2(0, -25.0f);
                    break;
                default :
                    direction = new Vector2(25.0f, 0);
                    break;
            }

            Vector2 nouvPosTete = corps[0].Position + direction;

            // Ajout d'une nouvelle tête
            Sprite nouvTete = new Sprite("SerpentTete", corps[0].Position + direction);
            nouvTete.Orientation = orientation * 90.0f;
            corps.Insert(0, nouvTete);

            // Suppression de la queue
            corps[corps.Count - 2].Orientation = corps[corps.Count - 3].Orientation;
            corps[corps.Count - 2].TextureName = "SerpentQueue";
            corps.RemoveAt(corps.Count - 1);

            // Modification de la texture du corps
            if (corps[0].Orientation == corps[2].Orientation)
            {
                corps[1].TextureName = "SerpentCorps";
            }
            else
            {
                corps[1].TextureName = "SerpentVirage";
                /*
                       *
                       * *
                */
                if(corps[0].Orientation == 0 && corps[2].Orientation == 1 * 90.0f || corps[0].Orientation == 3 * 90.0f && corps[2].Orientation == 2 * 90.0f)
                {
                    corps[1].Orientation = 3 * 90.0f;
                }
                /*
                       * *
                       *
                */
                else if (corps[0].Orientation == 1 * 90.0f && corps[2].Orientation == 2 * 90.0f || corps[0].Orientation == 0 && corps[2].Orientation == 3 * 90.0f)
                {
                    corps[1].Orientation = 0 * 90.0f;
                }
                /*
                       * *
                         *
                */
                else if (corps[0].Orientation == 2 * 90.0f && corps[2].Orientation == 3 * 90.0f || corps[0].Orientation == 1 * 90.0f && corps[2].Orientation == 0)
                {
                    corps[1].Orientation = 1 * 90.0f;
                }
                /*
                         *
                       * *
                */
                else if (corps[0].Orientation == 3 * 90.0f && corps[2].Orientation == 0 || corps[0].Orientation == 2 * 90.0f && corps[2].Orientation == 1 * 90.0f)
                {
                    corps[1].Orientation = 2 * 90.0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SortedList<string, Texture2D> listeTexture)
        {
            foreach(Sprite sprite in corps)
            {
                sprite.Draw(spriteBatch, gameTime, listeTexture);
            }
        }
    }
}
