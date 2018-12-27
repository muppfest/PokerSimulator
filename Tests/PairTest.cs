using NUnit.Framework;
using PokerSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Tests
{
    [TestFixture]
    class PairTest
    {
        private const char CLUBS = 'c';
        private const char SPADES = 's';
        private const char HEARTS = 'h';
        private const char DIAMONDS = 'd';

        private const int DEUCE = 2;
        private const int THREE = 3;
        private const int FOUR = 4;
        private const int FIVE = 5;
        private const int SIX = 6;
        private const int SEVEN = 7;
        private const int EIGHT = 8;
        private const int NINE = 9;
        private const int TEN = 10;
        private const int JACK = 11;
        private const int QUEEN = 12;
        private const int KING = 13;
        private const int ACE = 14;

        Hand PairOfAcesKingKicker;
        Hand PairOfAcesQueenKicker;

        [SetUp]
        public void Init()
        {
            PairOfAcesKingKicker = new Hand(new List<Card>()
            {
                new Card(SPADES,14),
                new Card(CLUBS,13)
            });
            PairOfAcesKingKicker.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,4),
                new Card(DIAMONDS,2),
                new Card(HEARTS, 8),
                new Card(CLUBS, 10)
            });

            PairOfAcesQueenKicker = new Hand(new List<Card>()
            {
                new Card(SPADES,14),
                new Card(CLUBS,12)
            });
            PairOfAcesQueenKicker.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,4),
                new Card(DIAMONDS,2),
                new Card(HEARTS, 8),
                new Card(CLUBS, 10)
            });
        }

        [Test]
        public void TestKicker()
        {
            Assert.True(PairOfAcesKingKicker.CompareTo(PairOfAcesQueenKicker) > 0);
        }
    }
}
