using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EOrderBook.View.Convertors
{
    public class PriceTypeToStringMultiConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string label = Enum.GetName(values[0].GetType(), values[0]);
            if(label.ToLower().Equals("limit"))
            {
                return values[1].ToString();
            }
            else
            {
                return label;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
