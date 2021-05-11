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


        private static void OnTimedValueCheck(Object source, ElapsedEventArgs e)
        {
            ProcessValues();
        }

        private static void ProcessValues()
        {
            currentValue = GetCurrentValue();
            ProcessCurrentValue(currentValue);
            previousValue = currentValue;
            WriteToLog(currentValue.ToString());
        }

        private static void WriteToLog(string valueToWrite)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "values.txt";
            using (StreamWriter outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine(valueToWrite);
            }
        }

        private static void ProcessCurrentValue(decimal currentValue)
        {
            if (!previousValue.Equals(decimal.Zero))
            {
                if ((currentValue > previousValue) && buyingState.Equals("alreadySold"))
                {
                    BuyTheCoin(currentValue);
                    buyingState = "alreadyBought";
                }
                else if ((currentValue < previousValue) && buyingState.Equals("alreadyBought"))
                {
                    SellTheCoin(currentValue);
                    buyingState = "alreadySold";
                }
            }
        }

        private static void SellTheCoin(decimal currentValue)
        {
            if (lastSellingPrice.Equals(decimal.Zero))
            {
                lastSellingPrice = currentValue;
            }          
            WriteToLog("--Sold at " + currentValue.ToString() + " price");
            WriteToLog("--Previous price was " + previousValue.ToString());
            decimal buyingBudgetAfterFees = (lastBuyingPrice - lastBuyingPrice / 1000);
            decimal profit = buyingBudgetAfterFees - lastSellingPrice;
            WriteToLog("--Buying budget with fees calculated in was " + (buyingBudgetAfterFees).ToString());
            WriteToLog("***Profit: " + profit.ToString());
            WriteToLog("*Percentage: " + (profit / buyingBudgetAfterFees * 100).ToString() + "%");
            lastSellingPrice = currentValue;
        }

        private static void BuyTheCoin(decimal currentValue)
        {
            lastBuyingPrice = currentValue;
            WriteToLog("Bought at " + currentValue.ToString() + " price");
            WriteToLog("Previous price was " + previousValue.ToString());
        }

        private static decimal GetCurrentValue()
        {
            IWebDriver driver = ChromeDriverData.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}");
            driver.Navigate().GoToUrl(selectedCoinUrl);
            var element = driver.FindElement(By.Id("FormRow-BUY-price"));
            currentValue = decimal.Parse(element.GetAttribute("value"), CultureInfo.InvariantCulture);
            driver.Close();
            driver.Dispose();
            return currentValue;
        }

        public static void StartTrading(string selectedCoin)
        {
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
