using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class BidFOKStrategy : BidTradingStrategy
    {
        public BidFOKStrategy(OrderQueue<Ask> asks, OrderQueue<Bid> bids, TradeBook tradebook) : base(ask, bids, tradebook)
        {

        }

        public override TradeExecutionResult Execute(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}
