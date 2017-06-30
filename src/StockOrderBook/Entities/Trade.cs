using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class Trade
    {
        public string Ticker { get; protected set; }
		public Guid BidOrderID { get; private set; }
		public Guid AskOrderID { get; private set; }
        public float Price { get; protected set; }
        public int Volume { get; protected set; }
        public DateTime Time { get; protected set; }

        public Trade(string ticker, Guid bidOrderID, Guid askOrderID, float price, int volume)
        {
            Ticker = ticker;
			BidOrderID = bidOrderID;
			AskOrderID = askOrderID;
            Price = price;
            Volume = volume;
            Time = DateTime.Now;
        }
    }
}
