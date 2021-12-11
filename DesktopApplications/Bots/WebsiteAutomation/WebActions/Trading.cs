using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using WebActions.Browsers.Chrome;

namespace WebActions
{
    public class Trading
    {
        public static decimal PanicSellPercentage;
        private static System.Timers.Timer ValueCheckTimer;
        public static string SelectedCoinUrl = string.Empty;
        private static DateTime StartingTime;
        private static DateTime Starting24hValueInspectionTime;
        public static bool shouldStartTrading = false;
        public static BuyingState buyingState;
        public static string apiKey = string.Empty;
        public static string secretKey = string.Empty;
        public static decimal PanicSellTimeout;
        public static int AverageValueCalculationPeriod;
        private static bool buyingInProgress;
        private static bool sellingInProgress;
        public static decimal SellingPercentage { get; set; }
        public static decimal BuyingThreshold { get; set; }
        public static decimal CustomBuyingThreshold { get; set; }
        public static decimal CustomSellingThreshold { get; set; }
        public static bool CalculateBuyingThresholdAutomatically { get; set; }
        public static bool CustomlyCalculateBuyingThresholds { get; set; }       
        public static void StartTrading(string selectedCoin)
        {
            StartingTime = DateTime.Now;
            Starting24hValueInspectionTime = DateTime.Now;
            SelectedCoinUrl = selectedCoin;
            buyingInProgress = false;
            sellingInProgress = false;
            buyingState = BuyingState.AlreadySold;
            Values.Last24hPrices = Values.GetLast24hValues(Coin.Pair);

            //NavigateToCryptoPage("ADA", driver);
            Values.GetCurrentValue(Coin.Pair);
            Values.LogCurrentValue();
            PeriodicallyCheckCoinValue(1000);
        }
        public static void StopTrading()
        {
            ValueCheckTimer.Stop();
            ValueCheckTimer.Dispose();
        }

