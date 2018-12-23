using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PokerSimulator.Models;

namespace PokerSimulator.Tests
{
    [TestFixture]
    class HandTest
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

        Hand flush;
        Hand straight;
        Hand wheel;
        Hand onePair;
        Hand twoPairs;
        Hand threeOfAKind;
        Hand fullHouse;
        Hand fourOfAKind;
        Hand straightFlush;
        Hand notStraightFlush;
        Hand royalStraightFlush;

        [SetUp]
        public void Init()
        {
            royalStraightFlush = new Hand(new List<Card>()
            {
                new Card(SPADES,ACE),
                new Card(SPADES,KING)
            });
            royalStraightFlush.Draw(new List<Card>()
            {
                new Card(SPADES,QUEEN),
                new Card(SPADES,JACK),
                new Card(SPADES,TEN),
                new Card(CLUBS,ACE),
                new Card(CLUBS,KING)
            });

            notStraightFlush = new Hand(new List<Card>()
            {
                new Card(CLUBS,DEUCE),
                new Card(CLUBS,THREE)
            });
            notStraightFlush.Draw(new List<Card>()
            {
                new Card(CLUBS,FOUR),
                new Card(CLUBS,FIVE),
                new Card(CLUBS,QUEEN),
                new Card(SPADES,SIX),
                new Card(SPADES,SEVEN)
            });

            straightFlush = new Hand(new List<Card>()
            {
                new Card(CLUBS,DEUCE),
                new Card(CLUBS,THREE)
            });
            straightFlush.Draw(new List<Card>()
            {
                new Card(CLUBS,FOUR),
                new Card(CLUBS,FIVE),
                new Card(CLUBS,SIX)
            });

            fullHouse = new Hand(new List<Card>()
            {
                new Card(CLUBS,DEUCE),
                new Card(SPADES,DEUCE)
            });
            fullHouse.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(HEARTS,THREE),
                new Card(CLUBS,THREE)
            });

            fourOfAKind = new Hand(new List<Card>()
            {
                new Card(CLUBS,DEUCE),
                new Card(SPADES,DEUCE)
            });
            fourOfAKind.Draw(new List<Card>()
            {
                new Card(HEARTS,DEUCE),
                new Card(DIAMONDS,DEUCE),
                new Card(CLUBS,THREE)
            });

            onePair = new Hand(new List<Card>()
            {
                new Card(CLUBS,DEUCE),
                new Card(SPADES,DEUCE)
            });
            onePair.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(HEARTS,SIX),
                new Card(SPADES,EIGHT)
            });

            threeOfAKind = new Hand(new List<Card>()
            {
                new Card(CLUBS,EIGHT),
                new Card(HEARTS,DEUCE)
            });
            threeOfAKind.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(DIAMONDS,EIGHT),
                new Card(HEARTS,EIGHT)
            });

            flush = new Hand(new List<Card>()
            {
                new Card(SPADES,ACE),
                new Card(SPADES,DEUCE)
            });
            flush.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(SPADES,SIX),
                new Card(SPADES,EIGHT)
            });

            straight = new Hand(new List<Card>()
            {
                new Card(SPADES,ACE),
                new Card(CLUBS,KING),
            });
            straight.Draw(new List<Card>()
            {
                new Card(CLUBS,QUEEN),
                new Card(DIAMONDS,JACK),
                new Card(HEARTS,TEN)
            });

            wheel = new Hand(new List<Card>()
            {
                new Card(SPADES,ACE),
                new Card(DIAMONDS,DEUCE)
            });
            wheel.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(CLUBS,FOUR),
                new Card(HEARTS,FIVE)
            });

            twoPairs = new Hand(new List<Card>()
            {
                new Card(SPADES,DEUCE),
                new Card(DIAMONDS,DEUCE)
            });
            twoPairs.Draw(new List<Card>()
            {
                new Card(SPADES,THREE),
                new Card(CLUBS,FOUR),
                new Card(HEARTS,FOUR)
            });
        }

        [Test]
        public void TestPair()
        {
            Assert.True(onePair.IsPair());
            Assert.False(onePair.IsTwoPair());
        }

        [Test]
        public void TestTwoPairs()
        {
            Assert.True(twoPairs.IsTwoPair());
            Assert.False(twoPairs.IsThreeOfAKind());
        }

        [Test]
        public void ThreeOfAKind()
        {
            Assert.True(threeOfAKind.IsThreeOfAKind());
            Assert.False(twoPairs.IsThreeOfAKind());
        }

        [Test]
        public void TestStraight()
        {
            Assert.True(straight.IsStraight());
            Assert.True(wheel.IsStraight());
            Assert.False(flush.IsStraight());
        }

        [Test]
        public void TestFlush()
        {
            Assert.True(flush.IsFlush());
            Assert.False(straight.IsFlush());
        }

        [Test]
        public void TestFullHouse()
        {
            Assert.True(fullHouse.IsFullHouse());
            Assert.False(threeOfAKind.IsFullHouse());
            Assert.False(twoPairs.IsFullHouse());
        }

        [Test]
        public void TestFourOfAKind()
        {
            Assert.False(threeOfAKind.IsFourOfAKind());
            Assert.False(fullHouse.IsFourOfAKind());
            Assert.True(fourOfAKind.IsFourOfAKind());
        }

        [Test]
        public void TestStraightFlush()
        {
            Assert.True(straightFlush.IsStraightFlush());
            Assert.False(notStraightFlush.IsStraightFlush());
            Assert.True(royalStraightFlush.IsStraightFlush());
        }

        [Test]
        public void TestRoyalStraightFlush()
        {
            Assert.True(royalStraightFlush.IsRoyalStraightFlush());
            Assert.False(straightFlush.IsRoyalStraightFlush());
        }

        [Test]
        public void TestCompareHands()
        {
            Assert.True(royalStraightFlush.CompareTo(straightFlush) > 0);
            Assert.False(straightFlush.CompareTo(royalStraightFlush) > 0);
            Assert.True(fullHouse.CompareTo(flush) > 0);
        }
    }
}
