using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class AskFOKStrategy : AskTradingStrategy
    {
        public AskFOKStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, FillBook tradebook) : base(asks, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
