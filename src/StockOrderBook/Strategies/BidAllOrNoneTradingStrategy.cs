using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class BidAllOrNoneTradingStrategy : BidTradingStrategy
    {

        public BidAllOrNoneTradingStrategy(OrderQueue<Ask> asks) : base(asks)
        {
            
        }

        public override TradeExecutionResult<Bid> Execute(Bid order)
        {
            if(order == null)
            {
				throw new ArgumentNullException(nameof(order));
            }

            if(Asks == null 
               || (Asks != null && Asks.Orders == null)
              )
            {
                throw new ApplicationException("No orders to trade");
            }

            List<Trade> trades = new List<Trade>();
            int cumVolume = 0;
            Ask ask;
            TradeResult result = TradeResult.NotTraded;
            Stack<Ask> matchedOrders = new Stack<Ask>();

            var enumerator = Asks.Orders.GetEnumerator();
			// Accumulate Ask orders to fill bid order 
			while (enumerator.MoveNext())
			{
				ask = enumerator.Current;

				if (ask.AskPrice < order.BidPrice)
				{					break;
				}

				cumVolume += ask.Volume;
				matchedOrders.Push(ask);

				if (cumVolume == order.Volume)
				{
					break;
				}

				if (cumVolume > order.Volume)
				{
					// if cum volume exceeds bid volume, try to divide last ask order to match volumes
					if (ask.Trade == TradeType.AllowPartial)
					{
						matchedOrders.Pop();

						ask.TradedVolume = order.Volume - (cumVolume - ask.Volume); 
						matchedOrders.Push(ask);
						cumVolume += ask.TradedVolume;
					}
					// trade not possible
					else
					{
						break;
					}
				}
			}
			// If we got exact volumes to trade for bid order then execute the trade
			if (cumVolume == order.Volume)
			{
				// trades
				CreateTrades(order.ID, trades, matchedOrders);
				Asks.Remove(matchedOrders.ToList());
				result = TradeResult.Traded;
			}

            return new TradeExecutionResult<Bid>(result, order, trades);
        }
    }
}
