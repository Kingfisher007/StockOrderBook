using EOrderBook.Entities;
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
    public class MatchingEngine
    {
        ITradingStrategyProvider TradingStrategyProvider;
        IOrderBook OrderBook;
        IOrderExecutionLogbook OrderExecutionLogbook;
        Queue<Order> NewOrderQueue;
        AutoResetEvent signal;
        CancellationTokenSource tokensource;
        bool IsRunning;

        public MatchingEngine(IOrderBook orderBook, IOrderExecutionLogbook orderExecutionLogbook, ITradingStrategyProvider tradingStrategyProvider)
        {
            OrderBook = orderBook;
            OrderExecutionLogbook = orderExecutionLogbook;
            TradingStrategyProvider = tradingStrategyProvider;
            NewOrderQueue = new Queue<Order>();
            signal = new AutoResetEvent(false);
            tokensource = new CancellationTokenSource();
            IsRunning = false;
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

        public bool Start()
        {
            if(!IsRunning)
            {
                try
                {
                    Task.Factory.StartNew(() => { this.MatchOrders(tokensource.Token); });
                    IsRunning = true;
                }
                catch(Exception expn)
                {
                    //
                }
            }
            return IsRunning;
        }

        private void MatchOrders(CancellationToken token)
        {
            TradeExecutionResult result = null;
            Order ord = null;
            ITradingStrategy tradingStrategy = null;
            bool IsNewOrder = false;
            
            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        // put all trades in order book and exit
                    }
                    //
                    OrderExecutionLog execLog = new OrderExecutionLog();
                    execLog.State = OrderState.Cancelled;
                    //
                    ord = null;
                    // Get order to trade
                    if (NewOrderQueue.Count > 0)
                    {
                        ord = NewOrderQueue.Dequeue();
                        IsNewOrder = true;
                    }
                    else if (OrderBook.Asks.Count > 0 && OrderBook.Bids.Count > 0 && OrderBook.TopAsk.AskPrice <= OrderBook.TopBid.BidPrice)
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
                    // Execute trade for selected order
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
                                OrderBook.Add(ord as Ask);
                            }
                            else
                            {
                                OrderBook.Add(ord as Bid);
                            }
                        }

                        //
                        if (!IsNewOrder && status == TradeStatus.Traded)
                        {
                            // remove order form orderbook queue
                            if (ord.Type == OrderType.Ask)
                            {
                                OrderBook.Remove(ord as Ask);
                            }
                            else
                            {
                                OrderBook.Remove(ord as Bid);
                            }
                        }

                        //
                        execLog.OrderID = ord.ID;
                        execLog.Trade = ord.Trade;
                        execLog.Type = ord.Type;
                        execLog.Volume = ord.Volume;
                        execLog.Price = ord.Price;
                        execLog.Time = ord.Timestamp;
                        execLog.State = GetOrderState(status);
                        OrderExecutionLogbook.Log(execLog);

                    }
                    
                    // 
                    if(ord is null || (ord != null && result?.Status == TradeStatus.NotTraded))
                    {
                        // reset
                        ord = null;
                        result = null;
                        // wait for new order signal
                        signal.WaitOne();
                    }
                }
                catch (Exception expn)
                {
                    // log
                }
            }
        }

        private OrderState GetOrderState(TradeStatus status)
        {
            switch (status)
            {
                case TradeStatus.NotTraded:
                    return OrderState.Cancelled;
                case TradeStatus.PartiallyTraded:
                    return OrderState.PratiallyTradedAndQueued;
                case TradeStatus.Traded:
                    return OrderState.Traded;
                default:
                    return OrderState.Queued;
            }
        }
    }
}
