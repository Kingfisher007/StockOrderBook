using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
	public class OrderQueue<T> where T : Order
	{
		protected SortedSet<T> Queue;

		public string Ticker { get; protected set; }
		public T Top { get { return Queue.Max; } }
		public IComparer<T> Comparer { get; protected set; }
		public IEnumerable<T> Orders { get { return Queue.AsEnumerable(); } }
		public event TopOrderChanged<T> TopOrderAdded;
		public event TopOrderChanged<T> TopOrderRemoved;

		public OrderQueue(string ticker, IComparer<T> comparer)
		{
			Ticker = ticker;
			Comparer = comparer;
			Queue = new SortedSet<T>(comparer);
		}

		public bool Add(T order)
		{
			if (!order.Ticker.Equals(Ticker))
			{
				throw new Exception("Invalid order.");
			}

			if (Queue.Add(order))
			{
				// make sure order is same/unique
				if (Queue.Max.ID.Equals(order.ID))
				{
					TopOrderChangedEventArgs<T> Args = new TopOrderChangedEventArgs<T>(ChangeReason.Added, order);
					RaiseTopOrderAdded(Args);
				}

				return true;
			}
			return false;
		}

		public bool Remove(T order)
		{
			bool isTop = false;
			// Reference comparison.
			// make sure order is same/unique
			if (Queue.Max == order)
			{
				isTop = true;
			}

			if (Queue.Remove(order) && isTop)
			{
				TopOrderChangedEventArgs<T> Args = new TopOrderChangedEventArgs<T>(ChangeReason.Removed, order);
				RaiseTopOrderRemoved(Args);
				return true;
			}

			return false;
		}

		public void Remove(IEnumerator<T> orders)
		{
            while(orders.MoveNext())
            {
                Queue.Remove(orders.Current);
            }
		}

		private void RaiseTopOrderAdded(TopOrderChangedEventArgs<T> args)
		{
			if (TopOrderAdded != null)
			{
				TopOrderAdded(this, args);
			}
		}

		private void RaiseTopOrderRemoved(TopOrderChangedEventArgs<T> args)
		{
			if (TopOrderRemoved != null)
			{
				TopOrderRemoved(this, args);
			}
		}

	}
}

