using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;
using System.Collections.Generic;
using System.Linq;

namespace StockOrderBook.Strategies
{
    internal class AskIOCStrategy : AskTradingStrategy
    {
        public AskIOCStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(asks, bids, tradebook)
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
					// IOC allows partial
					else
					{
						order.TradedVolume = cumVolume;
						result = TradeResult.Traded;
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

			// remove order since its IOC
			Asks.Remove(order);

			return new TradeExecutionResult(result, order.AskPrice, matchedOrders.Last().BidPrice);
        }
    }
}