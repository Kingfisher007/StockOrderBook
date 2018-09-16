using EOrderBook.View.Util;
using EOrderBook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EOrderBook.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private MainViewModel MainVM { get; set; }
        private VisualisationViewModel VisualisationVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();            
        }

        public MainWindow(MainViewModel mainvm, VisualisationViewModel visualisationvm)
        {
            InitializeComponent();

            this.MainVM = mainvm;
            this.VisualisationVM = visualisationvm;

            this.NewOrderView.DataContext = mainvm.NewOrderVM;
            this.FillBookView.DataContext = mainvm.FillBookVM;
            this.OrderBook.DataContext = mainvm.OrderBookVM;
            this.LogBook.DataContext = mainvm.LogBookVM;
            this.SimulationView.DataContext = mainvm.SimulationVM;

            PriceChartContext priceChartContext = new PriceChartContext(VisualisationVM.TradeChartVM);
            VolumeChartContext volumeChartContext = new VolumeChartContext(VisualisationVM.TradeChartVM);
            this.TradeChatrsView.SetDataContext(VisualisationVM.TradeChartVM, priceChartContext, volumeChartContext);
        }
    }
}
