using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class AskFOKStrategy : AskTradingStrategy
    {
        public AskFOKStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(asks, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Ask order)
        {
            ValidateParams(order);

			int cumVolume = 0;
			TradeResult result = TradeResult.NotTraded;
			Stack<Bid> matchedOrders = new Stack<Bid>();

			// Accumulate Ask orders to fill bid order 
			foreach (Bid bid in Bids.Orders)
			{
				if (order.AskPrice > bid.BidPrice)
				{
					break;
				}

				matchedOrders.Push(bid);
				cumVolume += bid.Volume;

				if (cumVolume == order.Volume)
				{
					result = TradeResult.Traded;
					break;
				}

				if (cumVolume > order.Volume)
				{
					// if cum volume exceeds ask volume, try to divide last bid order to match volumes
					matchedOrders.Pop();
					cumVolume -= bid.Volume;

					if (bid.Trade != TradeType.AllOrNothing)
					{
						bid.TradedVolume = bid.Volume - (order.Volume - cumVolume);

						matchedOrders.Push(bid);
						cumVolume += bid.TradedVolume;
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
				Bids.Remove(matchedOrders.ToList());
				result = TradeResult.Traded;
			}

			// remove order since its FOK
			Asks.Remove(order);


			return new TradeExecutionResult(result, order.AskPrice, matchedOrders.Last().BidPrice);
        }
    }
}
