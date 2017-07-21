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
        ITradingStrategyProvider TradingStrategyProvider
        volatile bool TradeInProgress;
        Object lockObj;

        public OrderBook(string ticker, ITradingStrategyProvider tradingStrategyProvider)
        {
            Ticker = ticker;
            Asks = new OrderQueue<Ask>(ticker, new AskOrderComparer());
            Bids = new OrderQueue<Bid>(ticker, new BidOrderComparer());
            TradingStrategyProvider = tradingStrategyProvider;
            TradeInProgress = false;
            lockObj = new Object();

			TradingStrategyProvider.Initialise(Asks, Bids);
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
				{
					return Bids.Top.BidPrice;
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
			try
			{
				ExecuteTrade(TradingStrategyProvider.GetAskStrategy(ask.Trade), ask);
				return true;
			}
			catch(Exception expn)
			{
				return false;
			}
        }

        public bool Bid(Bid bid)
        {
			try
			{
				ExecuteTrade(TradingStrategyProvider.GetBidStrategy(bid.Trade), bid);
				return true;
			}
			catch(Exception expn)
			{
				return false;
			}
        }

		private void ExecuteTrade<T>(ITradingStrategy<T> tradingStrategy, T order) where T : Order
		{
			try
			{
				if (tradingStrategy != null)
				{
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
		}
    }
}
