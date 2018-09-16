using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StockOrderBook
{
    public class OrderExecutionLogbook : IOrderExecutionLogbook
    {
        SafeObservableCollection<OrderExecutionLog> logs;

        public OrderExecutionLogbook()
        {
            logs = new SafeObservableCollection<OrderExecutionLog>();  
        }

        public SafeObservableCollection<OrderExecutionLog> Logs
        {
            get
            {
                return logs; 
            }
        }

        public void Log(OrderExecutionLog orderExecutionLog)
        {
            logs.Add(orderExecutionLog);
        }
    }
}
