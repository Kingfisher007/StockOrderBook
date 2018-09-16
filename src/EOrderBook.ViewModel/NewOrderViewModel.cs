using EOrderBook.ViewModel.Commands;
using EOrderBook.ViewModel.Entities;
using StockOrderBook;
using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EOrderBook.ViewModel
{
    public class NewOrderViewModel
    {
        DelegateCommand placeOrder;
        DelegateCommand cancel;
        protected bool Processing;
        IOrderManager ordermanager;
        string Ticker;

        public NewOrderViewModel(string ticker, IOrderManager orderManager)
        {
            Ticker = ticker;
            ordermanager = orderManager;
            TradeOptions = new List<TradeOption> {
                                                    new TradeOption(TradeType.AllOrNothing),
                                                    new TradeOption(TradeType.FillOrKill),
                                                    new TradeOption(TradeType.ImmidiateOrCancel),
                                                    new TradeOption(TradeType.None)
                                                 };
            Processing = false;
            placeOrder = new DelegateCommand(CanPlaceOrder, PlaceOrder);
            cancel = new DelegateCommand(CanClearOrder, Clear);
        }

        public ICommand Ok
        {
            get { return placeOrder; }
        }

        public ICommand Cancel
        {
            get { return cancel; }
        }

        public OrderType OrderType
        {
            get;
            set;
        }

        public PriceType PriceType
        {
            get;
            set;
        }

        public float Price
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public TradeType Option
        {
            get;
            set;
        }

        public List<TradeOption> TradeOptions
        {
            get;
            protected set;
        }

        protected bool CanPlaceOrder()
        {
            return !Processing && ValidateOrder();
        }

        private bool ValidateOrder()
        {
            return Price >= 0.0F && Quantity > 0;
        }

        protected void PlaceOrder()
        {
            Processing = true;
            if(OrderType == OrderType.Ask)
            {
                Ask ask = new Ask(Ticker, Quantity, Option, PriceType, Price, TimeInForce.GoodUntilCancelled);
                ordermanager.NewAsk(ask);
            }
            else
            {
                Bid bid = new Bid(Ticker, Option, Quantity, PriceType, Price, TimeInForce.GoodUntilCancelled);
                ordermanager.NewBid(bid);
            }
            Processing = false;
            Clear();
        }

        protected bool CanClearOrder()
        {
            return !Processing;
        }

        public void Clear()
        {

        }
    }
}
