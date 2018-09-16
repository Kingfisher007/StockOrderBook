using StockOrderBook;
using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Threading;
using StockOrderBook.Util;
using EOrderBook.Entities;

namespace EOrderBook.ViewModel
{
    public class LogBookViewModel
    {
        SafeObservableCollection<OrderExecutionLog> logs;

        public LogBookViewModel(IOrderExecutionLogbook logBook)
        {
            logs = logBook.Logs;
        }

        public ObservableCollection<OrderExecutionLog> Logs
        {
            get
            {
                return logs;
            }
        }
    }
}
