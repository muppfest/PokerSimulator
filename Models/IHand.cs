﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSimulator.Models
{
    interface IHand<ICard> : IComparable<IHand<ICard>>
    {
        double GetRanking();
        List<ICard> GetHand();
        List<ICard> GetShowdownHand();
        void Draw(ICard card);
        void Draw(List<ICard> cards);
        bool IsPair();
        bool IsTwoPair();
        bool IsThreeOfAKind();
        bool IsStraight();
        bool IsFlush();
        bool IsFullHouse();
        bool IsFourOfAKind();
        bool IsStraightFlush();
        bool IsRoyalStraightFlush();
        void Print();
        List<ICard> Fold();
    }
}
