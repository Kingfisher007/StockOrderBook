﻿using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class BidTradingStrategy : BaseTradingStrategy<Bid>
    {
		protected BidTradingStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, FillBook tradebook) : base(asks, bids, tradebook)
        {

        }

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