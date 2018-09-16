using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOrderBook.Entities;

namespace StockOrderBook.Util

{
    public class NewOrderEventArgs
    {
        public NewOrderEventArgs(string ticker, OrderType type, Order order)
        {
            Ticker = ticker;
            Type = type;
            Order = order;
        }

        public Order Order
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