using EOrderBook.Entities;
using StockOrderBook.Strategies;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    

    public class OrderBook : IOrderBook
    {
        OrderQueue<Ask> _asks;
        OrderQueue<Bid> _bids;
        Object lockObj;

        

        public OrderBook(string ticker)
        {
            Ticker = ticker;
            _asks = new OrderQueue<Ask>(ticker, new AskOrderComparer());
            _bids = new OrderQueue<Bid>(ticker, new BidOrderComparer());
            lockObj = new Object();
        }

        public OrderQueue<Ask> Asks
        {
            get
            {
                return _asks;
            }
        }

        public OrderQueue<Bid> Bids
        {
            get
            {
                return _bids;
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
                if (_asks.Top != null)
                {
                    return _asks.Top.AskPrice;
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
                if (_bids.Top != null)
                {
                    return _bids.Top.BidPrice;
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
                return _asks.Queue.Sum(ao => ao.Volume) + _bids.Queue.Sum(bo => bo.Volume);
            }
        }

        public Ask TopAsk
        {
            get
            {
                return _asks.Top;
            }
        }

        public Bid TopBid
        {
            get
            {
                return _bids.Top;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Explicit intefrace implementation for matching engine.
        /////////////////////////////////////////////////////////////////////////////

        void IOrderBook.Remove(Ask ask)
        {
            lock(lockObj)
            {
                _asks.Remove(ask);
            }
        }

        void IOrderBook.Remove(Bid bid)
        {
            lock(lockObj)
            {
                _bids.Remove(bid);
            }
        }

        void IOrderBook.Remove(IList<Ask> asks)
        {
            lock(lockObj)
            {
                this._asks.Remove(asks);
            }
        }

        void IOrderBook.Remove(IList<Bid> bids)
        {
            lock(lockObj)
            {
                this._bids.Remove(bids);
            }
        }

        void IOrderBook.Add(Ask ask)
        {
            lock (lockObj)
            {
                _asks.Add(ask);
            }
        }

        void IOrderBook.Add(Bid bid)
        {
            lock (lockObj)
            {
                _bids.Add(bid);
            }
        }

        /////////////////////////////////////////////////////////////////////////////
    }
}
