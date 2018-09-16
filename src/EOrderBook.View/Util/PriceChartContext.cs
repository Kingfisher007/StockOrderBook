using EOrderBook.ViewModel;
using EOrderBook.ViewModel.Entities;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOrderBook.View.Util
{
    public class PriceChartContext : TradeChartContext
    {
        protected ChartValues<PriceTimeValue> chartvalues;

        public PriceChartContext(TradeChartViewModel tradeChartViewModel) : base(tradeChartViewModel)
        {
            SetYAxisLimits(100, 20);
            chartvalues = new ChartValues<PriceTimeValue>();
            var mapper = Mappers.Xy<PriceTimeValue>().X(m => m.Time.Ticks).Y(m => m.Price);
            Charting.For<PriceTimeValue>(mapper);
            tradeChartViewModel.PriceValues.CollectionChanged += PriceValues_CollectionChanged;
            DateTimeFormatter = value => ".";
        }

        public ChartValues<PriceTimeValue> ChartValues
        {
            get
            {
                return chartvalues;
            }
        }

        private void PriceValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            float max = 0.0F;
            foreach(PriceTimeValue value in e.NewItems)
            {
                chartvalues.Add(value);
                if(YAxisMax < value.Price)
                {
                    max = value.Price;
                }
            }

            if(max > maxYAxisValue)
            {
                SetYAxisLimits(max, 20);
            }
            SetXAxisLimits(DateTime.Now);
        }
    }
}
