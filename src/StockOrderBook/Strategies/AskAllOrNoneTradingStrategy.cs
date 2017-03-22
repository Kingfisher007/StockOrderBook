using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class AskAllOrNoneStrategy : ITradingStrategy<Ask>
    {
        OrderQueue<Bid> Bids;

        public AskAllOrNoneStrategy(OrderQueue<Bid> bids)
        {
            Bids = bids;
        }

        public TradeExecutionResult<Ask> Execute(Ask order)
        {
            throw new NotImplementedException();
        }
    }
}
