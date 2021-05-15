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
        private static System.Timers.Timer valueCheckTimer;
        private static string selectedCoinUrl = string.Empty;

        private static decimal currentValue = decimal.Zero;
        private static decimal previousValue = decimal.Zero;
        private static string buyingState = string.Empty;

        private static decimal lastSellingPrice = decimal.Zero;
        private static decimal lastBuyingPrice = decimal.Zero;
        private static DateTime startingTime;
        private static List<decimal> valueChanges = new List<decimal>();


        private static void OnTimedValueCheck(Object source, ElapsedEventArgs e)
        {
            DateTime currentTime = e.SignalTime;
            int hours = (currentTime - startingTime).Hours;
            currentValue = GetCurrentValue();
            valueChanges.Add(currentValue);

            //if (hours.Equals(1))
            //{
                ProcessValues();
            //}

        }

        private static void ProcessValues()
        {
            ProcessCurrentValue(currentValue);
            previousValue = currentValue;
            WriteToLog(currentValue.ToString() + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
        }

        private static void WriteToLog(string valueToWrite, string fileName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using (StreamWriter outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine(valueToWrite);
            }
        }

        private static void ProcessCurrentValue(decimal currentValue)
        {
            if (!previousValue.Equals(decimal.Zero))
            {
                var averageValue = valueChanges.Average();
                WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " Current average price:" + averageValue, "AverageValues.txt");
                if ((currentValue < averageValue) && buyingState.Equals("alreadySold"))
                {
                    BuyTheCoin(currentValue);
                }
                else if ((currentValue > ((decimal)1.001* lastBuyingPrice)) && buyingState.Equals("alreadyBought"))
                {
                    SellTheCoin(currentValue);
                }
                else if (currentValue < ((decimal)0.8*lastBuyingPrice) && buyingState.Equals("alreadyBought"))
                {
                    WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD AT:" + averageValue, "values.txt");
                    SellTheCoin(currentValue);
                }
            }
        }

        private static void SellTheCoin(decimal currentValue)
        {
            if (lastSellingPrice.Equals(decimal.Zero))
            {
                lastSellingPrice = currentValue;
            }          
            WriteToLog("--Sold at " + currentValue.ToString() + " price", "values.txt");
            lastSellingPrice = currentValue;
            decimal buyingBudgetAfterFees = (lastBuyingPrice - lastBuyingPrice / 1000);
            decimal sellingBudgetAfterFees = (lastSellingPrice - lastSellingPrice / 1000);
            decimal profit = sellingBudgetAfterFees - buyingBudgetAfterFees;
            WriteToLog("--Buying budget with fees calculated in was " + buyingBudgetAfterFees.ToString(), "values.txt");
            WriteToLog("--Selling budget with fees calculated in was " + sellingBudgetAfterFees.ToString(), "values.txt");
            WriteToLog("***Profit: " + profit.ToString(), "values.txt");
            WriteToLog("*Percentage: " + ((double)(profit / buyingBudgetAfterFees * 100)).ToString() + "%", "values.txt");
            buyingState = "alreadySold";
        }

        private static void BuyTheCoin(decimal currentValue)
        {
            lastBuyingPrice = currentValue;
            WriteToLog("Bought at " + currentValue.ToString() + " price", "values.txt");
            buyingState = "alreadyBought";
        }

        private static decimal GetCurrentValue()
        {
            IWebDriver driver = ChromeDriverData.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(selectedCoinUrl);
            var element = driver.FindElement(By.Id("FormRow-BUY-price"));
            currentValue = decimal.Parse(element.GetAttribute("value"), CultureInfo.InvariantCulture);
            driver.Close();
            driver.Dispose();
            return currentValue;
        }

        public static void StartTrading(string selectedCoin)
        {
            startingTime = DateTime.Now;
            selectedCoinUrl = selectedCoin;
            buyingState = "alreadySold";
            //NavigateToCryptoPage("ADA", driver);
            ProcessValues();
            CheckCoinValue(30000);
        }
        public static void StopTrading()
        {
            valueCheckTimer.Stop();
            valueCheckTimer.Dispose();
        }

        private static void CheckCoinValue(int timeInterval)
        {
            valueCheckTimer = new System.Timers.Timer(timeInterval);
            valueCheckTimer.Elapsed += OnTimedValueCheck;
            valueCheckTimer.AutoReset = true;
            valueCheckTimer.Enabled = true;
        }

        private static void NavigateToCryptoPage(string tokenName, IWebDriver driver)
        {
            driver.FindElement(By.Id("ba-tableMarkets")).Click();
            driver.FindElement(By.XPath("//input[@value='']")).Click();
            driver.FindElement(By.XPath("//input[@value='']")).SendKeys(tokenName);
            driver.FindElement(By.XPath("//div[@id='__APP']/div/main/div/div[2]/div/div/div[2]/div[2]/div/div[2]/div/div/div/div/div[2]/div/div[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='__APP']/div/main/div/div[2]/div/div/div[2]/div[2]/div/div[2]/div/div/div/div/div[2]/div/div")).Click();
            Thread.Sleep(3000);
            String url = driver.Url;
        }
    }
}
