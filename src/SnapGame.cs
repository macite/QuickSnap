using System;
using SwinGameSDK;
using CardGames.GameLogic;

namespace CardGames
{
    public class SnapGame
    {
        public static void LoadResources()
        {
            Bitmap cards;
            cards = SwinGame.LoadBitmapNamed ("Cards", "Cards.png");
            SwinGame.BitmapSetCellDetails (cards, 82, 110, 13, 5, 53);      // set the cells in the bitmap to match the cards
        }

        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 800, 600);

            LoadResources();
            
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

                //turn over the card on F press
                if (SwinGame.KeyTyped(KeyCode.vk_f))
                {
                    testCard.TurnOver();
                }

                SwinGame.DrawText ("Card generated was: " + testCard.ToString (), Color.RoyalBlue, 0, 20);
                SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), testCard.CardIndex, 160, 50);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}