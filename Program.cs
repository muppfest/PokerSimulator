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
            Console.ReadKey();
        }
    }
}
