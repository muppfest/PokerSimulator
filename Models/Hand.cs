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

        private const int NUMBER_OF_CARDS_IN_SHOWDOWN = 5;

        private List<Card> HoleCards { get; set; }
        private List<Card> CommunityCards { get; set; }
        public List<Card> ShowdownCards { get; set; }

        public Hand(List<Card> cards)
        {
            HoleCards = cards;
            CommunityCards = new List<Card>();
            ShowdownCards = new List<Card>();
        }

        public Hand()
        {
            HoleCards = new List<Card>();
            CommunityCards = new List<Card>();
            ShowdownCards = new List<Card>();
        }

        public int CompareTo(IHand<Card> otherHand)
        {
            if (GetRanking() > otherHand.GetRanking()) return 1;
            else if (GetRanking() < otherHand.GetRanking()) return -1;
            else
            {
                List<Card> opponentHand = otherHand.GetShowdownHand().OrderByDescending(o => o.Rank).ToList();
                List<Card> ourHand = ShowdownCards.OrderByDescending(o => o.Rank).ToList();

                for(int i = 0; i < ourHand.Count-1; i++)
                {
                    if (ourHand[i].CompareTo(opponentHand[i]) > 0) return 1;
                    else if (ourHand[i].CompareTo(opponentHand[i]) < 0) return -1;
                }
            }
            return 0;
        }

        public double GetRanking()
        {
            double ranking = 0.0;

            if (IsRoyalStraightFlush()) return 9.0;
            else if (IsStraightFlush())
            {
                ShowdownCards = ShowdownCards.OrderBy(o => o.Rank).ToList();
                if (ShowdownCards[0].Rank == DEUCE && ShowdownCards[4].Rank == ACE) return 8.0;
                ranking = ShowdownCards.Sum(s => s.Rank) * 0.01;
                return ranking + 8.0;
            }
            else if (IsFourOfAKind())
            {
                ShowdownCards = ShowdownCards.OrderBy(o => o.Rank).ToList();
                if (ShowdownCards[0].Rank == ShowdownCards[3].Rank) ranking = ShowdownCards[0].Rank * 4 * 0.01;
                else ranking = ShowdownCards[4].Rank * 4 * 0.01;
                return ranking + 7.0;
            }
            else if (IsFullHouse())
            {
                ShowdownCards = ShowdownCards.OrderBy(o => o.Rank).ToList();
                if (ShowdownCards[0].Rank == ShowdownCards[2].Rank)
                {
                    ranking += ShowdownCards[0].Rank * 3 * 0.01;
                    ranking += ShowdownCards[4].Rank * 2 * 0.001;
                }
                else
                {
                    ranking += ShowdownCards[4].Rank * 3 * 0.01;
                    ranking += ShowdownCards[0].Rank * 2 * 0.001;
                }
                return ranking + 6.0;
            }
            else if (IsFlush())
            {
                ranking += ShowdownCards.Sum(s => s.Rank) * 0.01;
                return ranking + 5.0;
            }
            else if (IsStraight())
            {
                ShowdownCards = ShowdownCards.OrderBy(o => o.Rank).ToList();
                if (ShowdownCards[0].Rank == DEUCE && ShowdownCards[4].Rank == ACE) return 4.0;
                ranking = ShowdownCards.Sum(s => s.Rank) * 0.01;
                return ranking + 4.0;
            }
            else if (IsThreeOfAKind())
            {
                ShowdownCards = ShowdownCards.OrderByDescending(o => o.Rank).ToList();
                for (int i = 0; i < ShowdownCards.Count - 3; i++)
                {
                    if (ShowdownCards[i].Rank == ShowdownCards[i + 2].Rank)
                    {
                        ranking += ShowdownCards[i].Rank * 3 * 0.01;
                    }
                }

                return ranking + 3.0;
            }
            else if (IsTwoPair())
            {
                List<Card> pairs = new List<Card>();
                ShowdownCards = ShowdownCards.OrderByDescending(o => o.Rank).ToList();
                for (int i = 0; i < ShowdownCards.Count - 1; i++)
                {
                    if(ShowdownCards[i].Rank == ShowdownCards[i+1].Rank)
                    {
                        pairs.Add(ShowdownCards[i]);
                    }
                }
                ranking += pairs.Max(m => m.Rank) * 0.01;
                ranking += pairs.Min(m => m.Rank) * 0.001;
                
                return ranking + 2.0;
            }
            else if (IsPair())
            {
                ShowdownCards = ShowdownCards.OrderByDescending(o => o.Rank).ToList();
                for (int i = 0; i < ShowdownCards.Count - 1; i++)
                {
                    if (ShowdownCards[i].Rank == ShowdownCards[i + 1].Rank)
                    {
                        ranking += (ShowdownCards[i].Rank + ShowdownCards[i + 1].Rank) * 0.01;
                    }
                }
                return ranking + 1.0;
            }
            return ranking;
        }

        public bool IsFlush()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < NUMBER_OF_CARDS_IN_SHOWDOWN) return false;

            cards = cards.OrderBy(o => o.Suit).ThenByDescending(o => o.Rank).ToList();
            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Suit == cards[i + 4].Suit)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }
                    
            }
            return false;
        }

        public bool IsFourOfAKind()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-3; i++)
            {
                if (cards[i].Rank == cards[i + 3].Rank)
                {
                    ShowdownCards.AddRange(cards.GetRange(i, 4));
                    cards.RemoveRange(i, 4);
                    cards = cards.OrderByDescending(o => o.Rank).ToList();
                    ShowdownCards.Add(cards[0]);
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < NUMBER_OF_CARDS_IN_SHOWDOWN) return false;

            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 2].Rank && cards[i + 3].Rank == cards[i + 4].Rank)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }
                if (cards[i].Rank == cards[i + 1].Rank && cards[i + 2].Rank == cards[i + 4].Rank)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }
            }
            return false;
        }

        // FIXA DENNA METOD
        public bool IsRoyalStraightFlush()
        {
            if (!IsStraightFlush()) return false;

            List<Card> cards = GetAllCards();
            cards = cards.OrderBy(o => o.Suit).ThenBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == TEN && cards[i + 1].Rank == JACK
                    && cards[i + 2].Rank == QUEEN && cards[i + 3].Rank == KING && cards[i+4].Rank == ACE && cards[i].Suit == cards[i+4].Suit)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }
            }

            return false;
        }


        // KOLLA UPP WHEEL SF
        public bool IsStraightFlush()
        {
            if (!IsFlush() || !IsStraight()) return false;

            List<Card> cards = GetAllCards();
            List<Card> aces = cards.Where(w => w.Rank == ACE).ToList();

            cards = cards.OrderBy(o => o.Suit).ThenBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank - 1 && cards[i + 1].Rank == cards[i + 2].Rank - 1
                    && cards[i + 2].Rank == cards[i + 3].Rank - 1 && cards[i + 3].Rank == cards[i + 4].Rank - 1 && cards[i].Suit == cards[i+4].Suit)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }

                if (cards[i].Rank == DEUCE && cards[i+1].Rank == THREE && cards[i+2].Rank == FOUR && cards[i+3].Rank == FIVE && cards[i].Suit == cards[i+3].Suit 
                    && aces.Where(w => w.Suit == cards[i].Suit).Count() == 1)
                {
                    ShowdownCards = cards.GetRange(i, 4);
                    ShowdownCards.Add(aces.Where(w => w.Suit == cards[i].Suit).SingleOrDefault());
                    return true;
                }
            }

            return false;
        }

        public bool IsStraight()
        {
            List<Card> cards = GetAllCards();

            if (cards.Count < NUMBER_OF_CARDS_IN_SHOWDOWN) return false;

            cards = cards.OrderBy(o => o.Rank).ToList();
            Card[] temp = cards.ToArray();

            for(int i = 0; i < temp.Count()-1; i++)
            {
                if (temp[i].Rank == temp[i + 1].Rank) cards.RemoveAt(i);
            }
            
            for(int i = 0; i < cards.Count-4; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank - 1 && cards[i + 1].Rank == cards[i + 2].Rank - 1 
                    && cards[i + 2].Rank == cards[i + 3].Rank - 1 && cards[i + 3].Rank == cards[i+4].Rank-1)
                {
                    ShowdownCards = cards.GetRange(i, NUMBER_OF_CARDS_IN_SHOWDOWN);
                    return true;
                }
            }

            if (cards[0].Rank == DEUCE && cards[1].Rank == THREE && cards[2].Rank == FOUR && cards[3].Rank == FIVE && cards[cards.Count-1].Rank == ACE)
            {
                ShowdownCards = cards.GetRange(0, 4);
                ShowdownCards.Add(cards[cards.Count-1]);
                return true;
            }

            return false;
        }

        public bool IsThreeOfAKind()
        {
            List<Card> cards = GetAllCards();
            cards = cards.OrderByDescending(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-2; i++)
            {
                if (cards[i].Rank == cards[i + 2].Rank)
                {
                    ShowdownCards = cards.GetRange(i, 3);
                    cards.RemoveRange(i, 3);
                    cards = cards.OrderByDescending(o => o.Rank).ToList();
                    ShowdownCards.Add(cards[0]);
                    ShowdownCards.Add(cards[1]);
                    return true;
                }
            }
            return false;
        }

        public bool IsTwoPair()
        {
            List<Card> cards = GetAllCards();
            List<Card> showdown = new List<Card>();
            cards = cards.OrderBy(o => o.Rank).ToList();
            int pairCounter = 0;

            for (int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank)
                {
                    showdown.Add(cards[i]);
                    showdown.Add(cards[i + 1]);
                    pairCounter++;
                }
            }

            if (pairCounter == 2)
            {
                foreach(var card in showdown)
                {
                    cards.Remove(card);
                }
                cards = cards.OrderByDescending(o => o.Rank).ToList();
                showdown.Add(cards[0]);
                ShowdownCards = showdown;
                return true;
            } else if (pairCounter == 3)
            {
                showdown = showdown.OrderBy(o => o.Rank).ToList();
                showdown.RemoveRange(0, 2);
                foreach (var card in showdown)
                {
                    cards.Remove(card);
                }
                showdown.Add(cards[0]);
                ShowdownCards = showdown;
            }

            return false;
        }

        public bool IsPair()
        {
            List<Card> cards = GetAllCards();
            List<Card> showdown = new List<Card>();
            cards = cards.OrderBy(o => o.Rank).ToList();

            for(int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank)
                {
                    showdown.Add(cards[i]);
                    showdown.Add(cards[i + 1]);
                    cards.RemoveRange(i, 2);
                    cards = cards.OrderByDescending(o => o.Rank).ToList();
                    showdown.AddRange(cards.GetRange(0,3));
                    ShowdownCards = showdown;
                    return true;
                }
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

        public List<Card> GetShowdownHand()
        {
            return ShowdownCards;
        }
    }
}
