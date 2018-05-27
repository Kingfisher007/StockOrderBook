﻿using StockOrderBook.Entities;
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

        public BidAONStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {
            
        }

        public override TradeExecutionResult Execute(Bid order)
        {
            if(order == null)
            {
				throw new ArgumentNullException(nameof(order));
            }

            IEnumerable<Ask> Asks = Orderbook.Asks;

            if(Asks == null)
            {
                throw new ApplicationException("No orders to trade");
            }

            int cumVolume = 0;
            Ask ask;
            TradeStatus result = TradeStatus.NotTraded;
            Stack<Ask> matchedOrders = new Stack<Ask>();

            var enumerator = Asks.GetEnumerator();
			// Accumulate Ask orders to fill bid order 
			while (enumerator.MoveNext())
			{
				ask = enumerator.Current;

				if (ask.AskPrice > order.BidPrice)
				{
					break;
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
					if (ask.Trade == TradeType.None)
					{
						matchedOrders.Pop();

						ask.TradedVolume = order.Volume - (cumVolume - ask.Volume); 
						matchedOrders.Push(ask);
						cumVolume += ask.TradedVolume;
						break;
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
                AddTrades(CreateTrades(order, matchedOrders));
				Orderbook.Remove(matchedOrders.ToList());
				result = TradeStatus.Traded;
			}

			return new TradeExecutionResult(result, order.BidPrice, order.Volume);
        }
    }
}
