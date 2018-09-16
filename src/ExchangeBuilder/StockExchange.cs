using StockOrderBook;
using StockOrderBook.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBuilder
{
    public class StockExchange
    {
        String _ticker;
        IFillBook fillbook;
        IOrderBook orderbook;
        IOrderExecutionLogbook logbook;
        ITradingStrategyProvider strategyProvider;
        MatchingEngine matchengine;
        OrderManager ordermanager;

        public StockExchange(string ticker, IOrderBook orderBook, 
            IFillBook fillBook, 
            IOrderExecutionLogbook logBook, 
            ITradingStrategyProvider tradeStrategyProvider,
            MatchingEngine matchingEngine,
            OrderManager orderManager)
        {
            _ticker = ticker;
            fillbook = fillBook;
            orderbook = orderBook;
            logbook = logBook;
            strategyProvider = tradeStrategyProvider;
            matchengine = matchingEngine;
            ordermanager = orderManager;
            //
            ordermanager.NewOrderReceived += matchengine.OrderBook_NewOrderReceived;
        }

        public string Ticker
        {
            get
            {
                return _ticker;
            }
        }

        public IFillBook FillBook
        {
            get
            {
                return fillbook;
            }
        }

        public IOrderBook OrderBook
        {
            get
            {
                return orderbook;
            }
        }

        public IOrderExecutionLogbook LogBook
        {
            get
            {
                return logbook;
            }
        }

        public IOrderManager OrderManager
        {
            get
            {
                return ordermanager;
            }
        }

        public void Start()
        {
            matchengine.Start();
        }

        public void Stop()
        {
            matchengine.Stop();
        }
    }
}
