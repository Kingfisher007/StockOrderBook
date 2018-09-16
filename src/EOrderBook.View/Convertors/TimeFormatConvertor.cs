using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EOrderBook.View.Convertors
{
    public class TimeFormatConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String result = String.Empty;
            try
            {
                DateTime datetime = (DateTime)value;
                result = String.Format("{0}   {1}", datetime.ToString("yyyy-MMM-dd"), datetime.ToString("hh:mm:ss.fff tt"));
            }
            catch { }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
