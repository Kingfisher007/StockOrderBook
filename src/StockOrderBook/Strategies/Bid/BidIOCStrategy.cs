using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class BidIOCStrategy : BidTradingStrategy
    {
        public BidIOCStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, FillBook tradebook) : base(asks, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}