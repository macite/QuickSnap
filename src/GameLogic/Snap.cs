using System;
using SwinGameSDK;

#if DEBUG
using NUnit.Framework;
#endif 


namespace CardGames.GameLogic
{
	/// <summary>
	/// The Snap card game in which the user scores a point if they
	/// click when the rank of the last two cards match.
	/// </summary>
	public class Snap
	{
		// Keep only the last two cards...
		private readonly Card[] _topCards = new Card[2];

		// Have a Deck of cards to play with.
		private readonly Deck _deck;

		// Use a timer to allow the game to draw cards at timed intervals
		private readonly Timer _gameTimer;

		// The amount of time that must pass before a card is flipped?
		private int _flipTime = 1000;

		// the score for the 2 players
		private int[] _score = new int[2];

		private bool _started = false;

		/// <summary>
		/// Create a new game of Snap!
		/// </summary>
		public Snap ()
		{
			_deck = new Deck ();
		}

		/// <summary>
		/// Gets the card on the top of the "flip" stack. This card will be face up.
		/// </summary>
		/// <value>The top card.</value>
		public Card TopCard
		{
			get
			{
				return _topCards [1];
			}
		}

		/// <summary>
		/// Indicates if there are cards remaining in the Snap game's Deck.
		/// The game is over when there are no cards remaining.
		/// </summary>
		/// <value><c>true</c> if cards remain; otherwise, <c>false</c>.</value>
		public bool CardsRemain
		{
			get { return _deck.CardsRemaining > 0; }
		}

		/// <summary>
		/// Determines how many milliseconds need to pass before a new card is drawn
		/// and placed on the top of the game's card stack.
		/// </summary>
		/// <value>The flip time.</value>
		public int FlipTime
		{
			get { return _flipTime; }
			set { _flipTime = value; }
		}

		/// <summary>
		/// Indicates if the game has already been started. You can only start the game once.
		/// </summary>
		/// <value><c>true</c> if this instance is started; otherwise, <c>false</c>.</value>
		public bool IsStarted
		{
			get { return _started; }
		}

		/// <summary>
		/// Start the Snap game playing!
		/// </summary>
		public void Start()
		{
			if ( ! IsStarted )			// only start if not already started!
			{
				_started = true;
				_deck.Shuffle ();		// Return the cards and shuffle

				FlipNextCard ();		// Flip the first card...
			}
		}
			
		public void FlipNextCard()
		{
			if (_deck.CardsRemaining > 0)			// have cards...
			{
				_topCards [0] = _topCards [1];		// move top to card 2
				_topCards [1] = _deck.Draw ();		// get a new top card
				_topCards[1].TurnOver();			// reveal card
			}
		}

		/// <summary>
		/// Update the game. This should be called in the Game loop to enable
		/// the game to update its internal state.
		/// </summary>
		public void Update()
		{
			//TODO: implement update to automatically slip cards!
		}

		/// <summary>
		/// Gets the player's score.
		/// </summary>
		/// <value>The score.</value>
		public int Score(int idx)
		{
			if ( idx >= 0 && idx < _score.Length )
				return _score[idx]; 
			else
				return 0;
		}

		/// <summary>
		/// The player hit the top of the cards "snap"! :)
		/// Check if the top two cards' ranks match.
		/// </summary>
		public void PlayerHit (int player)
		{
			//TODO: consider deducting score for miss hits???
			if ( player >= 0 && player < _score.Length &&  	// its a valid player
				 IsStarted && 								// and the game is started
				 _topCards [0] != null && _topCards [0].Rank == _topCards [1].Rank) // and its a match
			{
				_score[player]++;
				//TODO: consider playing a sound here...
			}
            else if (player >= 0 && player < _score.Length)
            {
                _score[player]--;
            }

            // stop the game...
            _started = false;
		}
	
		#region Snap Game Unit Tests
		#if DEBUG

		public class SnapTests
		{
			[Test]
			public void TestSnapCreation()
			{
				Snap s = new Snap();

				Assert.IsTrue(s.CardsRemain);
				Assert.IsNull (s.TopCard);
			}

			[Test]
			public void TestFlipNextCard()
			{
				Snap s = new Snap();

				Assert.IsTrue(s.CardsRemain);
				Assert.IsNull (s.TopCard);

				s.FlipNextCard ();

				Assert.IsNull (s._topCards [0]);
				Assert.IsNotNull (s._topCards [1]);
			}
		}

		#endif 
		#endregion
	}
}

