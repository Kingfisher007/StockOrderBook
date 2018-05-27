using StockOrderBook.Entities;
using StockOrderBook.Strategies;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockOrderBook
{
    class MatchingEngine
    {
        ITradingStrategyProvider TradingStrategyProvider;
        ManualResetEvent signal;

        public MatchingEngine(ITradingStrategyProvider tradingStrategyProvider)
        {
            TradingStrategyProvider = tradingStrategyProvider;
            signal = new ManualResetEvent(false);
        }

        public void MatchNewAskOrder(OrderBook orderBook, NewOrderEventArgs<Ask> newOrder)
        {
            Ask ask = newOrder.Order;
            TradeExecutionResult result = null;

            ITradingStrategy<Ask> tradingstrategy = TradingStrategyProvider.GetAskStrategy(newOrder.Order.Trade);
            if (tradingstrategy != null)
            {
                result = tradingstrategy.Execute(newOrder.Order);
                if (result.Status != TradeStatus.Traded && (newOrder.Order.Trade == TradeType.AllOrNothing || newOrder.Order.Trade == TradeType.None))
                {
                    // enque order
                    orderBook.Ask(newOrder.Order);
                }
                else
                {
                    MatchBook(orderBook);
                }
            }
        }

        public void MatchNewBidOrder(OrderBook orderBook, NewOrderEventArgs<Bid> newOrder)
        {
            TradeExecutionResult result = null;

            ITradingStrategy<Bid> tradingstrategy = TradingStrategyProvider.GetBidStrategy(newOrder.Order.Trade);
            if (tradingstrategy != null)
            {
                tradingstrategy.Execute(newOrder.Order);
                if (result.Status != TradeStatus.Traded && (newOrder.Order.Trade == TradeType.AllOrNothing || newOrder.Order.Trade == TradeType.None))
                {
                    // enque order
                    orderBook.Bid(newOrder.Order);
                }
                else
                {
                    MatchBook(orderBook);
                }
            }
        }

        private void MatchBook(OrderBook orderBook)
        {
            while (orderBook.AskPrice <= orderBook.BidPrice)
            {

            }
        }
    }
}
