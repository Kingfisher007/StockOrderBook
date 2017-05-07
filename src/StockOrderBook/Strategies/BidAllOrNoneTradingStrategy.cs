﻿using StockOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class BidAllOrNoneTradingStrategy : BidTradingStrategy
    {

        public BidAllOrNoneTradingStrategy(OrderQueue<Ask> asks) : base(asks)
        {
            
        }

        public override TradeExecutionResult<Bid> Execute(Bid order)
        {
            if(order == null)
            {
                throw new ArgumentNullException("order");
            }

            if(Asks == null 
               || (Asks != null && Asks.Orders == null)
              )
            {
                throw new ApplicationException("No orders to trade");
            }

            List<Trade> trades = new List<Trade>();
            int cumVolume = 0;
            Ask ask;
            TradeResult result = TradeResult.NotTraded;
            Stack<Ask> matchedOrders = new Stack<Ask>();

            var enumerator = Asks.Orders.GetEnumerator();

            while(enumerator.MoveNext())
            {
                ask = enumerator.Current;
                cumVolume += ask.Volume;
                matchedOrders.Push(ask);

                if(cumVolume > order.Volume)
                {
                    if (ask.Trade == TradeType.AllOrNothing  )
                    {
                        break;
                    }

                    Ask newOrder = new Ask(ask.Ticker, cumVolume - order.Volume, ask.Trade, ask.Type, ask.AskPrice, ask.GoodTill );
                    ask = new Ask(ask.Ticker, ask.Volume - newOrder.Volume, ask.Trade,  ask.Type, ask.AskPrice, ask.GoodTill);
                    matchedOrders.Push(ask);
                    // trade
                    CreateTrades(trades, matchedOrders);
                    break;
                }

                if(cumVolume == order.Volume)
                {
                    // trades
                    CreateTrades(trades, matchedOrders);
                    break;
                }
            }

            return new TradeExecutionResult<Bid>(result, order, trades);
        }

        private void CreateTrades(List<Trade> trades, IEnumerable<Ask> orders)
        {

        }
    }
}