using StockOrderBook.Entities;
using System.Collections.Generic;

namespace StockOrderBook
{
    public interface IFillBook
    {
        void Add(Trade trade);
        void AddRange(IList<Trade> trade);
    }
}