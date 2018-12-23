using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    interface ICard : IComparable<ICard>
    {
        void FlipCard();
        void PrintCard();
    }
}
