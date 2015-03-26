using System;
using SwinGameSDK;

namespace CardGames
{
    public class SnapGame
    {
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 800, 600);
            
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                SwinGame.ClearScreen(Color.White);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);
                
                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}