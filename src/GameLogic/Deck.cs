using System;

#if DEBUG
	using NUnit.Framework;
#endif 


namespace CardGames.GameLogic
{
	/// <summary>
	/// A Deck of 52 cards from which cards can be drawn.
	/// </summary>
	public class Deck
	{
        private readonly Card[] 	_cards = new Card[52];
        private int 	_topCard;
     
		/// <summary>
		/// Creates a new Deck with 52 Cards. The first card
		/// will be the top of the Deck.
		/// </summary>
		public Deck ()
		{
		    int i = 0;
		    
		    for (Suit s = Suit.CLUB; s <= Suit.SPADE; s++) 
		    {
		        for (Rank r = Rank.ACE; r <= Rank.KING; r++) 
		        {
		            Card c = new Card(r, s);
		            _cards[i] = c;
		            i++;
		        }    
		    }
		
		    _topCard = 0;
		}
        
		/// <summary>
		/// Indicates how many Cards remain in the Deck.
		/// </summary>
		/// <value>The number of cards remaining.</value>
        public int CardsRemaining
        {
            get
            {
                return 52 - _topCard;
            }
        }

		/// <summary>
		/// Returns all of the cards to the Deck, and shuffles their order.
		/// All cards are turned so that they are face down.
		/// </summary>
		public void Shuffle()
		{
			//TODO: implement shuffle!
		}
        
		/// <summary>
		/// Takes a card from the top of the Deck. This will return
		/// <c>null</c> when there are no cards remaining in the Deck.
		/// </summary>
        public Card Draw()
        {
            if (_topCard < 52) 
		    {
		        Card result = _cards[_topCard];
		        _topCard++;
		        return result;
		    }
		    else
		    {
		        return null;
		    }
		
        }
	}

	#region Deck Unit Tests
	#if DEBUG

	public class DeckTests
	{
		[Test]
		public void TestDeckCreation()
		{
			Deck d = new Deck();

			Assert.AreEqual(52, d.CardsRemaining);
		}

		[Test]
		public void TestDraw()
		{
			Deck d = new Deck();

			Assert.AreEqual(52, d.CardsRemaining);
			
			Card c = d.Draw();
			Assert.AreEqual(51, d.CardsRemaining);
			Assert.AreEqual(Rank.ACE, c.Rank);
			Assert.AreEqual(Suit.CLUB, c.Suit);

			int count = 51;
			
			// Draw all cards from the deck 
			while ( d.CardsRemaining > 0 )
			{
				c = d.Draw();
				count--;
				
				Assert.AreEqual(count, d.CardsRemaining);
			}

		}
	}

	#endif 
	#endregion
}

