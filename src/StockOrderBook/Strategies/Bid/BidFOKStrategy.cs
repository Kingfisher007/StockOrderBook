using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOrderBook.Entities;
using StockOrderBook.Util;

namespace StockOrderBook.Strategies
{
    class BidFOKStrategy : BidTradingStrategy
    {
        public BidFOKStrategy(IOrderBook orderbook, IFillBook tradebook) : base(orderbook, tradebook)
        {

        }

        protected override TradeExecutionResult ExecuteBid(Bid order)
        {
            throw new NotImplementedException();
        }
    }
}
