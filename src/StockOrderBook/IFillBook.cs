using EOrderBook.Entities;
using StockOrderBook.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StockOrderBook
{
    public interface IFillBook
    {
        ObservableCollection<Trade> Trades
        {
            get;
        }
        string Ticker { get; }

        void Add(Trade trade);
        void AddRange(IList<Trade> trade);
    }
}