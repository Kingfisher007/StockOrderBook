using EOrderBook.Entities;
using EOrderBook.ViewModel.Commands;
using StockOrderBook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EOrderBook.ViewModel
{

    public class SimulationViewModel : ViewModelBase
    {
        DelegateCommand SimulationCommand;
        IOrderManager StockOrderManager;
        bool IsSimulating;
        string status;

        public SimulationViewModel(string ticker, IOrderManager orderManager)
        {
            SimulationCommand = new DelegateCommand(CanSmulate, Simulate);
            StockOrderManager = orderManager;
            IsSimulating = false;
            status = "Start Simulation";
        }

        public ICommand Simulation
        {
            get
            {
                return SimulationCommand;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
        }

        private void Simulate()
        {
            try
            {
                status = "Simulating...";
                RaisePropertychanged("Status");

                IsSimulating = true;
                
                Task.Run(() =>
                {
                    try
                    {
                        char[] seperator = new char[] { ',' };
                        string ticker = "MSFT";
                        int volume = 0;
                        bool toggle = true;
                        Random rand = new Random(20);
                        IEnumerable<string> Lines = File.ReadLines(@"..\..\Resources\stock-orders.txt");
                        IEnumerator<string> iterator = Lines.GetEnumerator();
                        iterator.MoveNext();
                        do
                        {
                            if (string.Empty.Equals(iterator.Current))
                                continue;

                            if(toggle)
                            {
                                volume = rand.Next(10, 150);
                            }

                            string[] order = iterator.Current.Split(seperator);
                            float price = float.Parse(order[2]);
                            if (order[0] == "0")
                            {
                                Ask ask = new Ask(ticker, volume, TradeType.AllOrNothing, PriceType.Limit, price, TimeInForce.GoodForDay);
                                StockOrderManager.NewAsk(ask);
                            }
                            else
                            {
                                Bid bid = new Bid(ticker, TradeType.AllOrNothing, volume, PriceType.Limit, price, TimeInForce.GoodForDay);
                                StockOrderManager.NewBid(bid);
                            }
                            toggle = !toggle;
                            Thread.Sleep(300);
                        } while (iterator.MoveNext());
                    }
                    catch
                    {

                    }
                });

                status = "Start Simulation";
                RaisePropertychanged("Status");
            }
            catch (Exception Ex)
            {
                status = Ex.Message;
                RaisePropertychanged("Status");
            }
            finally
            {
                IsSimulating = false;
            }
        }

        private bool CanSmulate()
        {
            return !IsSimulating;
        }
    }
}
