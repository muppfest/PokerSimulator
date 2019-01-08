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
    class DeckTest
    {
        private Deck deck;

        [SetUp]
        public void init()
        {
            deck = new Deck();
        }

        [Test]
        public void testRemoveCard()
        {
            Card c = new Card('s', 14);
            deck.RemoveCard(c);

            bool cardExist = false;

            foreach(var card in deck.Cards)
            {
                if (card.Equals(c)) cardExist = true;
            }

            Assert.True(cardExist == false);
            Assert.True(deck.NumberOfCards() == 51);

            
        }
    }
}
