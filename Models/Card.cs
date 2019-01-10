using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    class Card : ICard, IComparable<ICard>
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

        private bool IsFaceUp { get; set; }
        public char Suit { get; set; }
        public int Rank { get; set; }

        public Card(char suit, int rank)
        {
            IsFaceUp = false;
            Suit = suit;
            Rank = rank;
        }

        public void Flip()
        {
            if(IsFaceUp)
            {
                IsFaceUp = false;
            } else
            {
                IsFaceUp = true;
            }
        }

        private string RankToString()
        {
            switch (Rank)
            {
                case DEUCE:
                    return "Deuce";
                case THREE:
                    return "Three";
                case FOUR:
                    return "Four";
                case FIVE:
                    return "Five";
                case SIX:
                    return "Six";
                case SEVEN:
                    return "Seven";
                case EIGHT:
                    return "Eight";
                case NINE:
                    return "Nine";
                case TEN:
                    return "Ten";
                case JACK:
                    return "Jack";
                case QUEEN:
                    return "Queen";
                case KING:
                    return "King";
                case ACE:
                    return "Ace";
                default:
                    return "Rank does not exist";
            }
        }

        private string SuitToString()
        {
            switch (Suit)
            {
                case SPADES:
                    return "Spades";
                case CLUBS:
                    return "Clubs";
                case HEARTS:
                    return "Hearts";
                case DIAMONDS:
                    return "Diamonds";
                default:
                    return "Suit does not exist";
            }
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        override
        public string ToString()
        {
            return RankToString() + " of " + SuitToString();
        }

        public bool Equals(ICard card)
        {
            if (Suit == card.GetSuit() && Rank == card.GetRank()) return true;
            return false;
        }

        public int CompareTo(ICard card)
        {
            if (Rank > card.GetRank()) return 1;
            else if (Rank < card.GetRank()) return -1;
            else return 0;
        }

        public char GetSuit()
        {
            return Suit;
        }

        public int GetRank()
        {
            return Rank;
        }
    }
}
