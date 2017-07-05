using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class AskTradingStrategy : BaseTradingStrategy<Ask>
    {
        OrderQueue<Bid> Bids;

		protected AskTradingStrategy(OrderQueue<Bid> bids, TradeBook tradebook) : base(tradebook)
        {
            Bids = bids;
        }

		protected  List<Trade> CreateTrades(Ask order, IEnumerable<Bid> orders)
		{
			List<Trade> trades = new List<Trade>();
			foreach (Bid bid in orders)
			{
				trades.Add(new Trade(bid.Ticker, order.ID, bid.ID, bid.BidPrice, bid.TradedVolume));
			}
			return trades;
		}
    }
}
