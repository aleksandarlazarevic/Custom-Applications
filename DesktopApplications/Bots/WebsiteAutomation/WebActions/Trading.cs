using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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

        private static void OnTimedValueCheck(Object source, ElapsedEventArgs e)
        {
            IWebDriver driver = ChromeDriverData.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            driver.Navigate().GoToUrl("https://www.binance.com/en/trade/ADA_BNB?type=spot");
            var element = driver.FindElement(By.Id("FormRow-BUY-price"));
            var value = element.GetAttribute("value");

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "values.txt";
            using (StreamWriter outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine(value);
            }

            driver.Close();
            driver.Dispose();

        }
        public static void StartTrading()
        {
            //NavigateToCryptoPage("ADA", driver);
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
