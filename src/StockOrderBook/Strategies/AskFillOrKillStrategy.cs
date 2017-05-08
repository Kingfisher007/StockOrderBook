using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class AskFillOrKillStrategy : AskTradingStrategy
    {
        public AskFillOrKillStrategy(OrderQueue<Bid> bid) : base(bid)
        {

        }

        public override TradeExecutionResult<Ask> Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
