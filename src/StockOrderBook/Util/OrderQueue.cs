using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Util
{
    class OrderQueue<T> where T : Order
    {
        SortedSet<T> Queue;

        public string Ticker { get; protected set; }
        public T Top { get { return Queue.Max; } }
        public IComparer<T> Comparer { get; protected set; }
        public SortedSet<T> Orders { get { return Queue; } }
        public event TopOrderChanged TopOrderChanged;

        public OrderQueue(string ticker, IComparer<T> comparer)
        {
            Ticker = ticker;
            Comparer = comparer;
            Queue = new SortedSet<T>(comparer);
        }

        public bool Add(T order)
        {
            if (order.Ticker.Equals(Ticker))
            {
                throw new Exception("Invalid order.");
            }

            if (Queue.Add(order))
            {
                // Reference comparison.
                // make sure order is same/unique
                if (Queue.Max == order)
                {
                    TopOrderChangedEventArgs Args = new TopOrderChangedEventArgs(ChangeReason.Added);
                    RaiseOrderChange(Args);
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
            if(Queue.Max == order)
            {
                isTop = true;
            }

            if(Queue.Remove(order) && isTop)
            {
                TopOrderChangedEventArgs Args = new TopOrderChangedEventArgs(ChangeReason.Removed);
                RaiseOrderChange(Args);
                return true;
            }

            return false;
        }

        private void RaiseOrderChange(TopOrderChangedEventArgs args)
        {
            if(TopOrderChanged != null)
            {
                TopOrderChanged(this, args);
            }
        }
    }
}
