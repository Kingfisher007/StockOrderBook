using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public enum TradeType
    {
        AllOrNothing,
        FillOrKill,
        ImmidiateOrCancel,
		AllowPartial,
        Market
    }
}
