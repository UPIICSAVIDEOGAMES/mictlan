using System;

namespace PlayerOnStage
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RUVG_Game game = new RUVG_Game())
            {
                game.Run();
            }
        }
    }
#endif
}

