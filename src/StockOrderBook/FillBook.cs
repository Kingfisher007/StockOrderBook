﻿using StockOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrderBook
{
	public class FillBook
	{
		List<Trade> Trades;

		public FillBook()
		{
			Trades = new List<Trade>();
		}

		public void Add(Trade trade)
		{
			Trades.Add(trade);
		}

		public void AddRange(IList<Trade> trade)
		{
			Trades.AddRange(trade);
		}
	}
}