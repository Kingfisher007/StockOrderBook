using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Entities;

namespace StockOrderBook.Util
{
	public delegate void TopOrderChanged<T>(OrderQueue<T> sender, TopOrderChangedEventArgs<T> eventArgs) where T : Order;

    public enum ChangeReason
    {
        Added,
        Removed
    }

    public class TopOrderChangedEventArgs<T>:EventArgs
    {
        public ChangeReason Reason { get; protected set; }
		public T Order { get; protected set; }

        public TopOrderChangedEventArgs(ChangeReason reason, T order)
        {
            Reason = reason;
			Order = order;
        }
    }
}
