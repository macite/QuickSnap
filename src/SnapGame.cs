using System;
using SwinGameSDK;
using CardGames.GameLogic;

namespace CardGames
{
    public class SnapGame
    {
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 800, 600);
            
			Deck myDeck = new Deck ();
			Card testCard = Card.RandomCard();

            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                SwinGame.ClearScreen(Color.White);
                
                //generate a random card on spacebar press
				if (SwinGame.KeyTyped(KeyCode.vk_SPACE) && myDeck.CardsRemaining > 0)
                {
					testCard = myDeck.Draw ();
                }

				SwinGame.DrawText ("Card generated was: " + testCard.ToString (), Color.RoyalBlue, 0, 20);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}