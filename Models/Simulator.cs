using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PokerSimulator.Models
{
    class Simulator
    {
        private List<Hand> winningHands;
        private List<Card> communityCards;
        private List<Hand> hands;
        private Deck deck;

        public Simulator()
        {
            
        }

        public void Simulate(int nbrOfTimes)
        {
            var watch = Stopwatch.StartNew();

            winningHands = new List<Hand>();

            int nbrOfRoyalStraightFlushes = 0;
            int nbrOfStraightFlushes = 0;
            int nbrOfFourOfAKind = 0;
            int nbrOfFullHouses = 0;
            int nbrOfFlushes = 0;
            int nbrOfStraights = 0;
            int nbrOfThreeOfAKind = 0;
            int nbrOfTwoPairs = 0;
            int nbrOfPairs = 0;
            int nbrOfHighHands = 0;

            for (int i = 0; i < nbrOfTimes; i++)
            {
                deck = new Deck();

                Hand hand = new Hand(new List<Card>()
                {
                    deck.DrawRandom(),
                    deck.DrawRandom()
                });

                hand.Draw(new List<Card>()
                {
                    deck.DrawRandom(),
                    deck.DrawRandom(),
                    deck.DrawRandom(),
                    deck.DrawRandom(),
                    deck.DrawRandom()
                });

                if (hand.IsRoyalStraightFlush()) nbrOfRoyalStraightFlushes++;
                else if (hand.IsStraightFlush()) nbrOfStraightFlushes++;
                else if (hand.IsFourOfAKind()) nbrOfFourOfAKind++;
                else if (hand.IsFullHouse()) nbrOfFullHouses++;
                else if (hand.IsFlush()) nbrOfFlushes++;
                else if (hand.IsStraight()) nbrOfStraights++;
                else if (hand.IsThreeOfAKind()) nbrOfThreeOfAKind++;
                else if (hand.IsTwoPair()) nbrOfTwoPairs++;
                else if (hand.IsPair()) nbrOfPairs++;
                else nbrOfHighHands++;

                winningHands.Add(hand);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            double royalStraightFlushPercentage = (double) nbrOfRoyalStraightFlushes/winningHands.Count;
            double straightFlushPercentage = (double) nbrOfStraightFlushes / winningHands.Count;
            double fourOfAKindPercentage = (double) nbrOfFourOfAKind / winningHands.Count;
            double fullHousePercentage = (double) nbrOfFullHouses / winningHands.Count;
            double flushPercentage = (double) nbrOfFlushes / winningHands.Count;
            double straightPercentage = (double) nbrOfStraights / winningHands.Count;
            double threeOfAKindPercentage = (double) nbrOfThreeOfAKind / winningHands.Count;
            double twoPairPercentage = (double) nbrOfTwoPairs / winningHands.Count;
            double onePairPercentage = (double) nbrOfPairs / winningHands.Count;
            double highHandPercentage = (double) nbrOfHighHands / winningHands.Count;

            Console.WriteLine("Number of trials: {0:#,0}", winningHands.Count);
            Console.WriteLine("Simulation time: {0:#,0} ms", elapsedMs);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Hand strength: \t\t Number of times: \t\t Percentage:");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Royal Straight Flush: \t {0:#,0} \t\t\t\t {1:P5}", nbrOfRoyalStraightFlushes, royalStraightFlushPercentage);
            Console.WriteLine("Straight Flush: \t {0:#,0} \t\t\t\t {1:P5}", nbrOfStraightFlushes, straightFlushPercentage);
            Console.WriteLine("Four of a kind: \t {0:#,0} \t\t\t\t {1:P5}", nbrOfFourOfAKind, fourOfAKindPercentage);
            Console.WriteLine("Full house: \t\t {0:#,0} \t\t\t {1:P5}", nbrOfFullHouses, fullHousePercentage);
            Console.WriteLine("Flush: \t\t\t {0:#,0} \t\t\t {1:P5}", nbrOfFlushes, flushPercentage);
            Console.WriteLine("Straight: \t\t {0:#,0} \t\t\t {1:P5}", nbrOfStraights, straightPercentage);
            Console.WriteLine("Three of a kind: \t {0:#,0} \t\t\t {1:P5}", nbrOfThreeOfAKind, threeOfAKindPercentage);
            Console.WriteLine("Two pair: \t\t {0:#,0} \t\t\t {1:P5}", nbrOfTwoPairs, twoPairPercentage);
            Console.WriteLine("One pair: \t\t {0:#,0} \t\t\t {1:P5}", nbrOfPairs, onePairPercentage);
            Console.WriteLine("High hand: \t\t {0:#,0} \t\t\t {1:P5}", nbrOfHighHands, highHandPercentage);
        }
    }
}
