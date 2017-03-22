using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
    public delegate void TopOrderChanged(object sender, TopOrderChangedEventArgs eventArgs);

    public enum ChangeReason
    {
        Added,
        Removed
    }

    public class TopOrderChangedEventArgs:EventArgs
    {
        public ChangeReason Reason { get; protected set; }

        public TopOrderChangedEventArgs(ChangeReason reason)
        {
            Reason = reason;
        }
    }
}
