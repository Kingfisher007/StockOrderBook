using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public OrderBookViewModel OrderBookVM { get; private set; }
        public FillBookViewModel FillBookVM { get; private set; }
        public LogBookViewModel LogBookVM { get; private set; }
        public NewOrderViewModel NewOrderVM { get; private set; }
        public SimulationViewModel SimulationVM { get; private set; }

        public MainViewModel(OrderBookViewModel orderbookvm, FillBookViewModel fillbookvm, LogBookViewModel logbookvm, NewOrderViewModel newordervm, SimulationViewModel simulationvm)
        {
            OrderBookVM = orderbookvm;
            FillBookVM = fillbookvm;
            LogBookVM = logbookvm;
            NewOrderVM = newordervm;
            SimulationVM = simulationvm;
        }
    }
}
