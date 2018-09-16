using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook;
using EOrderBook.Entities;

namespace EOrderBook.ViewModel
{
    public class VolumeChartViewModel
    {
        public VolumeChartViewModel(IFillBook fillBook, double topPadding)
        {
            
        }

        protected bool IsValueIncreased(Trade trade)
        {
            return true;
        }
    }
}
