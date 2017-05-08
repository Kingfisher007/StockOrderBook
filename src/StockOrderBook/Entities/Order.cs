using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class Order
    {
        protected Guid ID { get; private set; }
        public string Ticker { get; protected set; }
        public TradeType Trade { get; protected set; }
        public PriceType Type { get; protected set; }
        public int Volume { get; protected set; }
        public TimeInForce GoodTill { get; protected set; }
        public DateTime Time { get; protected set; }

        protected Order(string ticker, int volume, TradeType trade, PriceType type, TimeInForce goodTill)
        {
            ID = Guid.NewGuid();
            Ticker = ticker;
            Trade = trade;
            Type = type;
            Volume = volume;
            GoodTill = goodTill;
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
                && orderToCompare.Trade.Equals(this.Trade)
                && orderToCompare.Type.Equals(this.Type)
                && orderToCompare.Volume.Equals(this.Volume)
                && orderToCompare.GoodTill.Equals(this.GoodTill)
                && orderToCompare.Time.Equals(this.Time);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
