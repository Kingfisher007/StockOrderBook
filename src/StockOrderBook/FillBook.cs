using EOrderBook.Entities;
using StockOrderBook.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
	public class FillBook : IFillBook
	{
        ObservableCollection<Trade> trades;
        private string _ticker;

        public FillBook(string ticker)
		{
            _ticker = ticker;
			trades = new ObservableCollection<Trade>();
		}

		public void Add(Trade trade)
		{
			trades.Add(trade);
		}

		public void AddRange(IList<Trade> trade)
		{
			foreach(Trade t in trade)
            {
                trades.Add(t);
            }
		}

        public ObservableCollection<Trade> Trades
        {
            get
            {
                return trades;
            }
        }

        public string Ticker
        {
            get
            {
                return _ticker;
            }
        }
    }
}
