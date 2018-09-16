using EOrderBook.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
	public class OrderQueue<T> : INotifyCollectionChanged  where T : Order
	{
		protected SortedSet<T> queue;

		public string Ticker { get; protected set; }
		public T Top { get { return queue.Max; } }
		public IComparer<T> Comparer { get; protected set; }
        public IEnumerator<T> Orders { get { return queue.GetEnumerator(); } }
        public event TopOrderChanged<T> TopOrderAdded;
		public event TopOrderChanged<T> TopOrderRemoved;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public OrderQueue(string ticker, IComparer<T> comparer)
		{
			Ticker = ticker;
			Comparer = comparer;
			queue = new SortedSet<T>(comparer);
		}

        public SortedSet<T> Queue
        {
            get
            {
                return queue;
            }
        }

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        protected void RaiseCollectionChanged(NotifyCollectionChangedAction action, IList orders)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs( action, orders);
            CollectionChanged?.Invoke(this, args);
        }

		public void Add(T order)
		{
			if (!order.Ticker.Equals(Ticker))
			{
				throw new Exception("Invalid order.");
			}

			if (queue.Add(order))
			{
				// make sure order is same/unique
				if (queue.Max.ID.Equals(order.ID))
				{
					RaiseTopOrderAdded(order);
				}
                IList list = new ArrayList();
                list.Add(order);
                RaiseCollectionChanged(NotifyCollectionChangedAction.Add, list);
			}

		}

		public void Remove(T order)
		{
			bool isTop = false;
			// Reference comparison.
			// make sure order is same/unique
			if (queue.Max == order)
			{
				isTop = true;
			}

			if (queue.Remove(order) && isTop)
			{
				RaiseTopOrderRemoved(order);
                IList list = new ArrayList();
                list.Add(order);
                RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, list);
			}
		}

		public void Remove(IList<T> orders)
		{
            foreach(T order in orders)
            {
                queue.Remove(order);
            }
            IList list = new ArrayList(orders.ToArray());
            RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, list);
        }

		private void RaiseTopOrderAdded(T order)
		{
			if (TopOrderAdded != null)
			{
                TopOrderChangedEventArgs<T> args = new TopOrderChangedEventArgs<T>(ChangeReason.Added, order);
                TopOrderAdded(this, args);
			}
		}

		private void RaiseTopOrderRemoved(T order)
		{
			if (TopOrderRemoved != null)
			{
                TopOrderChangedEventArgs<T> args = new TopOrderChangedEventArgs<T>(ChangeReason.Removed, order);
                TopOrderRemoved(this, args);
			}
		}
	}
}

