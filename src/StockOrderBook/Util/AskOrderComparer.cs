using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
    class AskOrderComparer : IComparer<Ask>
    {
        public int Compare(Ask x, Ask y)
        {
            // 100 is for comparing upto 2 precision.
            // comparison of 10.99 and 10.98 should result proper integer value. 
            if(x.AskPrice != y.AskPrice)
            {
                return (int)(100 * (y.AskPrice - x.AskPrice));
            }
            // compare time if prices result in a tie.
            return y.Timestamp <= x.Timestamp ? 1 : -1;
        }
    }
}
