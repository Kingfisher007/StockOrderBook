using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    public class TradingStrategyProvider : ITradingStrategyProvider
    {
        TradeBook Tradebook;
        IDictionary<TradeType, AskTradingStrategy> AskStrategies;
        IDictionary<TradeType, BidTradingStrategy> BidStrategies;

        public TradingStrategyProvider(TradeBook tradebook)
        {
            Tradebook = tradebook;
            AskStrategies = new Dictionary<TradeType, AskTradingStrategy>();
            BidStrategies = new Dictionary<TradeType, BidTradingStrategy>();
        }

        void Initialise(OrderQueue<Ask> asks, OrderQueue<Bid> bids)
        {
            // Asks
            AskStrategies.Add();

            // Bids
            BidStrategies.Add();
        }

        AskTradingStrategy GetAskStrategy(TradeType type)
        {
            return AskStrategies[type];
        }

        BidTradingStrategy GetBidStrategy(TradeType type)
        {
            return BidStrategies[type];
        }
    }
}