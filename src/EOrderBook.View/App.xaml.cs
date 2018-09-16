using EOrderBook.ViewModel;
using ExchangeBuilder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EOrderBook.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    MainWindow mainwindow = new MainWindow();
        //    this.MainWindow = mainwindow;
        //    mainwindow.Show();

        //}

        [STAThread]
        static void Main()
        {
            try
            {
                //
                StockExchange stockExchange = ExchangeBuilder.ExchangeBuilder.Build("MSFT");
                stockExchange.Start();
                //
                ViewModelProvider VMProvider = new ViewModelProvider(stockExchange);
                //
                App app = new App();
                MainWindow main = new MainWindow(VMProvider.GetMainViewModel(), VMProvider.GetVisualisationViewModel());
                //
                app.Run(main);
                //
                stockExchange.Stop();
                //
            }
            catch(Exception ex)
            {

            }
        }
    }
}
