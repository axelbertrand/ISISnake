using System;

namespace ISISnake
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SnakeGame game = new SnakeGame())
            {
                game.Run();
            }
        }
    }
#endif
}

