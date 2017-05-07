using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class Bid : Order
    {
        public float BidPrice { get; }

        public Bid(string ticker, TradeType type, int volume, OrderType otype, float bidPrice, Validity goodTill) 
                : base(ticker, volume, type, otype, goodTill)
        {
            BidPrice = bidPrice;
        }

        public override bool Equals(object obj)
        {
            Bid bidToCompare = obj as Bid;

            if (bidToCompare == null)
            {
                return false;
            }

            return bidToCompare.GetType().Equals(this.GetType())
                    && base.Equals(bidToCompare)
                    && bidToCompare.BidPrice.Equals(this.BidPrice);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
