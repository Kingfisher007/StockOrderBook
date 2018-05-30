using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    public delegate void NewOrder(OrderManager orderBook, NewOrderEventArgs newOrder);

    public class OrderManager
    {
        public event NewOrder NewOrderReceived;

        public bool NewAsk(Ask ask)
        {
            try
            {
                NewOrderReceived?.Invoke(this, new NewOrderEventArgs(ask.Ticker, OrderType.Ask, ask));
                return true;
            }
            catch (Exception expn)
            {
                return false;
            }
        }

        public bool NewBid(Bid bid)
        {
            try
            {
                NewOrderReceived?.Invoke(this, new NewOrderEventArgs(bid.Ticker, OrderType.Bid, bid));
                return true;
            }
            catch (Exception expn)
            {
                return false;
            }
        }
    }
}
