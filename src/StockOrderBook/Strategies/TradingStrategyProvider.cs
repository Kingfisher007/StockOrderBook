using StockOrderBook.Entities;
using StockOrderBook.Util;
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

        public void Initialise(OrderQueue<Ask> asks, OrderQueue<Bid> bids)
        {
            // Asks
            AskStrategies.Add(TradeType.AllOrNothing, new AskAONStrategy(asks, bids, Tradebook));
            AskStrategies.Add(TradeType.FillOrKill, new AskFOKStrategy(asks, bids, Tradebook));
            AskStrategies.Add(TradeType.ImmidiateOrCancel, new AskIOCStrategy(asks, bids, Tradebook));
            AskStrategies.Add(TradeType.Market, new AskMarketStrategy(asks, bids, Tradebook));

            // Bids
            BidStrategies.Add(TradeType.AllOrNothing, new BidAONStrategy(asks, bids, Tradebook));
            BidStrategies.Add(TradeType.FillOrKill, new BidFOKStrategy(asks, bids, Tradebook));
            BidStrategies.Add(TradeType.ImmidiateOrCancel, new BidIOCStrategy(asks, bids, Tradebook));
            BidStrategies.Add(TradeType.Market, new BidMarketStrategy(asks, bids, Tradebook));
        }

        public AskTradingStrategy GetAskStrategy(TradeType type)
        {
            return AskStrategies[type];
        }

        public BidTradingStrategy GetBidStrategy(TradeType type)
        {
            return BidStrategies[type];
        }
    }
}