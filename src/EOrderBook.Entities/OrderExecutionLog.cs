using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.Entities
{
    public class OrderExecutionLog
    {
        public Guid OrderID
        {
            get;
            set;
        }

        public OrderState State
        {
            get;
            set;
        }

        public OrderType Type
        {
            get;
            set;
        }

        public TradeType Trade
        {
            get;
            set;
        }

        public PriceType Pricetype
        {
            get;
            set;
        }

        public float Price
        {
            get;
            set;
        }

        public int Volume
        {
            get;
            set;
        }

        public int TradingPrice
        {
            get;
            set;
        }

        public int TradingVolume
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }
    }
}
