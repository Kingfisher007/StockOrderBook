using System;
using System.Collections.Generic;
using StockOrderBook;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
	public abstract class BaseTradingStrategy<T> : ITradingStrategy<T> where T : Order
	{
		protected OrderQueue<Ask> Asks;
		protected OrderQueue<Bid> Bids;
		FillBook Tradebook;

		public BaseTradingStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids,FillBook tradebook)
		{
			Asks = asks;
			Bids = bids;
			Tradebook = tradebook;
		}

		public abstract TradeExecutionResult Execute(T order);

		protected void AddTrades(IList<Trade> trades)
		{
			Tradebook.AddRange(trades);
		}
	}
}
