using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.ViewModel
{
    public class VisualisationViewModel : ViewModelBase
    {
        public TradeSummaryViewModel TradeSummaryVM { get; private set; }
        public TradeChartViewModel TradeChartVM { get; private set; }

        public VisualisationViewModel(TradeSummaryViewModel tradesummaryvm, TradeChartViewModel tradechartvm)
        {
            TradeSummaryVM = tradesummaryvm;
            TradeChartVM = tradechartvm;
        }
    }
}
