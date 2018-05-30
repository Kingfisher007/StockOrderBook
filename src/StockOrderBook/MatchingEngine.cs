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
        IOrderBook OrderBook;
        Queue<Order> NewOrderQueue;
        AutoResetEvent signal;
        CancellationTokenSource tokensource;

        public MatchingEngine(IOrderBook orderBook, ITradingStrategyProvider tradingStrategyProvider)
        {
            OrderBook = orderBook;
            TradingStrategyProvider = tradingStrategyProvider;
            NewOrderQueue = new Queue<Order>();
            signal = new AutoResetEvent(false);
            tokensource = new CancellationTokenSource();
        }

        public void OrderBook_NewOrderReceived(OrderManager orderBook, NewOrderEventArgs eventArgs)
        {
            NewOrderQueue.Enqueue(eventArgs.Order);
            if(signal != null)
            {
                // signal order matching thread
                signal.Set();
            }
        }

        public void Stop()
        {
            tokensource.Cancel();
        }

        private void MatchOrders(IOrderBook orderBook, CancellationToken token)
        {
            TradeExecutionResult result = null;
            Order ord = null;
            ITradingStrategy tradingStrategy = null;
            bool IsNewOrder = false;

            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        // put all trades in order book and exit
                    }

                    //
                    ord = null;
                    //
                    if (NewOrderQueue.Count > 0)
                    {
                        ord = NewOrderQueue.Dequeue();
                        IsNewOrder = true;

                    }
                    else if (OrderBook.TopAsk.AskPrice <= OrderBook.TopBid.BidPrice)
                    {
                        if (OrderBook.TopAsk.Volume <= OrderBook.TopBid.Volume)
                        {
                            ord = OrderBook.TopBid;
                        }
                        else
                        {
                            ord = OrderBook.TopAsk;
                        }

                        IsNewOrder = false;
                    }

                    if (ord != null)
                    {
                        // Get strategy
                        tradingStrategy = TradingStrategyProvider.GetTradingStrategy(ord.Type, ord.Trade);
                        // execute
                        result = tradingStrategy.Execute(ord);
                        //
                        TradeStatus status = result.Status;
                        if (IsNewOrder
                            &&
                            (
                               (status == TradeStatus.NotTraded && ord.Trade == TradeType.AllOrNothing)
                                ||
                               ((status == TradeStatus.PartiallyTraded || status == TradeStatus.NotTraded) && ord.Trade == TradeType.None)
                            )
                           )
                        {
                            // Add in orderbook queue
                            if (ord.Type == OrderType.Ask)
                            {
                                orderBook.Add(ord as Ask);
                            }
                            else
                            {
                                orderBook.Add(ord as Bid);
                            }
                        }

                        //
                        if (!IsNewOrder && status == TradeStatus.Traded)
                        {
                            // remove order form orderbook queue
                            if (ord.Type == OrderType.Ask)
                            {
                                orderBook.Remove(ord as Ask);
                            }
                            else
                            {
                                orderBook.Remove(ord as Bid);
                            }
                        }
                    }
                    else
                    {
                        // wait for new order signal
                        signal.WaitOne();
                    }
                }
            }
            catch(Exception expn)
            {
                // log
            }
        }
    }
}
