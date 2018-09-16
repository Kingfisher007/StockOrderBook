using StockOrderBook;
using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.ViewModel
{
    public class OrderBookViewModel : ViewModelBase
    {
        IOrderBook orderbook;
        SafeObservableCollection<Ask> asks;
        SafeObservableCollection<Bid> bids;
        Action<IReadOnlyList<Ask>> AskAction;
        Action<IReadOnlyList<Bid>> BidAction;

        public OrderBookViewModel(IOrderBook orderBook)
        {
            orderbook = orderBook;
            asks = new SafeObservableCollection<Ask>();
            bids = new SafeObservableCollection<Bid>();
            orderBook.Asks.CollectionChanged += Asks_CollectionChanged;
            orderBook.Bids.CollectionChanged += Bids_CollectionChanged;
        }

        private void Bids_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList items = null;
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Bid a in e.NewItems)
                {
                    bids.Add(a);
                }

                //bids.AddRange(e.NewItems.Cast<Bid>().ToList());
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Bid a in e.OldItems)
                {
                    bids.Remove(a);
                }
                //bids.RemoveRange(e.OldItems.Cast<Bid>().ToList());
            }
            else if(e.Action == NotifyCollectionChangedAction.Replace)
            {
                
            }
                
            RaisePropertychanged("Spread");
        }

        private void Asks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList items = null;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(Ask a in e.NewItems)
                {
                    asks.Add(a);
                }

                //asks.AddRange(e.NewItems.Cast<Ask>().ToList());
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Ask a in e.OldItems)
                {
                    asks.Remove(a);
                }
                //asks.RemoveRange(e.OldItems.Cast<Ask>().ToList());
            }

            RaisePropertychanged("Spread");
        }

        public SafeObservableCollection<Ask> Asks
        {
            get
            {
                return asks;
            }
        }

        public SafeObservableCollection<Bid> Bids
        {
            get
            {
                return bids;
            }
        }

        public float Spread
        {
            get
            {
                return orderbook.AskPrice - orderbook.BidPrice;
            }
        }
    }
}
