using StockOrderBook.Entities;
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
        AskTradingStrategy GetAskStrategy(TradeType type);
        BidTradingStrategy GetBidStrategy(TradeType type);
    }
}