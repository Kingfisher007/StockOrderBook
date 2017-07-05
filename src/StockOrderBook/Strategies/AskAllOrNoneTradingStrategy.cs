using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class AskAllOrNoneStrategy : AskTradingStrategy
    {
        public AskAllOrNoneStrategy(OrderQueue<Bid> bid, TradeBook tradebook) :base(bid, tradebook)
        {
            
        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
