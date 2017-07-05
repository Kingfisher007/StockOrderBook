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
			if (!TradeInProgress)
			{
				try
				{
					TradeInProgress = true;
					BidTradingStrategy tradingStrategy = BidStrategies[eventArgs.Order.Trade];
					if (tradingStrategy != null)
					{
						TradeExecutionResult<Bid> traderesult = tradingStrategy.Execute(eventArgs.Order);
					}
				}
				catch (Exception expn)
				{

				}
				finally
				{
					TradeInProgress = false;
				}
			}
        }

		private void Asks_TopOrderAdded(OrderQueue<Ask> sender, TopOrderChangedEventArgs<Ask> eventArgs)
        {
            if (!TradeInProgress)
			{
				try
				{
					TradeInProgress = true;
					AskTradingStrategy tradingstrategy = AskStrategies[eventArgs.Order.Trade];
					if (tradingstrategy != null)
					{
						TradeExecutionResult<Ask> traderesult = tradingstrategy.Execute(eventArgs.Order);
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
