using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.Entities
{
    public enum TradeType
    {
        AllOrNothing = 0,
        FillOrKill = 1,
        ImmidiateOrCancel = 2,
        None = 3
    }
}
