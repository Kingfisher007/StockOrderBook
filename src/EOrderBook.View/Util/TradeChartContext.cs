using EOrderBook.Entities;
using EOrderBook.ViewModel;
using System;
using System.ComponentModel;

namespace EOrderBook.View.Util
{
    public class TradeChartContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TradeChartViewModel fillbook;
        private double xAxisMax;
        private double xAxisMin;
        private double yAxisMax;
        private const double yAxisMin = 0.0F;
        private double yAxisStep;
        private double yAxisUnit;
        protected double maxYAxisValue;

        public TradeChartContext(TradeChartViewModel fillBook)
        {
            fillbook = fillBook;
            // X Axis
            DateTime now = DateTime.Now;
            XAxisMax = now.Ticks + TimeSpan.FromSeconds(90).Ticks; // lets force the axis to be 120 second ahead
            XAxisMin = now.Ticks - TimeSpan.FromSeconds(1).Ticks; // and 1 seconds behind
            XAxisUnit = TimeSpan.TicksPerSecond; // each division represents a second 
            XAxisStep = TimeSpan.FromSeconds(1).Ticks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertychanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public double XAxisUnit { get; protected set; }
        public double XAxisStep { get; protected set; }
        public Func<double, string> DateTimeFormatter { get; set; }

        public double XAxisMax
        {
            get { return xAxisMax; }
            protected set
            {
                xAxisMax = value;
                RaisePropertychanged("XAxisMax");
            }
        }

        public double XAxisMin
        {
            get { return xAxisMin; }
            protected set
            {
                xAxisMin = value;
                RaisePropertychanged("XAxisMin");
            }
        }

        public double YAxisMax
        {
            get { return yAxisMax; }
            protected set
            {
                yAxisMax = value;
                RaisePropertychanged("YAxisMax");
            }
        }

        public double YAxisMin
        {
            get { return yAxisMin; }
        }

        //
        public double YAxisUnit
        {
            get
            {
                return yAxisUnit;
            }
            protected set
            {
                yAxisUnit = value;
                RaisePropertychanged("YAxisUnit");
            }
        }

        // 
        public double YAxisStep
        {
            get
            {
                return yAxisStep;
            }
            protected set
            {
                yAxisStep = value;
                RaisePropertychanged("YAxisStep");
            }
        }

        public double TopPadding { get; set; }

        protected void SetXAxisLimits(DateTime now)
        {
            if ((XAxisMax - now.Ticks) < TimeSpan.TicksPerSecond)
            {
                // Lets keep rolling ahead and show 120 seconds of data
                XAxisMax = now.Ticks + TimeSpan.TicksPerSecond;
                XAxisMin = now.Ticks - TimeSpan.FromSeconds(89).Ticks;
            }
        }

        protected void SetYAxisLimits(double value, int height)
        {
            YAxisMax = TopPadding + value;
            YAxisStep = YAxisMax / height;
            YAxisUnit = YAxisStep;
        }
    }
}
