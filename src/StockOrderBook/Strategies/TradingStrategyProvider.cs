using EOrderBook.Entities;
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
        IFillBook fillbook;
        IOrderBook orderbook;
        IDictionary<TradeType, AskTradingStrategy> AskStrategies;
        IDictionary<TradeType, BidTradingStrategy> BidStrategies;

        public TradingStrategyProvider(IOrderBook orderbook, IFillBook fillbook)
        {
            this.orderbook = orderbook;
            this.fillbook = fillbook;
            AskStrategies = new Dictionary<TradeType, AskTradingStrategy>();
            BidStrategies = new Dictionary<TradeType, BidTradingStrategy>();
            Initialise();
        }

        private void Initialise()
        {
            // Asks
            AskStrategies.Add(TradeType.AllOrNothing, new AskAONStrategy(orderbook, fillbook));
            AskStrategies.Add(TradeType.FillOrKill, new AskFOKStrategy(orderbook, fillbook));
            AskStrategies.Add(TradeType.ImmidiateOrCancel, new AskIOCStrategy(orderbook, fillbook));
            AskStrategies.Add(TradeType.None, new AskMarketStrategy(orderbook, fillbook));

            // Bids
            BidStrategies.Add(TradeType.AllOrNothing, new BidAONStrategy(orderbook, fillbook));
            BidStrategies.Add(TradeType.FillOrKill, new BidFOKStrategy(orderbook, fillbook));
            BidStrategies.Add(TradeType.ImmidiateOrCancel, new BidIOCStrategy(orderbook, fillbook));
            BidStrategies.Add(TradeType.None, new BidMarketStrategy(orderbook, fillbook));
        }

        protected AskTradingStrategy GetAskStrategy(TradeType type)
        {
            return AskStrategies[type];
        }

        protected BidTradingStrategy GetBidStrategy(TradeType type)
        {
            return BidStrategies[type];
        }
        
        public ITradingStrategy GetTradingStrategy(OrderType orderType, TradeType type)
        {
            switch (orderType)
            {
                case OrderType.Ask:
                    return GetAskStrategy(type);
                case OrderType.Bid:
                    return GetBidStrategy(type);
                default:
                    return null;
            } 
        }
    }
}