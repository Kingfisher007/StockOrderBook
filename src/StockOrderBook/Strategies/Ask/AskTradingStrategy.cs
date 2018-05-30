using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class AskTradingStrategy : BaseTradingStrategy
    {
		protected AskTradingStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Order order)
        {
            if (!(order is Ask))
            {
                throw new ArgumentException();
            }
            return ExecuteAsk(order as Ask);
        }

        protected abstract TradeExecutionResult ExecuteAsk(Ask Order);

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
