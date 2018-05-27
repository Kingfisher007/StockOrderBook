using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
    public interface IOrderBook
    {
        IEnumerable<Ask> Asks
        {
            get;
        }

        IEnumerable<Bid> Bids
        {
            get;
        }

        float AskPrice
        {
            get;
        }

        float BidPrice
        {
            get;
        }



        void Remove(Ask ask);
        void Remove(Bid bid);
        void Remove(IList<Ask> asks);
        void Remove(IList<Bid> bids);
        void Add(Ask ask);
        void Add(Bid bid);
    }
}
