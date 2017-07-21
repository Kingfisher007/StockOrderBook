using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public interface ITradingStrategyProvider
    {
        void Initialise(OrderQueue<Ask> asks, OrderQueue<Bid> bids);
        AskTradingStrategy GetAskStrategy(TradeType type);
        BidTradingStrategy GetBidStrategy(TradeType type);
    }
}