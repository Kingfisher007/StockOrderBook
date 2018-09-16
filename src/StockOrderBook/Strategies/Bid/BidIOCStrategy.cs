using System;
using EOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class BidIOCStrategy : BidTradingStrategy
    {
        public BidIOCStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        protected override TradeExecutionResult ExecuteBid(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}