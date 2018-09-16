using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class AskFOKStrategy : AskTradingStrategy
    {
        public AskFOKStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        protected override TradeExecutionResult ExecuteAsk(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
