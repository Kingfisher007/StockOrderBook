using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class TradeExecutionResult
    {
		public float Price { get; protected set; }
		public int Volume { get; protected set; }
        public TradeStatus Status { get; protected set; }

        public TradeExecutionResult(TradeStatus status, float price, int volume)
        {
            Status = status;
			Price = price;
			Volume = volume;
        }
    }
}
