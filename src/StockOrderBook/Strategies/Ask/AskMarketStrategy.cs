using System;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    internal class AskMarketStrategy : AskTradingStrategy
    {
        public AskMarketStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}