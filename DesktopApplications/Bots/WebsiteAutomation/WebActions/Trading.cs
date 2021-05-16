using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WebActions.Browsers.Chrome;

namespace WebActions
{
    public class Trading
    {
        private static System.Timers.Timer ValueCheckTimer;
        public static string SelectedCoinUrl = string.Empty;
        private static DateTime StartingTime;
        private static bool shouldStartTrading = false;
        public static BuyingState buyingState;

        public static void StartTrading(string selectedCoin)
        {
            StartingTime = DateTime.Now;
            SelectedCoinUrl = selectedCoin;
            Trading.buyingState = BuyingState.AlreadySold;

            //NavigateToCryptoPage("ADA", driver);
            Values.GetCurrentValue();
            Values.LogCurrentValue();
            PeriodicallyCheckCoinValue(30000);
        }
        public static void StopTrading()
        {
            ValueCheckTimer.Stop();
            ValueCheckTimer.Dispose();
        }

        public static void BuyTheCoin(decimal currentValue)
        {
            TradeInfo.lastBuyingPrice = currentValue;
            TradeInfo.LastBuyTime = DateTime.Now;
            Utilities.WriteToLog("Bought at " + currentValue.ToString() + " price", "values.txt");
            Trading.buyingState = BuyingState.AlreadyBought;
        }
        public static void SellTheCoin(decimal currentValue)
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
            Values.GetCurrentValue();
            Values.LogCurrentValue();
            Values.ValueChanges.Add(Values.CurrentValue);

            CalculateAverageValue(minutes);
            CheckIfShouldPanicSell();

            if (shouldStartTrading)
            {
                Values.DecideBasedOnTheCurrentValue(Values.CurrentValue);
            }
        }

        private static void CheckIfShouldPanicSell()
        {
            Int32 timeWithoutSelling = (DateTime.Now - TradeInfo.LastBuyTime).Hours;
            if (timeWithoutSelling.Equals(60) && buyingState.Equals(BuyingState.AlreadyBought))
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD (long time no sell) AT:" + Values.CurrentValue, "values.txt");
                SellTheCoin(Values.CurrentValue);
            }
            else if (Values.SmallestValueInAnInterval < (decimal)0.95*TradeInfo.lastBuyingPrice)
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD (value drop) AT:" + Values.CurrentValue, "values.txt");
                SellTheCoin(Values.CurrentValue);
            }
        }

        private static void CalculateAverageValue(int minutes)
        {
            if (minutes.Equals(3))
            {
                StartingTime = DateTime.Now;
                Values.AverageValue = Values.ValueChanges.Average();
                Values.SmallestValueInAnInterval = Values.ValueChanges.Min();
                Values.BiggestValueInAnInterval = Values.ValueChanges.Max();
                Values.ValueChanges = new List<decimal>();
                shouldStartTrading = true;
            }
        }
    }
}
