using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class BidTradingStrategy : BaseTradingStrategy
    {
		protected BidTradingStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Order order)
        {
            if(!(order is Bid))
            {
                throw new ArgumentException();
            }
            return ExecuteBid(order as Bid);
        }

        protected abstract TradeExecutionResult ExecuteBid(Bid Order);

        protected List<Trade> CreateTrades(Bid order, IEnumerable<Ask> orders)
		{
			List<Trade> trades = new List<Trade>();
			foreach (Ask askOrder in orders)
			{
				trades.Add(new Trade(order.Ticker, order.ID, askOrder.ID, askOrder.AskPrice, askOrder.TradedVolume));
			}
			return trades;
		}
    }
}
