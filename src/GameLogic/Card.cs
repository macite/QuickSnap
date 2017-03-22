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
    /// Each Card has a Rank and a Suit, and can be either face up or face down.
    /// </summary>
    public class Card
    {
        private Rank _rank;
        private Suit _suit;
        private bool _faceUp;
        
        /// <summary>
        /// Create a new card with the indicated rank and suit.
        /// By default the card will be face down.
        /// </summary>
        /// <param name="r">The rank value for the card.</param>
        /// <param name="s">The suit value for the card.</param>
        public Card (Rank r, Suit s)
        {
            _rank = r;
            _suit = s;
            _faceUp = false;
        }
        
        /// <summary>
        /// Create and return a new card with randomised Rank and Suit.
        /// </summary>
        public static Card RandomCard()
        {
            Rank randomRank = (Rank)( SwinGame.Rnd ((int)Rank.KING) + 1);
            Suit randomSuit = (Suit)( SwinGame.Rnd ((int)Suit.SPADE + 1));
            
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
        /// Allows you to check if the card is fact up, or face down.
        /// </summary>
        /// <value><c>true</c> if the card is face up; otherwise, <c>false</c> to indicate face down.</value>
        public bool FaceUp
        {
            get { return _faceUp; }
        }
        
        /// <summary>
        /// Turns the card over, a face up card will become face down whereas a face down card will become
        /// face up.
        /// </summary>
        public void TurnOver()
        {
            _faceUp = ! _faceUp;
        }

        /// <summary>
        /// Returns the index of the card when laid out in sequence from Ace Spades, to the Kind of Clubs.
        /// Suits are in the order Spades, Hearts, Diamonds, then Clubs. Cards are then ordered within the
        /// suit by rank from Ace to King. Where the card is face down, this will return the 52 (the 53rd card)
        /// which can be used to represent the back of the cards.
        /// </summary>
        /// <value>The index of the card.</value>
        public int CardIndex
        {
            get
            {
                if (_faceUp)
                {
                    switch (_suit)
                    {
                    case Suit.SPADE:
                        return (int)_rank - 1; // Ace = 1, but Ace Spades should be 0
                    case Suit.HEART:
                        return 12 + (int)_rank;
                    case Suit.DIAMOND:
                        return 25 + (int)_rank;
                    case Suit.CLUB:
                        return 38 + (int)_rank;
                    default:
                        return 52;
                    }
                }
                else
                    return 52;

            }
        }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="CardGames.Card"/>. This will have
        /// the first character representing the rank (using T for 10), and then a symbol for the suit. For example, QS is
        /// the Queen of Spades, 8H is Eight Hearts, TD is Ten Diamonds, and 2C is 2 of Clubs. Where the card is face down
        /// this will return the value <c>"**"</c>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="CardGames.Card"/>.</returns>
        public override string ToString()
        {
            if (_faceUp)
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
            else
            {
                return "**";
            }
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
            c.TurnOver();
            Assert.AreEqual (Rank.KING, c.Rank);
            Assert.AreEqual (Suit.HEART, c.Suit);

            c = new Card (Rank.TWO, Suit.DIAMOND);
            c.TurnOver();
            Assert.AreEqual (Rank.TWO, c.Rank);
            Assert.AreEqual (Suit.DIAMOND, c.Suit);
        }

        [Test]
        public void TestCardIndex()
        {
            Card c = new Card (Rank.ACE, Suit.SPADE);
            Assert.AreEqual (52, c.CardIndex);
            c.TurnOver();
			Assert.AreEqual (0, c.CardIndex);

            c = new Card (Rank.KING, Suit.CLUB);
			Assert.AreEqual (52, c.CardIndex);
            c.TurnOver();
			Assert.AreEqual (51, c.CardIndex);
        }

        [Test]
        public void TestCardToString()
        {
            Card c = new Card (Rank.ACE, Suit.SPADE);
            c.TurnOver();
            Assert.AreEqual ("AS", c.ToString ());

            c = new Card (Rank.TEN, Suit.CLUB);
            c.TurnOver();
            Assert.AreEqual ("TC", c.ToString ());

            c = new Card (Rank.THREE, Suit.DIAMOND);
            c.TurnOver();
            Assert.AreEqual ("3D", c.ToString ());

            c = new Card (Rank.JACK, Suit.HEART);
            c.TurnOver();
            Assert.AreEqual ("JH", c.ToString ());
        }

        [Test]
        public void TestCardTurnOver()
        {
            Card c = new Card(Rank.ACE, Suit.DIAMOND);
            Assert.AreEqual ("**", c.ToString ());
            c.TurnOver();
            Assert.AreEqual ("AD", c.ToString ());
            c.TurnOver();
            Assert.AreEqual ("**", c.ToString ());

            c = new Card(Rank.FOUR, Suit.HEART);
            Assert.AreEqual ("**", c.ToString ());
            c.TurnOver();
            Assert.AreEqual ("4H", c.ToString ());
        }
    }

    #endif

    #endregion
}

