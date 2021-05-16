using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebActions
{
    public class TradeInfo
    {
        public static decimal lastSellingPrice = decimal.Zero;
        public static decimal lastBuyingPrice = decimal.Zero;
        public static DateTime LastBuyTime = new DateTime();
        public static DateTime LastSellTime = new DateTime();

    }
}
