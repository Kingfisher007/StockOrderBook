using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public interface ITradingStrategy 
    {
        TradeExecutionResult Execute(Order order);
    }
}
