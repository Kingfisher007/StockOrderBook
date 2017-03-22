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
        public float Price { get; protected set; }
        public int Volume { get; protected set; }
        public DateTime Time { get; protected set; }

        public Trade(string ticker, float price, int volume)
        {
            Ticker = ticker;
            Price = price;
            Volume = volume;
            Time = DateTime.Now;
        }
    }
}
