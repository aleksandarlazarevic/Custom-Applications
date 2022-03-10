using Binance.Net;
using Binance.Net.Interfaces;
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
        public static IBinanceTick Last24hPrices;
        public static decimal SmallestValueInAnInterval = decimal.Zero;
        public static decimal BiggestValueInAnInterval = decimal.Zero;
        public static WebCallResult<BinanceAccountInfo> accountInfo;
        public static decimal CurrentWalletAmount = decimal.Zero;
        public static decimal PreviousWalletAmount = decimal.Zero;


        public static void DecideBasedOnTheCurrentValue(decimal currentValue)
        {
            if (!IsThisTheStartOfTrading())
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " Current average price:" + AverageValue, "AverageValues.txt");
                if ((currentValue < AverageValue) && (PreviousValue > (decimal)0.098 * AverageValue) && Trading.buyingState.Equals(BuyingState.AlreadySold) && Trading.shouldStartTrading && CompareToLast24hMarginOrCustomThresholds(currentValue))
                //if (Trading.buyingState.Equals(BuyingState.AlreadySold) && Trading.shouldStartTrading && CompareToLast24hMarginOrCustomThresholds(currentValue))
                {
                    Trading.BuyTheCoin(currentValue);
                }
                else if ((currentValue > ((decimal)Trading.SellingPercentage * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Trading.SellTheCoin(currentValue);
                }
                //else if (Values.HasWalletValueIncreased() && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                //{
                //    Trading.SellTheCoin(currentValue);
                //}
                else if ((currentValue < ((decimal)0.90 * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD AT:" + AverageValue, "values.txt");
                    Trading.SellTheCoin(currentValue);
                }
            }
            Trading.shouldStartTrading = false;
        }

        private static bool HasWalletValueIncreased()
        {
            bool hasIncreased = false;
            Values.CurrentWalletAmount = Values.GetWalletValue();
            if (Values.CurrentWalletAmount > (decimal)1.0035 * Values.PreviousWalletAmount)
            {
                hasIncreased = true;
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " Wallet amount has increased to:" + Values.CurrentWalletAmount + " from: " + Values.PreviousWalletAmount, "values.txt");
            }

            return hasIncreased;
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
            Values.CurrentWalletAmount = GetWalletValue();
            Values.PreviousValue = CurrentWalletAmount;    
            client.Dispose();
        }

        public static decimal GetWalletValue()
        {
            decimal totalWalletValue = decimal.Zero;
            foreach (var coin in Values.accountInfo.Data.Balances.Where(p => !p.Total.Equals(0)))
            {
                decimal assetValue = BinanceApi.GetCurrentValue(coin.Asset + "USDT");
                decimal coinValueInUsdt = coin.Total * assetValue;
                totalWalletValue += coinValueInUsdt;
            }

            Utilities.WriteToLog("Current Wallet funds: " + totalWalletValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "currentWalletFunds.txt");

            return totalWalletValue;
        }

        public static void GetCurrentValue(string coinToCheck)
        {
            CurrentValue = BinanceApi.GetCurrentValue(coinToCheck);
        }

        public static IBinanceTick GetLast24hValues(string coinsToLook)
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

            IBinanceTick last24hprices = client.Spot.Market.Get24HPrice(coinsToLook).Data;

            return last24hprices;
        }

        public static void LogCurrentValue()
        {
            PreviousValue = CurrentValue;
            //Utilities.WriteToLog(CurrentValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
        }

        public static bool CompareToLast24hMarginOrCustomThresholds(decimal currentValue)
        {
            bool isInsideInterval = false;

            decimal last24hBiggestValue = Values.Last24hPrices.HighPrice;
            decimal last24hSmallestValue = Values.Last24hPrices.LowPrice;

            if (Trading.CustomlyCalculateBuyingThresholds)
            {
                Utilities.WriteToLog("Examinig customly set buying threshold " + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                Utilities.WriteToLog("Current buying threshold: " + Trading.BuyingThreshold.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                Utilities.WriteToLog("Current selling threshold: " + Trading.CustomSellingThreshold.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");

                if ((currentValue > Trading.BuyingThreshold) && (currentValue < Trading.CustomSellingThreshold))
                {
                    isInsideInterval = true;
                }
            }
            else if ((currentValue > (decimal)1.015 * last24hSmallestValue) && (currentValue < (decimal)0.99 * last24hBiggestValue))
            {
                Utilities.WriteToLog("Smallest value in 24h: " + last24hSmallestValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                Utilities.WriteToLog("Biggest value in 24h: " + last24hBiggestValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                isInsideInterval = true;
            }


            Utilities.WriteToLog("Current value: " + currentValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");

            return isInsideInterval;
        }
    }
}
