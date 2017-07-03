using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class BidTradingStrategy : ITradingStrategy<Bid>
    {
        protected OrderQueue<Ask> Asks;

        protected BidTradingStrategy(OrderQueue<Ask> asks)
        {
            Asks = asks;
        }

        public abstract TradeExecutionResult<Bid> Execute(Bid order);

		protected void CreateTrades(Guid bidOrderID, List<Trade> trades, IEnumerable<Ask> orders)
		{
			foreach (Ask askOrder in orders)
			{
				trades.Add(new Trade(askOrder.Ticker, bidOrderID, askOrder.ID, askOrder.AskPrice, askOrder.TradedVolume));
			}
		}
    }
}
