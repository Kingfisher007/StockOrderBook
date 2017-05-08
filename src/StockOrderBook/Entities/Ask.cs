using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class Ask : Order
    {
        public float AskPrice {get; }

        public Ask(string ticker, int volume, TradeType type, PriceType otype, float askPrice, TimeInForce goodTill) 
                : base (ticker, volume, type, otype, goodTill)
        {
            AskPrice = askPrice;
        }

        public override bool Equals(object obj)
        {
            Ask askToCompare = obj as Ask;

            if (askToCompare == null)
            {
                return false;
            }

            return base.Equals(askToCompare)
                    && askToCompare.AskPrice.Equals(this.AskPrice);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
