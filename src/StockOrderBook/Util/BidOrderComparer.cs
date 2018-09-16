using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
    class BidOrderComparer : IComparer<Bid>
    {
        public int Compare(Bid x, Bid y)
        {
            if(x.ID.Equals(y.ID))
            {
                return 0;
            }
            // 100 is for comparing upto 2 precision.
            // comparison of 10.99 and 10.98 should result proper integer value. 
            if (x.BidPrice != y.BidPrice)
            {
                return (int)( 100 * (y.BidPrice - x.BidPrice));
            }
            // compare time if prices result in a tie.
            return x.Timestamp >= y.Timestamp ? 1 : -1;
        }
    }
}
