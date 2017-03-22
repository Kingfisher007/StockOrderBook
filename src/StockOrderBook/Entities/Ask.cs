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

        public Ask(string ticker, TradeType type, OrderType otype, float askPrice, int volume) : base (ticker, type, otype, volume)
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
