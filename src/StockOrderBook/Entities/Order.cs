using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class Order
    {
        protected Guid ID { get;  }
        public string Ticker { get; }
        public TradeType Trade { get; }
        public OrderType Type { get; }
        public int Volume { get; }
        public DateTime Time { get; }

        protected Order(string ticker, TradeType trade, OrderType type, int volume)
        {
            ID = Guid.NewGuid();
            Ticker = ticker;
            Trade = trade;
            this.Type = type;
            Volume = volume;
            Time = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            Order orderToCompare = obj as Order;
            if(orderToCompare == null)
            {
                return false;
            }

            return orderToCompare.Ticker.Equals(this.Ticker)
                && Trade.Equals(orderToCompare.Trade)
                && this.Type.Equals(orderToCompare.Type)
                && orderToCompare.Volume.Equals(this.Volume)
                && orderToCompare.Time.Equals(this.Time);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
