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
    public class VolumeChartContext : TradeChartContext
    {
        protected ChartValues<VolumeTimeValue> chartvalues;

        public VolumeChartContext(TradeChartViewModel tradechartviewmodel) : base(tradechartviewmodel)
        {
            SetYAxisLimits(200, 5);
            chartvalues = new ChartValues<VolumeTimeValue>();
            var mapper = Mappers.Xy<VolumeTimeValue>().X(m => m.Time.Ticks).Y(m => m.Volume);
            Charting.For<VolumeTimeValue>(mapper);
            DateTimeFormatter = value => { if ((value % TimeSpan.TicksPerMinute) == 0) { return new DateTime((long)value).ToString("hh:mm"); } else { return "."; } };
            tradechartviewmodel.VolumeValues.CollectionChanged += VolumeValues_CollectionChanged;     
        }

        public ChartValues<VolumeTimeValue> ChartValues
        {
            get
            {
                return chartvalues;
            }
        }

        private void VolumeValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            float max = 0.0F;
            foreach (VolumeTimeValue value in e.NewItems)
            {
                chartvalues.Add(value);
                if (YAxisMax < value.Volume)
                {
                    max = value.Volume;
                }
            }

            if (max > maxYAxisValue)
            {
                SetYAxisLimits(max, 5);
            }
            SetXAxisLimits(DateTime.Now);
        }
    }
}
