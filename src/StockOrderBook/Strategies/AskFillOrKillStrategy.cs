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
        public AskFillOrKillStrategy(OrderQueue<Bid> bid, TradeBook tradebook) : base(bid, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
