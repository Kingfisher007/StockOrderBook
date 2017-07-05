using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class BidFillOrKillStrategy : BidTradingStrategy
    {
        public BidFillOrKillStrategy(OrderQueue<Ask> ask, TradeBook tradebook) : base(ask, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}
