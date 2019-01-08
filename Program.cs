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
            Hand h1 = new Hand(new List<Card>()
            {
                new Card('s',14),
                new Card('c',14)
            });
            Hand h2 = new Hand(new List<Card>()
            {
                new Card('s',13),
                new Card('c',13)
            });

            List<Hand> hands = new List<Hand>();
            hands.Add(h1);
            hands.Add(h2);

            Simulator simulator = new Simulator(hands);
            simulator.Simulate(500);
            simulator.PrintResult();
            Console.ReadKey();
        }
    }
}
