using StockOrderBook.Entities;
using StockOrderBook.Strategies;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    

    class OrderBook : IOrderBook
    {
        OrderQueue<Ask> asks;
        OrderQueue<Bid> bids;
        Object lockObj;

        public event NewOrder NewOrderReceived;

        public OrderBook(string ticker)
        {
            Ticker = ticker;
            asks = new OrderQueue<Ask>(ticker, new AskOrderComparer());
            bids = new OrderQueue<Bid>(ticker, new BidOrderComparer());
            lockObj = new Object();
        }

        public IEnumerable<Ask> Asks
        {
            get
            {
                return Asks;
            }
        }

        public IEnumerable<Bid> Bids
        {
            get
            {
                return Bids;
            }
        }

        public string Ticker
        {
            get;
            protected set;
        }

        public float AskPrice
        {
            get
            {
                if (asks.Top != null)
                {
                    return asks.Top.AskPrice;
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public float BidPrice
        {
            get
            {
                if (bids.Top != null)
                {
                    return bids.Top.BidPrice;
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public int Volume
        {
            get
            {
                return asks.Orders.Sum(ao => ao.Volume) + bids.Orders.Sum(bo => bo.Volume);
            }
        }

        public Ask TopAsk
        {
            get;
            protected set;
        }

        public Bid TopBid
        {
            get;
            protected set;
        }

        public bool Ask(Ask ask)
        {
            try
            {
                NewOrderReceived?.Invoke(this, new NewOrderEventArgs(Ticker, OrderType.Ask, ask));
                return true;
            }
            catch (Exception expn)
            {
                return false;
            }
        }

        public bool Bid(Bid bid)
        {
            try
            {
                NewOrderReceived?.Invoke(this, new NewOrderEventArgs(Ticker, OrderType.Bid, bid));
                return true;
            }
            catch (Exception expn)
            {
                return false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Explicit intefrace implementation for matching engine.
        /////////////////////////////////////////////////////////////////////////////

        void IOrderBook.Remove(Ask ask)
        {
            lock(lockObj)
            {
                asks.Remove(ask);
            }
        }

        void IOrderBook.Remove(Bid bid)
        {
            lock(lockObj)
            {
                bids.Remove(bid);
            }
        }

        void IOrderBook.Remove(IList<Ask> asks)
        {
            
        }

        void IOrderBook.Remove(IList<Bid> bids)
        {

        }

        void IOrderBook.Add(Ask ask)
        {
            lock (lockObj)
            {
                asks.Add(ask);
            }
        }

        void IOrderBook.Add(Bid bid)
        {
            lock (lockObj)
            {
                bids.Add(bid);
            }
        }

        /////////////////////////////////////////////////////////////////////////////
    }
}
