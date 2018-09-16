using EOrderBook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBuilder
{
    public class ViewModelProvider
    {
        StockExchange stockExchange;
        //
        NewOrderViewModel newOrderVM;
        SimulationViewModel simulationVM;
        OrderBookViewModel orderBookVM;
        FillBookViewModel fillBookVM;
        LogBookViewModel logbookVM;
        TradeChartViewModel tradeChartVM;
        VolumeChartViewModel volumeChartVM;
        TradeSummaryViewModel tradeSummaryVM;
        MainViewModel mainVM;
        VisualisationViewModel visualVM;

        public ViewModelProvider(StockExchange stockexchange)
        {
            stockExchange = stockexchange;
            BuildViewModels();
        }

        protected void BuildViewModels()
        {
            newOrderVM = new NewOrderViewModel(stockExchange.Ticker, stockExchange.OrderManager);
            simulationVM = new SimulationViewModel(stockExchange.Ticker, stockExchange.OrderManager);
            orderBookVM = new OrderBookViewModel(stockExchange.OrderBook);
            fillBookVM = new FillBookViewModel(stockExchange.FillBook);
            logbookVM = new LogBookViewModel(stockExchange.LogBook);
            tradeChartVM = new TradeChartViewModel(stockExchange.FillBook);
            tradeSummaryVM = new TradeSummaryViewModel(stockExchange.FillBook);

            mainVM = new MainViewModel(orderBookVM, fillBookVM, logbookVM, newOrderVM, simulationVM);
            visualVM = new VisualisationViewModel(tradeSummaryVM, tradeChartVM);
        }

        public MainViewModel GetMainViewModel()
        {
            return mainVM;
        }

        public VisualisationViewModel GetVisualisationViewModel()
        {
            return visualVM;
        }
    }
}
