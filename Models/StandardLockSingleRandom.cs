using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    public static class StandardLockSingleRandom
    {
        private static Random random = new Random();
        private static object _lock = new object();

        public static int Next()
        {
            lock (_lock)
            {
                return random.Next();
            }
        }

        public static int Next(int min, int max)
        {
            lock (_lock)
            {
                return random.Next(min, max);
            }
        }

        public static double NextDouble()
        {
            lock (_lock)
            {
                return random.NextDouble();
            }
        }
    }
}
