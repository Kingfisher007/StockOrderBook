using EOrderBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EOrderBook.ViewModel.Entities
{
    public class TradeOption
    {
        TradeType type;
        string displaytext;

        public TradeOption(TradeType tradetype)
        {
            type = tradetype;
            displaytext = Regex.Replace(type.ToString(), "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1");
        }

        public TradeType Type
        {
            get
            {
                return type;
            }
        }

        public string DisplayText
        {
            get
            {
                return displaytext;
            }
        }

        public override string ToString()
        {
            return displaytext;
        }
    }
}
