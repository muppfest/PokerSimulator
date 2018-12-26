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
            d.ShuffleDeck();
            d.ShuffleDeck();
            d.ShuffleDeck();

            Hand h1 = new Hand(new List<Card>(){
                d.DrawFromTopOfDeck(),
                d.DrawFromTopOfDeck()
            });

            h1.PrintHand();
            d.PrintDeck();

            Hand h2 = new Hand(new List<Card>
            {
                new Card('s',14),
                new Card('c',14),
                new Card('c',13),
                new Card('h',10),
                new Card('s',5)
            });

            Console.WriteLine(h2.GetRanking());

            Console.ReadKey();
        }
    }
}
