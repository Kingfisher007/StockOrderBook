using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.Entities
{
    public class Order
    {
        public Guid ID { get; private set; }
        public string Ticker { get; protected set; }
        public OrderType Type { get; protected set; }
        public TradeType Trade { get; protected set; }
        public PriceType PriceType { get; protected set; }
        public int Volume { get; protected set; }
		public int TradedVolume { get; set; }
        public TimeInForce Validity { get; protected set; }
        public DateTime Timestamp { get; protected set; }

        protected Order(string ticker, int volume, TradeType trade, PriceType type, TimeInForce goodTill)
        {
            ID = Guid.NewGuid();
            Ticker = ticker;
            Trade = trade;
            PriceType = type;
            Volume = volume;
			TradedVolume = volume;
            Validity = goodTill;
            Timestamp = DateTime.Now;
        }

        public virtual float Price
        {
            get;
        }

        public override bool Equals(object obj)
        {
            Order orderToCompare = obj as Order;
            if(orderToCompare == null)
            {
                return false;
            }

            return orderToCompare.Ticker.Equals(this.Ticker)
                && orderToCompare.ID.Equals(this.ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
