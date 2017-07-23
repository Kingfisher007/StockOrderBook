using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class BidMarketStrategy : BidTradingStrategy
    {
        public BidMarketStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(asks, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}