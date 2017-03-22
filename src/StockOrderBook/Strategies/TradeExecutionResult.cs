using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public class TradeExecutionResult<T> where T : Order
    {
        public IEnumerable<Trade> Trades { get; protected set; }
        public T Order { get; protected set; }
        public TradeResult Result { get; protected set; }

        public TradeExecutionResult(TradeResult result, T order, IEnumerable<Trade> trades)
        {
            Result = result;
            Order = order;
            Trades = trades;
        }
    }
}
