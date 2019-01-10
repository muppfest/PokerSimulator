using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    interface IDeck<ICard>
    {
        void Shuffle();
        ICard DrawFromTop();
        ICard DrawRandom();
        ICard DrawFromIndex(int index);
        ICard Draw(ICard card);
        ICard Draw(int rank, char suit);
        void Remove(ICard card);
        void Remove(int index);
        void Insert(ICard card);
        void NewDeck();
        void PrintDeck();
        int Size();
    }
}
