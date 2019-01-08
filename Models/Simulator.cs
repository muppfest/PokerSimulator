using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    class Simulator
    {
        private List<Hand> winningHands;
        private List<Card> communityCards;
        private List<Hand> hands;

        public Simulator(List<Hand> hands)
        {
            this.hands = hands;
        }

        public Simulator(List<Hand> hands, List<Card> communityCards)
        {
            this.hands = hands;
            this.communityCards = communityCards;
        }

        public Deck GetDeckWithRemovedDuplicates()
        {
            Deck deck = new Deck();
            List<Card> holeCards = new List<Card>();
            foreach (var hand in hands)
            {
                holeCards.AddRange(hand.GetHand());
            }
            foreach(var card in deck.Cards)
            {
                foreach(var holeCard in holeCards)
                {
                    if (holeCard.Equals(card)) deck.RemoveCard(card);
                }
            }

            return deck;
        }

        public void Simulate(int nbrOfTimes)
        {
            winningHands = new List<Hand>();

            for (int i = 0; i < nbrOfTimes; i++)
            {
                List<Card> tempCommunityCards = communityCards;
                Deck tempDeck = GetDeckWithRemovedDuplicates();

                if (tempCommunityCards == null)
                {
                    tempCommunityCards = new List<Card>();
                    tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    tempCommunityCards.Add(tempDeck.DrawRandomCard());
                }
                else
                {
                    if (tempCommunityCards.Count == 3)
                    {
                        tempCommunityCards.Add(tempDeck.DrawRandomCard());
                        tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    }
                    else if (tempCommunityCards.Count == 4)
                    {
                        tempCommunityCards.Add(tempDeck.DrawRandomCard());
                    }
                }

                foreach (var hand in hands)
                {
                    hand.Draw(tempCommunityCards);
                }

                Hand winningHand = hands[0];

                foreach (var hand in hands)
                {
                    if (hand.CompareTo(winningHand) > 0) winningHand = hand;
                }

                winningHands.Add(winningHand);
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("{0} trials", winningHands.Count);
            Console.WriteLine("--------------------------");

            foreach (var hand in hands)
            {
                int numberOfWins = 0;

                for(int i = 0; i < winningHands.Count-1; i++)
                {
                    if (hand.Equals(winningHands[i])) numberOfWins++;
                }

                double equity = (double) numberOfWins / winningHands.Count;
                Console.WriteLine("Equity: {0:P}% Wins: {1}", equity, numberOfWins);
                Console.WriteLine("--------------------------");
            }
        }
    }
}
