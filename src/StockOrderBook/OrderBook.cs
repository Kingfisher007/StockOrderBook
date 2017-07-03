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
    class OrderBook
    {
        OrderQueue<Ask> Asks;
        OrderQueue<Bid> Bids;
        Dictionary<TradeType, AskTradingStrategy> AskStrategies;
        Dictionary<TradeType, BidTradingStrategy> BidStrategies;
        volatile bool TradeInProgress;
        Object lockObj;

        public OrderBook(string ticker, Dictionary<TradeType, AskTradingStrategy> askStrategies, Dictionary<TradeType, BidTradingStrategy> bidStrategies)
        {
            Ticker = ticker;
            Asks = new OrderQueue<Ask>(ticker, new AskOrderComparer());
            Bids = new OrderQueue<Bid>(ticker, new BidOrderComparer());
            AskStrategies = askStrategies;
            BidStrategies = bidStrategies;
            TradeInProgress = false;
            lockObj = new Object();

            Asks.TopOrderChanged += Asks_TopOrderChanged;
            Bids.TopOrderChanged += Bids_TopOrderChanged;
        }

        public string Ticker
        {
            get;
            protected set;
        }

        public bool Ask(Ask ask)
        {
            return Asks.Add(ask);
        }

        public bool Bid(Bid bid)
        {
            return Bids.Add(bid);
        }

        private void Bids_TopOrderChanged(object sender, TopOrderChangedEventArgs eventArgs)
        {
			if (!TradeInProgress)
			{
				TradeInProgress = true;

				TradeInProgress = false;
			}
        }

        private void Asks_TopOrderChanged(object sender, TopOrderChangedEventArgs eventArgs)
        {
            if (!TradeInProgress)
			{
				TradeInProgress = true;

				TradeInProgress = false;	
			}
        }
    }
}
