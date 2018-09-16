using System;
using EOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class AskIOCStrategy : AskTradingStrategy
    {
        public AskIOCStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {
        }

        protected override TradeExecutionResult ExecuteAsk(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}