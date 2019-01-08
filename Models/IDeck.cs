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
        ICard DrawRandomCard();
        ICard DrawFromIndex(int index);
        ICard DrawCard(ICard card);
        ICard DrawCard(int rank, char suit);
        void RemoveCard(ICard card);
        void InsertCard(ICard card);
        void NewDeck();
        void PrintDeck();
        int NumberOfCards();
    }
}
