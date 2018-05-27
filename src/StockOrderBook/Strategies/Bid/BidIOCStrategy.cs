using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class BidIOCStrategy : BidTradingStrategy
    {
        public BidIOCStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}