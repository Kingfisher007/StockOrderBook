using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public interface ITradingStrategyProvider
    {
        ITradingStrategy GetTradingStrategy(OrderType orderType, TradeType type);
    }
}