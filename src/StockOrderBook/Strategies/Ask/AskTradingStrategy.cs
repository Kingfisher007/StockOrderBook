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
		protected AskTradingStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(asks, bids, tradebook)
        {

        }

		protected bool ValidateParams(Ask order)
		{
			if(order == null)
            {
				throw new ArgumentNullException(nameof(order));
            }

			if(Bids == null 
               || (Bids != null && Bids.Orders == null)
              )
            {
                throw new ApplicationException("No orders to trade");
            }

			return true;
		}

		protected  List<Trade> CreateTrades(Ask order, IEnumerable<Bid> orders)
		{
			List<Trade> trades = new List<Trade>();
			foreach (Bid bid in orders)
			{
				trades.Add(new Trade(order.Ticker, order.ID, bid.ID, bid.BidPrice, bid.TradedVolume));
			}
			return trades;
		}
    }
}
