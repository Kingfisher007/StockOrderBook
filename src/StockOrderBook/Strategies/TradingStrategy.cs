using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public interface ITradingStrategy<T> where T : Order
    {
        TradeExecutionResult<T> Execute(T order);
    }
}
