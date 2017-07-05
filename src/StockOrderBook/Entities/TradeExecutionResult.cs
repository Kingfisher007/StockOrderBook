using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook.Entities
{
    public class TradeExecutionResult
    {
		public float BidPrice { get; protected set; }
		public float AskPrice { get; protected set; }
        public TradeResult Result { get; protected set; }

        public TradeExecutionResult(TradeResult result, float tradeBidPrice, float tradeAskPrice)
        {
            Result = result;
			BidPrice = tradeBidPrice;
			AskPrice = tradeAskPrice;
        }
    }
}
