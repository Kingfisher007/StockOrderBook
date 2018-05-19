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
        FillBook fillbook;
        IDictionary<TradeType, AskTradingStrategy> AskStrategies;
        IDictionary<TradeType, BidTradingStrategy> BidStrategies;

        public TradingStrategyProvider(FillBook fillbook)
        {
            this.fillbook = fillbook;
            AskStrategies = new Dictionary<TradeType, AskTradingStrategy>();
            BidStrategies = new Dictionary<TradeType, BidTradingStrategy>();
        }

        public void Initialise(OrderQueue<Ask> asks, OrderQueue<Bid> bids)
        {
            // Asks
            AskStrategies.Add(TradeType.AllOrNothing, new AskAONStrategy(asks, bids, fillbook));
            AskStrategies.Add(TradeType.FillOrKill, new AskFOKStrategy(asks, bids, fillbook));
            AskStrategies.Add(TradeType.ImmidiateOrCancel, new AskIOCStrategy(asks, bids, fillbook));
            AskStrategies.Add(TradeType.None, new AskMarketStrategy(asks, bids, fillbook));

            // Bids
            BidStrategies.Add(TradeType.AllOrNothing, new BidAONStrategy(asks, bids, fillbook));
            BidStrategies.Add(TradeType.FillOrKill, new BidFOKStrategy(asks, bids, fillbook));
            BidStrategies.Add(TradeType.ImmidiateOrCancel, new BidIOCStrategy(asks, bids, fillbook));
            BidStrategies.Add(TradeType.None, new BidMarketStrategy(asks, bids, fillbook));
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