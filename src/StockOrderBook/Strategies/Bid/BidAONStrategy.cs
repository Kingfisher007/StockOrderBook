using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class BidAONStrategy : BidTradingStrategy
    {

        public BidAONStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(asks, bids, tradebook)
        {
            
        }

        public override TradeExecutionResult Execute(Bid order)
        {
			ValidateParams(order);

            int cumVolume = 0;
            TradeResult result = TradeResult.NotTraded;
            Stack<Ask> matchedOrders = new Stack<Ask>();

			// Accumulate Ask orders to fill bid order 
			foreach(Ask ask in Asks.Orders)
			{
				if (ask.AskPrice > order.BidPrice)
				{
					break;
				}

				matchedOrders.Push(ask);
				cumVolume += ask.Volume;

				if (cumVolume == order.Volume)
				{
					result = TradeResult.Traded;
					break;
				}

				if (cumVolume > order.Volume)
				{
					// if cum volume exceeds bid volume, try to divide last ask order to match volumes					matchedOrders.Pop();
					cumVolume -= ask.Volume;

					if (ask.Trade != TradeType.AllOrNothing)
					{
						ask.TradedVolume = ask.Volume - (order.Volume - cumVolume);
						matchedOrders.Push(ask);
						cumVolume += ask.TradedVolume;
						result = TradeResult.Traded;
						break;
					}
					// trade not possible
					else
					{
						break;
					}
				}
			}

			// execute trade
			if (result == TradeResult.Traded)
			{
				// trades
				AddTrades(CreateTrades(order, matchedOrders));
				Asks.Remove(matchedOrders.ToList());
				result = TradeResult.Traded;
				// remove order since its traded
				Bids.Remove(order);
			}
			else
			{
				Bids.Add(order);
			}

			return new TradeExecutionResult(result, order.BidPrice, matchedOrders.Last().AskPrice);
        }
    }
}
