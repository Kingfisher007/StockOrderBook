using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.Entities
{
    public class Bid : Order
    {
        public float BidPrice { get; }

        public Bid(string ticker, TradeType type, int volume, PriceType otype, float bidPrice, TimeInForce goodTill) 
                : base(ticker, volume, type, otype, goodTill)
        {
            BidPrice = bidPrice;
            this.Type = OrderType.Bid;
        }

        public override float Price { get => BidPrice; }

        public override bool Equals(object obj)
        {
            Bid bidToCompare = obj as Bid;

            if (bidToCompare == null)
            {
                return false;
            }

            return base.Equals(bidToCompare);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
