using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    class Deck : IDeck<Card>
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

        public List<Card> Cards { get; set; }

        public Deck()
        {
            NewDeck();
        }

        public Card DrawFromTop()
        {
            Card topCard = Cards.ElementAt(Cards.Count - 1);
            Remove(Cards.Count - 1);
            return topCard;
        }

        public void NewDeck()
        {
            Cards = new List<Card>();

            for (int i = DEUCE; i <= ACE; i++)
            {
                Card cardSpades = new Card(SPADES, i);
                Card cardClubs = new Card(CLUBS, i);
                Card cardHearts = new Card(HEARTS, i);
                Card cardDiamonds = new Card(DIAMONDS, i);

                Cards.Add(cardSpades);
                Cards.Add(cardClubs);
                Cards.Add(cardHearts);
                Cards.Add(cardDiamonds);
            }
        }

        public void PrintDeck()
        {
            foreach (var card in Cards)
            {
                card.Print();
            }
        }

        public void Shuffle()
        {
            Card[] cards = Cards.ToArray();

            for (int i = 0; i < cards.Count() - 1; i++)
            {
                int r = StandardLockSingleRandom.Next(0, cards.Count() - i);
                Card randomCard = cards[r];
                cards[r] = cards[i];
                cards[i] = randomCard;
            }
            Cards = cards.ToList();
        }

        public Card DrawFromIndex(int index)
        {
            Card card = Cards.ElementAt(index);
            Remove(index);
            return card;
        }

        public void Remove(Card card)
        {
            Cards.Remove(card);
        }

        public void Remove(int index)
        {
            Cards.RemoveAt(index);
        }

        public void Insert(Card card)
        {
            Cards.Insert(Cards.Count - 1, card);
        }

        public Card Draw(Card card)
        {
            Remove(card);
            return card;
        }

        public Card Draw(int rank, char suit)
        {
            Card card = Cards.Where(w => w.Rank == rank && w.Suit == suit).SingleOrDefault();
            Remove(card);
            return card;
        }

        public Card DrawRandom()
        {
            int r = StandardLockSingleRandom.Next(0, Cards.Count());
            Card randomCard = Cards.ElementAt(r);
            Remove(r);
            return randomCard;
        }

        public int Size()
        {
            return Cards.Count;
        }
    }
}
