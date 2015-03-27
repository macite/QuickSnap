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
            SwinGame.BitmapSetCellDetails (cards, 179, 259, 13, 5, 53);      // set the cells in the bitmap to match the cards
        }

        private static Card _testCard = Card.RandomCard();

        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("Snap!", 800, 600);

            LoadResources();
            
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

                //turn over the card on F press
                if (SwinGame.KeyTyped(KeyCode.vk_f))
                {
                    _testCard.TurnOver();
                }

                SwinGame.DrawText ("Card generated was: " + _testCard.ToString (), Color.RoyalBlue, 0, 20);
                SwinGame.DrawCell (SwinGame.BitmapNamed ("Cards"), _testCard.CardIndex, 160, 50);

                //Clear the screen and draw the framerate
                SwinGame.DrawFramerate(0,0);

                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}