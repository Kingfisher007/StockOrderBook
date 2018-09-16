using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    public interface IOrderExecutionLogbook
    {
        SafeObservableCollection<OrderExecutionLog> Logs
        {
            get;
        }

        void Log(OrderExecutionLog orderExecutionLog);
    }
}
