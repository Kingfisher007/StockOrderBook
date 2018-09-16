using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EOrderBook.View.ValidationRules
{
    class IntValidationRule : ValidationRule
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;
            if (int.TryParse(value.ToString(), out number))
            {
                return new ValidationResult(true, String.Empty);
            }
            else
            {
                return new ValidationResult(false, ErrorMessage);
            }
        }
    }
}
