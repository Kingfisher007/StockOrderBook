using StockOrderBook;
using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EOrderBook.ViewModel
{
    public class FillBookViewModel : ViewModelBase
    {
        ObservableCollection<Trade> trades;

        public FillBookViewModel(IFillBook fillbook)
        {
            trades = new ObservableCollection<Trade>();
            fillbook.Trades.CollectionChanged += Trades_CollectionChanged;
        }

        private void Trades_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => { 
                                                            if(e.NewItems?.Count > 0)
                                                            {
                                                                foreach (Trade t in e.NewItems)
                                                                {
                                                                    trades.Add(t);
                                                                }
                                                            }
                                                        }, DispatcherPriority.DataBind);

            RaisePropertychanged("Volume");
        }

        public ObservableCollection<Trade> Trades
        {
            get
            {
                return trades;
            }
        }

        public int Volume
        {
            get
            {
                return Trades.Sum(t => t.Volume);
            }
        }
    }
}
