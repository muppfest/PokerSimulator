using PokerSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck d = new Deck();
            List<Card> holeCards = new List<Card>();
            List<Card> communityCards = new List<Card>();

            holeCards.Add(new Card('c', 6));
            holeCards.Add(new Card('d', 6));
            communityCards.Add(new Card('s', 12));
            communityCards.Add(new Card('s', 7));
            communityCards.Add(new Card('c', 7));
            communityCards.Add(new Card('h', 6));
            communityCards.Add(new Card('d', 14));

            Hand h = new Hand(holeCards);
            h.Draw(communityCards);

            h.PrintHand();
            Console.WriteLine(h.IsFullHouse().ToString());
            h.PrintHand();
            Console.ReadKey();
        }
    }
}
