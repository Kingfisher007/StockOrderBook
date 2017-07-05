using System;
using System.Collections.Generic;
using StockOrderBook;
using StockOrderBook.Entities;

namespace StockOrderBook.Strategies
{
	public abstract class BaseTradingStrategy<T> : ITradingStrategy<T> where T : Order
	{
		TradeBook tradeBook;

		public BaseTradingStrategy(TradeBook tradebook)
		{
			tradeBook = tradebook;
		}

		public abstract TradeExecutionResult Execute(T order);

		protected void AddTrades(IList<Trade> trades)
		{
			tradeBook.AddRange(trades);
		}
	}
}
