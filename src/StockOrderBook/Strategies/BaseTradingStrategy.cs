using System;
using System.Collections.Generic;
using StockOrderBook;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
	public abstract class BaseTradingStrategy<T> : ITradingStrategy<T> where T : Order
	{
		protected IOrderBook Orderbook;
		protected IFillBook Fillbook;

		public BaseTradingStrategy(IOrderBook orderbook, IFillBook tradebook)
		{
            Orderbook = orderbook;
            Fillbook = tradebook;
		}

		public abstract TradeExecutionResult Execute(T order);

		protected void AddTrades(IList<Trade> trades)
		{
			Fillbook.AddRange(trades);
		}
	}
}
