using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class AskMarketStrategy : AskTradingStrategy
    {
        public AskMarketStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, FillBook tradebook) : base(asks, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}