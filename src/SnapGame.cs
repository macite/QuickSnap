using System;
using SwinGameSDK;
using CardGames.GameLogic;

namespace CardGames
{
    public class SnapGame
    {
        private static Card _testCard = Card.RandomCard();

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
                
                //generate a random card on spacebar press
                if (SwinGame.KeyTyped(KeyCode.vk_SPACE))
                {
                    _testCard = Card.RandomCard ();
                }

                SwinGame.DrawText ("Card generated was: " + _testCard.ToString (), Color.RoyalBlue, 0, 20);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}