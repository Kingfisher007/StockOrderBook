using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    public interface IOrderManager
    {
        bool NewAsk(Ask ask);
        bool NewBid(Bid bid);
    }
}
