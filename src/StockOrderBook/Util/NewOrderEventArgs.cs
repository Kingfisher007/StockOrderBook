using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;

namespace StockOrderBook.Util

{
    internal class NewOrderEventArgs<T> where T : Order
    {
        public NewOrderEventArgs(string ticker, OrderType type, T order)
        {
            Ticker = ticker;
            Type = type;
            Order = order;
        }

        public T Order
        {
            get;
            private set;
        }

        public string Ticker
        {
            get;
            private set;
        }

        public OrderType Type
        {
            get;
            private set;
        }
    }
}