using StockOrderBook;
using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrderBook.Util;
using EOrderBook.ViewModel.Entities;

namespace EOrderBook.ViewModel
{
    public class TradeChartViewModel : ViewModelBase
    {
        Trade lastTrade;
        IFillBook fillbook;
        SafeObservableCollection<PriceTimeValue> pricevalues;
        SafeObservableCollection<VolumeTimeValue> volumevalues;

        public TradeChartViewModel(IFillBook fillBook)
        {
            fillbook = fillBook;
            pricevalues =  new SafeObservableCollection<PriceTimeValue>();
            volumevalues = new SafeObservableCollection<VolumeTimeValue>();

            fillbook.Trades.CollectionChanged += Trades_CollectionChanged;
        }

        public ObservableCollection<PriceTimeValue> PriceValues
        {
            get
            {
                return pricevalues;
            }
        }

        public ObservableCollection<VolumeTimeValue> VolumeValues
        {
            get
            {
                return volumevalues;
            }
        }

        public string Ticker
        {
            get
            {
                return fillbook.Ticker;
            }
        }

        public float LastTradePrice
        {
            get
            {
                return lastTrade != null? lastTrade.Price : 0.00F;
            }
        }

        private void Trades_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in e.NewItems)
            {
                lastTrade = item as Trade;
                pricevalues.Add(new PriceTimeValue { Price = lastTrade.Price, Time = lastTrade.Time });
                volumevalues.Add(new VolumeTimeValue { Volume = lastTrade.Volume, Time = lastTrade.Time });
                RaisePropertychanged("LastTradePrice");
            }
        }
    }
}
