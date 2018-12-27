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
    class TwoPairTest
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

        Hand AcesKings;
        Hand AcesQueens;
        Hand JacksTens;
        Hand QueensDeuces;
        Hand AceKing;
        Hand AceQueen;

        [SetUp]
        public void Init()
        {
            AcesKings = new Hand(new List<Card>()
            {
                new Card(SPADES,14),
                new Card(CLUBS,13)
            });
            AcesKings.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,4),
                new Card(DIAMONDS,12),
                new Card(HEARTS, 13),
                new Card(CLUBS, 5)
            });

            AcesQueens = new Hand(new List<Card>()
            {
                new Card(SPADES,14),
                new Card(CLUBS,12)
            });
            AcesQueens.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,4),
                new Card(DIAMONDS,12),
                new Card(HEARTS, 13),
                new Card(CLUBS, 5)
            });

            JacksTens = new Hand(new List<Card>()
            {
                new Card(SPADES,11),
                new Card(CLUBS,10)
            });
            JacksTens.Draw(new List<Card>()
            {
                new Card(CLUBS,11),
                new Card(HEARTS,12),
                new Card(DIAMONDS,10),
                new Card(HEARTS, 13),
                new Card(CLUBS, 2)
            });

            QueensDeuces = new Hand(new List<Card>()
            {
                new Card(SPADES,12),
                new Card(SPADES,2)
            });
            QueensDeuces.Draw(new List<Card>()
            {
                new Card(CLUBS,11),
                new Card(HEARTS,12),
                new Card(DIAMONDS,10),
                new Card(HEARTS, 13),
                new Card(CLUBS, 2)
            });

            AceKing = new Hand(new List<Card>()
            {
                new Card(SPADES,14),
                new Card(SPADES,13)
            });
            AceKing.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,3),
                new Card(DIAMONDS,10),
                new Card(HEARTS, 2),
                new Card(CLUBS, 2)
            });

            AceQueen = new Hand(new List<Card>()
            {
                new Card(HEARTS,14),
                new Card(HEARTS,12)
            });
            AceQueen.Draw(new List<Card>()
            {
                new Card(CLUBS,14),
                new Card(HEARTS,3),
                new Card(DIAMONDS,10),
                new Card(HEARTS, 2),
                new Card(CLUBS, 2)
            });
        }

        [Test]
        public void TestTwoPair()
        {
            Assert.True(AcesKings.CompareTo(AcesQueens) > 0);
        }

        [Test]
        public void Test2TwoPair()
        {
            Assert.True(QueensDeuces.CompareTo(JacksTens) > 0);
        }

        [Test]
        public void TestSameTwoPairsWithDifferentKicker()
        {
            Assert.True(AceKing.CompareTo(AceQueen) > 0);
        }
    }
}
