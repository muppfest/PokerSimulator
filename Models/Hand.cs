using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    class Hand : IHand<Card>, IComparable<IHand<Card>>
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

        private List<Card> HoleCards { get; set; }
        private List<Card> CommunityCards { get; set; }

        public Hand(List<Card> cards)
        {
            HoleCards = cards;
            CommunityCards = new List<Card>();
        }

        public Hand()
        {
            HoleCards = new List<Card>();
            CommunityCards = new List<Card>();
        }

        public int CompareTo(IHand<Card> otherHand)
        {
            if (GetRanking() > otherHand.GetRanking()) return 1;
            else if (GetRanking() < otherHand.GetRanking()) return -1;
            else return 0;
        }

        public double GetRanking()
        {
            if (IsRoyalStraightFlush()) return 9.0;
            else if (IsStraightFlush()) return 8.0;
            else if (IsFourOfAKind()) return 7.0;
            else if (IsFullHouse()) return 6.0;
            else if (IsFlush()) return 5.0;
            else if (IsStraight()) return 4.0;
            else if (IsThreeOfAKind()) return 3.0;
            else if (IsTwoPair()) return 2.0;
            else if (IsPair()) return 1.0;
            else return 0.0;
        }

        public bool IsFlush()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < 5) return false;

            cards = cards.OrderBy(o => o.Suit).ToList();
            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Suit == cards[i + 4].Suit) return true;
            }
            return false;
        }

        public bool IsFourOfAKind()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-3; i++)
            {
                if (cards[i].Rank == cards[i + 3].Rank) return true;
            }
            return false;
        }

        public bool IsFullHouse()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < 5) return false;

            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 2].Rank && cards[i + 3].Rank == cards[i + 4].Rank) return true;
                if (cards[i].Rank == cards[i + 1].Rank && cards[i + 2].Rank == cards[i + 4].Rank) return true;
            }
            return false;
        }

        // FIXA DENNA METOD
        public bool IsRoyalStraightFlush()
        {
            if (!IsStraightFlush()) return false;

            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Suit).ToList();

            if(cards[0].Suit == cards[4].Suit)
            {
                List<Card> suitedCards = cards.GetRange(0, 5);
                suitedCards = suitedCards.OrderBy(o => o.Rank).ToList();

                if (suitedCards[4].Rank != ACE) return false;
            }
            else
            {
                cards = cards.OrderByDescending(o => o.Suit).ToList();

                List<Card> suitedCards = cards.GetRange(0, 5);
                suitedCards = suitedCards.OrderBy(o => o.Rank).ToList();

                if (suitedCards[4].Rank != ACE) return false;
            }

            return true;
        }


        // KOLLA UPP WHEEL SF
        public bool IsStraightFlush()
        {
            if (!IsFlush() || !IsStraight()) return false;

            List<Card> cards = GetAllCards();

            cards = cards.OrderBy(o => o.Suit).ThenBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank - 1 && cards[i + 1].Rank == cards[i + 2].Rank - 1
                    && cards[i + 2].Rank == cards[i + 3].Rank - 1 && cards[i + 3].Rank == cards[i + 4].Rank - 1 && cards[i].Suit == cards[i+4].Suit) return true;

                if (cards[i].Rank == DEUCE && cards[i+1].Rank == THREE && cards[i+2].Rank == FOUR && cards[i+3].Rank == FIVE && cards.Max(m => m.Rank) == ACE
                    && cards[i].Suit == cards[i+4].Suit) return true;
            }

            return false;
        }

        public bool IsStraight()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < 5) return false;

            cards = cards.OrderBy(o => o.Rank).ToList();
            Card[] temp = cards.ToArray();

            for(int i = 0; i < temp.Count()-1; i++)
            {
                if (temp[i].Rank == temp[i + 1].Rank) cards.RemoveAt(i);
            }
            
            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank - 1 && cards[i + 1].Rank == cards[i + 2].Rank - 1 
                    && cards[i + 2].Rank == cards[i + 3].Rank - 1 && cards[i + 3].Rank == cards[i+4].Rank-1) return true;
            }

            if (cards[0].Rank == DEUCE && cards[1].Rank == THREE && cards[2].Rank == FOUR && cards[3].Rank == FIVE && cards[cards.Count-1].Rank == ACE) return true;

            return false;
        }

        public bool IsThreeOfAKind()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-2; i++)
            {
                if (cards[i].Rank == cards[i + 2].Rank) return true;
            }
            return false;
        }

        public bool IsTwoPair()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Rank).ToList();
            int pairCounter = 0;

            for (int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank) pairCounter++;
                if (pairCounter >= 2) return true;
            }

            return false;
        }

        public bool IsPair()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank) return true;
            }
            return false;
        }

        public void PrintHand()
        {
            foreach (var card in HoleCards)
            {
                card.PrintCard();
            }
        }

        public void Draw(Card communityCard)
        {
            CommunityCards.Add(communityCard);
        }

        public void Draw(List<Card> communityCards)
        {
            CommunityCards.AddRange(communityCards);
        }

        public List<Card> Fold()
        {
            List<Card> foldedCards = HoleCards;
            HoleCards = null;
            CommunityCards = null;
            return foldedCards;
        }

        private List<Card> GetAllCards()
        {
            List<Card> allCards = new List<Card>();
            allCards.AddRange(HoleCards);
            allCards.AddRange(CommunityCards);
            return allCards;
        }
    }
}
