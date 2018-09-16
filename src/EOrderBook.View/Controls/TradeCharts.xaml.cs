using EOrderBook.View.Util;
using EOrderBook.ViewModel;
using LiveCharts;
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

namespace EOrderBook.View.Controls
{
    /// <summary>
    /// Interaction logic for TradeCharts.xaml
    /// </summary>
    public partial class TradeCharts : UserControl
    {
        public TradeCharts()
        {
            InitializeComponent();
        }

        public void SetDataContext(TradeChartViewModel viewModel, PriceChartContext priceContext, VolumeChartContext volumeContext)
        {
            this.DataContext = viewModel;
            this.TradePriceChart.DataContext = priceContext;
            this.TradeVolumeChart.DataContext = volumeContext;
        }
    }
}
