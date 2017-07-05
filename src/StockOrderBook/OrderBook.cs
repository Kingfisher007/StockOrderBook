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

			Asks.TopOrderAdded += Asks_TopOrderAdded;
			Bids.TopOrderAdded += Bids_TopOrderAdded;
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
				if (Asks.Top != null)
				{
					return Asks.Top.AskPrice;
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
				if (Bids.Top != null)
				{					return Bids.Top.BidPrice;
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
				return Asks.Orders.Sum(ao => ao.Volume) + Bids.Orders.Sum(bo => bo.Volume); 
			}
		}

		public float LastTradeAsk
		{
			get;
			protected set;
		}

		public float LastTradeBid
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

		private void Bids_TopOrderAdded(OrderQueue<Bid> sender, TopOrderChangedEventArgs<Bid> eventArgs)
        {
			ExecuteTrade<Bid>(BidStrategies[eventArgs.Order.Trade], eventArgs.Order);
        }

		private void Asks_TopOrderAdded(OrderQueue<Ask> sender, TopOrderChangedEventArgs<Ask> eventArgs)
		{
			ExecuteTrade<Ask>(AskStrategies[eventArgs.Order.Trade], eventArgs.Order);
        }

		private void ExecuteTrade<T>(ITradingStrategy<T> tradingStrategy, T order) where T : Order
		{
			if (!TradeInProgress)
			{
				try
				{
					if (tradingStrategy != null)
					{
						TradeInProgress = true;
						TradeExecutionResult traderesult = tradingStrategy.Execute(order);
						if (traderesult.Result == TradeResult.Traded)
						{
							LastTradeAsk = traderesult.AskPrice;
							LastTradeBid = traderesult.BidPrice;
						}
					}
				}
				catch (Exception Expn)
				{

				}
				finally
				{
					TradeInProgress = false;
				}
			}
		}
    }
}
