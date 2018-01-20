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
        public List<Sprite> corps;

        public Serpent()
        {
            corps = new List<Sprite>();
            corps.Add(new Sprite("SerpentTete", new Vector2(75, 25)));
            corps.Add(new Sprite("SerpentCorps", new Vector2(50, 25)));
            corps.Add(new Sprite("SerpentQueue", new Vector2(25, 25)));
        }

        public void Add()
        {
            Sprite queue = new Sprite("SerpentQueue", Vector2.Zero);
            Sprite s = corps[corps.Count - 1];
            switch(s.Orientation)
            {
                case 0 :
                    queue.Position = s.Position + new Vector2(-25, 0);
                    break;
                case 1:
                    queue.Position = s.Position + new Vector2(0, -25);
                    break;
                case 2:
                    queue.Position = s.Position + new Vector2(25, 0);
                    break;
                case 3:
                    queue.Position = s.Position + new Vector2(0, 25);
                    break;
            }
            queue.Orientation = s.Orientation;
            s.TextureName = "SerpentCorps";
            corps.Add(queue);

        }

        public Sprite Get(int index)
        {
            return corps[index];
        }

        public bool EstSurSerpent(Vector2 pos, int debut = 0)
        {
            return EstSurSerpent(pos, debut, corps.Count);
        }

        public bool EstSurSerpent(Vector2 pos, int debut, int fin)
        {
            for(int i = debut; i < fin; i++)
            {
                if(corps[i].Position == pos)
                {
                    return true;
                }
            }

            return false;
        }

        public bool EstHorsEcran(Vector2 tailleEcran)
        {
            return corps[0].Position.X >= tailleEcran.X || corps[0].Position.Y >= tailleEcran.Y || corps[0].Position.X < 0 || corps[0].Position.Y < 0;
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
            nouvTete.Orientation = orientation;
            corps.Insert(0, nouvTete);

            // Suppression de la queue
            corps[corps.Count - 2].Orientation = corps[corps.Count - 3].Orientation;
            corps[corps.Count - 2].TextureName = "SerpentQueue";
            corps.RemoveAt(corps.Count - 1);

            // Modification de la texture du corps
            if (corps[1].Orientation == orientation)
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
                if(corps[0].Orientation == 0 && corps[2].Orientation == 1 || corps[0].Orientation == 3 && corps[2].Orientation == 2)
                {
                    corps[1].Orientation = 3;
                }
                /*
                       * *
                       *
                */
                else if (corps[0].Orientation == 1 && corps[2].Orientation == 2 || corps[0].Orientation == 0 && corps[2].Orientation == 3)
                {
                    corps[1].Orientation = 0;
                }
                /*
                       * *
                         *
                */
                else if (corps[0].Orientation == 2 && corps[2].Orientation == 3 || corps[0].Orientation == 1 && corps[2].Orientation == 0)
                {
                    corps[1].Orientation = 1;
                }
                /*
                         *
                       * *
                */
                else if (corps[0].Orientation == 3 && corps[2].Orientation == 0 || corps[0].Orientation == 2 && corps[2].Orientation == 1)
                {
                    corps[1].Orientation = 2;
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
