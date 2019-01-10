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
            Simulator s = new Simulator();
            s.Simulate(1029112);
            Console.WriteLine("\nPress any key to close program.");
            Console.ReadKey();
        }
    }
}
