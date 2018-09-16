using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOrderBook.Entities;
using StockOrderBook;

namespace EOrderBook.ViewModel
{
    public class PriceChartViewModel 
    {
        public PriceChartViewModel(IFillBook fillbook, double topPadding)
        {
            
        }

        protected bool IsValueIncreased(Trade trade)
        {
            return true;
        }
    }
}
