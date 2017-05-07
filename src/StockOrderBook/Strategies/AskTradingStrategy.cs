using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public abstract class AskTradingStrategy : ITradingStrategy<Ask>
    {
        OrderQueue<Bid> Bids;

        protected AskTradingStrategy(OrderQueue<Bid> bids)
        {
            Bids = bids;
        }

        public abstract TradeExecutionResult<Ask> Execute(Ask order);
    }
}
