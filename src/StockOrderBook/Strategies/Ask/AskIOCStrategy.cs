using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class AskIOCStrategy : AskTradingStrategy
    {
        public AskIOCStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {
        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}