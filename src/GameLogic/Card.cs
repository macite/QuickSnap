using System;
using SwinGameSDK;

#if DEBUG
using NUnit.Framework;
#endif


namespace CardGames.GameLogic
{
	/// <summary>
	/// The Suit enumeration provides a list of all of the valid suit values
	/// for the cards in the program.
	/// </summary>
    public enum Suit
    {
	    CLUB,
	    DIAMOND,
	    HEART,
	    SPADE
    }
    
	/// <summary>
	/// The Rank enumeration provides a list of all of the valid ranks (values)
	/// for the cards in the program. The ACE has value 1, ensuring that it is
	/// easy to map cards to related numeric values.
	/// </summary>
    public enum Rank
    {
	    ACE = 1,
	    TWO,
	    THREE,
	    FOUR,
	    FIVE,
	    SIX,
	    SEVEN,
	    EIGHT,
	    NINE,
	    TEN,
	    JACK,
	    QUEEN,
	    KING //king is 13
    }
    
	/// <summary>
	/// Each Card has a Rank and a Suit
	/// </summary>
	public class Card
	{
        private Rank _rank;
        private Suit _suit;
        
		/// <summary>
		/// Create a new card with the indicated rank and suit.
		/// </summary>
		/// <param name="r">The rank value for the card.</param>
		/// <param name="s">The suit value for the card.</param>
		public Card (Rank r, Suit s)
		{
            _rank = r;
            _suit = s;
		}
        
		/// <summary>
		/// Create and return a new card with randomised Rank and Suit.
		/// </summary>
		public static Card RandomCard()
		{
			Rank randomRank = (Rank)(( SwinGame.Rnd ((int)Rank.KING) - 1) + 1);
			Suit randomSuit = (Suit)( SwinGame.Rnd ((int)Suit.SPADE+1));
			
			Card randomCard = new Card (randomRank, randomSuit);

			return randomCard;
		}

		/// <summary>
		/// Allows you to read the value of the Card's rank.
		/// </summary>
		/// <value>The rank.</value>
        public Rank Rank
        {
            get { return _rank; }
        }
        
		/// <summary>
		/// Allows you to read the value of the Card's suit.
		/// </summary>
		/// <value>The suit.</value>
        public Suit Suit
        {
            get { return _suit; }
        }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CardGames.Card"/>. This will have
		/// the first character representing the rank (using T for 10), and then a symbol for the suit. For example, QS is
		/// the Queen of Spades, 8H is Eight Hearts, TD is Ten Diamonds, and 2C is 2 of Clubs. 
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CardGames.Card"/>.</returns>
        public override string ToString()
        {
	        String result = "";
	
	        switch (_rank){
	            case Rank.JACK:
	                result += "J";
	                break;
	            case Rank.QUEEN:
	                result += "Q";
	                break;
	            case Rank.KING:
	                result += "K";
	                break;
	            case Rank.ACE:
	                result += "A";
	                break;
	            case Rank.TEN:
	                result += "T";
	                break;
	            default:
	                result += (int)_rank;
                	break;
	        }
	
	        switch (_suit) {
	            case Suit.CLUB:
	                result += "C";
	                break;
	            case Suit.SPADE:
	                result += "S";
	                break;
	            case Suit.HEART:
	                result += "H";
	                break;
	            case Suit.DIAMOND:
	                result += "D";
	                break;
	            default:
	                result += "?";
                	break;
	        }
	
	        return result;
        }
        
	}

	// Wrap the unit tests in a region.
	#region Unit Tests

	#if DEBUG

	public class CardUnitTests
	{
		[Test]
		public void TestCardCreation()
		{
			Card c = new Card (Rank.KING, Suit.HEART);
			Assert.AreEqual (Rank.KING, c.Rank);
			Assert.AreEqual (Suit.HEART, c.Suit);

			c = new Card (Rank.TWO, Suit.DIAMOND);
			Assert.AreEqual (Rank.TWO, c.Rank);
			Assert.AreEqual (Suit.DIAMOND, c.Suit);
		}

		[Test]
		public void TestCardToString()
		{
			Card c = new Card (Rank.ACE, Suit.SPADE);
			Assert.AreEqual ("AS", c.ToString ());

			c = new Card (Rank.TEN, Suit.CLUB);
			Assert.AreEqual ("TC", c.ToString ());

			c = new Card (Rank.THREE, Suit.DIAMOND);
			Assert.AreEqual ("3D", c.ToString ());

			c = new Card (Rank.JACK, Suit.HEART);
			Assert.AreEqual ("JH", c.ToString ());
		}
	}

	#endif

	#endregion
}

