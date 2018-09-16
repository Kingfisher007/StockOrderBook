using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EOrderBook.View.Convertors
{
    public class LogStateToImageConvertor : IValueConverter
    {
        string path = @"..\..\Resources\";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OrderState state = (OrderState)Enum.Parse(typeof(OrderState), value.ToString());
            string imagename = "Error.png";
            switch(state)
            {
                case OrderState.Queued:
                    imagename = "Info.png";
                    break;
                case OrderState.Traded:
                    imagename = "Success.png";
                    break;
                default:
                    imagename = "Error.png";
                    break;
            }
            return String.Format("{0}{1}", path, imagename);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
