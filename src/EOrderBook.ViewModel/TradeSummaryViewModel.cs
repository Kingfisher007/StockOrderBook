using StockOrderBook;
using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.ViewModel
{
    public class TradeSummaryViewModel : ViewModelBase
    {
        IFillBook FillBook;

        public TradeSummaryViewModel(IFillBook fillbook)
        {
            FillBook = fillbook;
            FillBook.Trades.CollectionChanged += Trades_CollectionChanged;
        }

        private void Trades_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertychanged("LastTrade");
        }

        public string Ticker
        {
            get
            {
                return FillBook.Ticker;
            }
        }

        public Trade LastTrade
        {
            get
            {
                return FillBook.Trades.Last();
            }
        }
    }
}
