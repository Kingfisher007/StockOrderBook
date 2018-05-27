using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class AskAONStrategy : AskTradingStrategy
    {
        public AskAONStrategy(IOrderBook orderbook, IFillBook tradebook) :base(orderbook, tradebook)
        {
            
        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
