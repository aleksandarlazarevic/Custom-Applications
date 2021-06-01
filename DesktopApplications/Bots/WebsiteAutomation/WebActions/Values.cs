using Binance.Net;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebActions.Browsers.Chrome;

namespace WebActions
{
    public class Values
    {
        public static decimal CurrentValue = decimal.Zero;
        public static decimal PreviousValue = decimal.Zero;
        public static List<decimal> ValueChanges = new List<decimal>();
        public static decimal AverageValue = decimal.Zero;
        public static decimal SmallestValueInAnInterval = decimal.Zero;
        public static decimal BiggestValueInAnInterval = decimal.Zero;
        public static WebCallResult<BinanceAccountInfo> accountInfo;

        public static void DecideBasedOnTheCurrentValue(decimal currentValue)
        {
            if (!IsThisTheStartOfTrading())
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " Current average price:" + AverageValue, "AverageValues.txt");

                if ((currentValue < AverageValue) && (PreviousValue > (decimal)0.098*AverageValue) && Trading.buyingState.Equals(BuyingState.AlreadySold) && Trading.shouldStartTrading)
                {
                    Trading.BuyTheCoin(currentValue);
                }
                else if ((currentValue > ((decimal)Trading.SellingPercentage * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Trading.SellTheCoin(currentValue);
                }
                else if ((currentValue < ((decimal)0.90 * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD AT:" + AverageValue, "values.txt");
                    Trading.SellTheCoin(currentValue);
                }
            }
            Trading.shouldStartTrading = false;
        }

        private static bool IsThisTheStartOfTrading()
        {
            return PreviousValue.Equals(decimal.Zero);
        }

        public static void GetAccountInfo()
        {
            var client = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(Trading.apiKey, Trading.secretKey),
                AutoTimestamp = true,
                AutoTimestampRecalculationInterval = TimeSpan.FromMinutes(30),
                BaseAddress = "https://api.binance.com",
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });
            Values.accountInfo = client.General.GetAccountInfo();
            client.Dispose();
        }

        public static void GetCurrentValue()
        {
            CurrentValue = BinanceApi.GetCurrentValue();
        }
        public static void LogCurrentValue()
        {
            PreviousValue = CurrentValue;
            //Utilities.WriteToLog(CurrentValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
        }
    }
}
