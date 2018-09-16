using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Strategies
{
    class AskAONStrategy : AskTradingStrategy
    {
        public AskAONStrategy(IOrderBook orderbook, IFillBook tradebook) :base(orderbook, tradebook)
        {
            
        }

        protected override TradeExecutionResult ExecuteAsk(Ask order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            TradeStatus result = TradeStatus.NotTraded;

            if (Orderbook.Bids.Count > 0)
            {
                int cumVolume = 0;
                //Bid bid;
                Stack<Bid> matchedOrders = new Stack<Bid>();

                //var enumerator = Orderbook.Bids.Orders;
                //// Accumulate Ask orders to fill bid order 
                //while (enumerator.MoveNext())
                //{
                foreach(Bid bid in Orderbook.Bids.Queue)
                { 
                    if (bid.BidPrice < order.AskPrice)
                    {
                        break;
                    }

                    if(TradeType.AllOrNothing == bid.Trade && bid.Volume > order.Volume)
                    {
                        continue;
                    }

                    cumVolume += bid.Volume;
                    matchedOrders.Push(bid);

                    if (cumVolume == order.Volume)
                    {
                        break;
                    }

                    if (cumVolume > order.Volume)
                    {
                        // if cum volume exceeds bid volume, try to divide last ask order to match volumes
                        if (bid.Trade == TradeType.None)
                        {
                            matchedOrders.Pop();

                            bid.TradedVolume = order.Volume - (cumVolume - bid.Volume);
                            matchedOrders.Push(bid);
                            cumVolume += bid.TradedVolume;
                            break;
                        }
                        // trade not possible
                        else
                        {
                            break;
                        }
                    }
                }
                // If we got exact volumes to trade for bid order then execute the trade
                if (cumVolume == order.Volume)
                {
                    // trades
                    AddTrades(CreateTrades(order, matchedOrders));
                    Orderbook.Remove(matchedOrders.ToList());
                    result = TradeStatus.Traded;
                }
            }
            return new TradeExecutionResult(result, order.AskPrice, order.Volume);
        }
    }
}
