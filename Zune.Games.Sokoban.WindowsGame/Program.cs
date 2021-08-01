using System;

namespace Zune.Games.Sokoban.WindowsGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Sokoban game = new Sokoban())
            {
                game.Run();
            }
        }
    }
}

