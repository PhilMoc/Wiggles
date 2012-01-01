using System;

namespace Wiggles
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TowerAssault game = new TowerAssault())
            {
                game.Run();
            }
        }
    }
#endif
}

