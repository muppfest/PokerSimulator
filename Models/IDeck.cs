using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    interface IDeck<ICard>
    {
        void ShuffleDeck();
        ICard DrawFromTopOfDeck();
        void NewDeck();
        void PrintDeck();
    }
}