        public async static void BuyTheCoin(decimal currentValue)
        {
            if (!buyingInProgress)
            {
                buyingInProgress = true;
                TradeInfo.lastBuyingPrice = currentValue;
                TradeInfo.LastBuyTime = DateTime.Now;

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

                decimal pairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                Utilities.WriteToLog(Coin.PairedCoin + " pre-BUYing value: " + pairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");

                if (pairedCoinBalance > (decimal)1)
                {
                    try
                    {
                        var result = await client.Spot.Order.PlaceOrderAsync(Coin.Pair, OrderSide.Buy, OrderType.Market, quoteOrderQuantity: pairedCoinBalance).ConfigureAwait(true);
                        if (result.Success)
                        {
                            Utilities.WriteToLog("Bought at " + currentValue.ToString() + " price", "values.txt");
                            Trading.buyingState = BuyingState.AlreadyBought;
                            Values.accountInfo = client.General.GetAccountInfo();
                            //Values.PreviousWalletAmount = Values.GetWalletValue();

                            buyingInProgress = false;
                            sellingInProgress = false;
                            //await WaitForBuyingToComplete();

                            decimal postbuyingPairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.PairedCoin + " post-BUYing value: " + postbuyingPairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                            decimal postbuyingCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.Name)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.Name + " post-BUYing value: " + postbuyingCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                        }
                        else
                        {
                            MessageBox.Show("Buying coin Failed: " + result.Error.Message);

                            decimal failedBuyingPairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.PairedCoin + " failed BUYing value: " + failedBuyingPairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                            decimal failedBuyingCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.Name)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.Name + " failed BUYing value: " + failedBuyingCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                client.Dispose();
            }
        }

        private static Task WaitForBuyingToComplete()
        {
            throw new NotImplementedException();
        }

        public async static void SellTheCoin(decimal currentValue)
        {
            if (!sellingInProgress)
            {
                sellingInProgress = true;
                CheckIfThisIsTheStartOfTrading(currentValue);
                TradeInfo.lastSellingPrice = currentValue;
                TradeInfo.LastSellTime = DateTime.Now;
                decimal buyingBudgetAfterFees = (TradeInfo.lastBuyingPrice - TradeInfo.lastBuyingPrice / 1000);
                decimal sellingBudgetAfterFees = (TradeInfo.lastSellingPrice - TradeInfo.lastSellingPrice / 1000);
                decimal profit = sellingBudgetAfterFees - buyingBudgetAfterFees;
                //Utilities.WriteToLog("--Buying budget with fees calculated in was " + buyingBudgetAfterFees.ToString(), "values.txt");
                //Utilities.WriteToLog("--Selling budget with fees calculated in was " + sellingBudgetAfterFees.ToString(), "values.txt");

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

                decimal coinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.Name)).Select(t => t.Free).First();

                decimal preSellingPairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                Utilities.WriteToLog(Coin.PairedCoin + " pre-SELLing value: " + preSellingPairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                Utilities.WriteToLog(Coin.Name + " pre-SELLing value: " + coinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                int roundedBalance = (int)Math.Floor(coinBalance * currentValue);

                if (roundedBalance > (decimal)1)
                {
                    try
                    {
                        Utilities.WriteToLog("Trying to sell: " + Coin.Pair + ", balance: " + roundedBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                        var result = await client.Spot.Order.PlaceOrderAsync(Coin.Pair, OrderSide.Sell, OrderType.Market, quoteOrderQuantity: roundedBalance).ConfigureAwait(true);
                        Thread.Sleep(3000);
                        if (result.Success)
                        {
                            Utilities.WriteToLog("--Sold at " + currentValue.ToString() + " price", "values.txt");
                            Utilities.WriteToLog("***Profit: " + profit.ToString(), "values.txt");
                            Utilities.WriteToLog("*Percentage: " + ((double)(profit / buyingBudgetAfterFees * 100)).ToString() + "%", "values.txt");
                            Trading.buyingState = BuyingState.AlreadySold;
                            Values.accountInfo = client.General.GetAccountInfo();
                            //Values.PreviousWalletAmount = Values.GetWalletValue();

                            shouldStartTrading = false;
                            sellingInProgress = false;

                            decimal postSellingPairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.PairedCoin + " post-SELLing value: " + postSellingPairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                            decimal postSellingCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.Name)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.Name + " post-SELLing value: " + postSellingCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                        }
                        else
                        {
                            MessageBox.Show("Selling coin Failed: " + result.Error.Message);
                            sellingInProgress = false;

                            Utilities.WriteToLog("Selling coin Failed: " + result.Error.Message + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                            decimal failedSellingPairedCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.PairedCoin)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.PairedCoin + " failed SELLing value: " + failedSellingPairedCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                            decimal failedSellingCoinBalance = Values.accountInfo.Data.Balances.Where(p => p.Asset.Equals(Coin.Name)).Select(t => t.Free).First();
                            Utilities.WriteToLog(Coin.Name + " failed SELLing value: " + failedSellingCoinBalance.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        sellingInProgress = false;
                    }
                }

                client.Dispose();
            }
        }

        public static void BuyTheCoinSimulated(decimal currentValue)
        {
            TradeInfo.lastBuyingPrice = currentValue;
            TradeInfo.LastBuyTime = DateTime.Now;
            Utilities.WriteToLog("Bought at " + currentValue.ToString() + " price", "values.txt");
            Trading.buyingState = BuyingState.AlreadyBought;
        }
        public static void SellTheCoinSimulated(decimal currentValue)
        {
            CheckIfThisIsTheStartOfTrading(currentValue);
            Utilities.WriteToLog("--Sold at " + currentValue.ToString() + " price", "values.txt");
            TradeInfo.lastSellingPrice = currentValue;
            TradeInfo.LastSellTime = DateTime.Now;
            decimal buyingBudgetAfterFees = (TradeInfo.lastBuyingPrice - TradeInfo.lastBuyingPrice / 1000);
            decimal sellingBudgetAfterFees = (TradeInfo.lastSellingPrice - TradeInfo.lastSellingPrice / 1000);
            decimal profit = sellingBudgetAfterFees - buyingBudgetAfterFees;
            Utilities.WriteToLog("--Buying budget with fees calculated in was " + buyingBudgetAfterFees.ToString(), "values.txt");
            Utilities.WriteToLog("--Selling budget with fees calculated in was " + sellingBudgetAfterFees.ToString(), "values.txt");
            Utilities.WriteToLog("***Profit: " + profit.ToString(), "values.txt");
            Utilities.WriteToLog("*Percentage: " + ((double)(profit / buyingBudgetAfterFees * 100)).ToString() + "%", "values.txt");
            Trading.buyingState = BuyingState.AlreadySold;
        }

        private static void CheckIfThisIsTheStartOfTrading(decimal currentValue)
        {
            if (TradeInfo.lastSellingPrice.Equals(decimal.Zero))
            {
                TradeInfo.lastSellingPrice = currentValue;
            }
        }

        private static void PeriodicallyCheckCoinValue(int miliSeconds)
        {
            ValueCheckTimer = new System.Timers.Timer(miliSeconds);
            ValueCheckTimer.Elapsed += OnTimedValueCheck;
            ValueCheckTimer.AutoReset = true;
            ValueCheckTimer.Enabled = true;
        }
        private static void OnTimedValueCheck(Object source, ElapsedEventArgs e)
        {
            DateTime currentTime = e.SignalTime;
            int minutes = (currentTime - StartingTime).Minutes;
            int dailyPriceRetrievalThreshold = (currentTime - Starting24hValueInspectionTime).Hours;
            
            Values.GetCurrentValue(Coin.Pair);
            Values.LogCurrentValue();
            Values.ValueChanges.Add(Values.CurrentValue);

            if (minutes.Equals(AverageValueCalculationPeriod))
            {
                CalculateAverageValue();
            }
            if (dailyPriceRetrievalThreshold.Equals(6))
            {
                Values.Last24hPrices = Values.GetLast24hValues(Coin.Pair);
                Starting24hValueInspectionTime = currentTime;
            }

            CheckIfShouldPanicSell();

            Values.DecideBasedOnTheCurrentValue(Values.CurrentValue);
        }

        private static void CheckIfShouldPanicSell()
        {
            Int32 timeWithoutSelling = (DateTime.Now - TradeInfo.LastBuyTime).Minutes;
            //if (timeWithoutSelling.Equals(PanicSellTimeout) && buyingState.Equals(BuyingState.AlreadyBought))
            //{
            //    Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD (long time no sell) AT:" + Values.CurrentValue, "values.txt");
            //    SellTheCoin(Values.CurrentValue);
            //    shouldStartTrading = false;
            //}
            //else 
            if (Values.SmallestValueInAnInterval < PanicSellPercentage * TradeInfo.lastBuyingPrice)
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD (10% value drop) AT:" + Values.CurrentValue, "values.txt");
                SellTheCoin(Values.CurrentValue);
                shouldStartTrading = false;
            }
        }

        public static bool CurrentPricesAreWayBelowAverageValue()
        {
            List<decimal> wayTooLowValues = new List<decimal>();
            foreach (decimal value in Values.ValueChanges.Skip(Values.ValueChanges.Count - 30))
            {
                if (value < (decimal)0.99 * Values.AverageValue)
                {
                    wayTooLowValues.Add(value);
                }
            }
            if (wayTooLowValues.Count() > 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void CalculateAverageValue()
        {
            if (!shouldStartTrading)
            {
                StartingTime = DateTime.Now;
                if (Trading.BuyingThreshold > decimal.Zero)
                {
                    Values.AverageValue = Trading.BuyingThreshold;
                }
                else
                {
                    Values.AverageValue = Values.ValueChanges.Average();
                }

                if (Trading.CalculateBuyingThresholdAutomatically)
                {
                    Values.AverageValue = RecalculateBuyingThreshold(Values.ValueChanges);
                }

                Values.SmallestValueInAnInterval = Values.ValueChanges.Min();
                Values.BiggestValueInAnInterval = Values.ValueChanges.Max();

                if ((Values.SmallestValueInAnInterval > (decimal)0.98 * Values.BiggestValueInAnInterval) && !CurrentPricesAreWayBelowAverageValue())
                {
                    shouldStartTrading = true;
                }
                else
                {
                    shouldStartTrading = false;
                }
                Values.ValueChanges = new List<decimal>();
            }
        }

        private static decimal RecalculateBuyingThreshold(List<decimal> valueChanges)
        {
            
            decimal mostCommonValue = valueChanges.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            Utilities.WriteToLog("Recalculated buying threshold" + mostCommonValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");

            return Math.Floor(mostCommonValue);
        }
    }
}
