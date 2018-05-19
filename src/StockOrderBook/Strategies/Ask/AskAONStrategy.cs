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
        public AskAONStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, FillBook tradebook) :base(asks, bids, tradebook)
        {
            
        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
