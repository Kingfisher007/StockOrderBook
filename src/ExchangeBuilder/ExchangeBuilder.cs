using StockOrderBook;
using StockOrderBook.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBuilder
{
    public static class ExchangeBuilder
    {
        public static StockExchange Build(string ticker)
        {
            IOrderBook orderbook = new OrderBook(ticker);
            IFillBook fillbook = new FillBook(ticker);
            IOrderExecutionLogbook logbook = new OrderExecutionLogbook();
            ITradingStrategyProvider strategyProvider = new TradingStrategyProvider(orderbook, fillbook);
            OrderManager ordermanager = new OrderManager();
            MatchingEngine matchengine = new MatchingEngine(orderbook, logbook, strategyProvider);
            //
            StockExchange exchange = new StockExchange(ticker, orderbook, fillbook, logbook, strategyProvider, matchengine, ordermanager);
            return exchange;
        }
    }
}
