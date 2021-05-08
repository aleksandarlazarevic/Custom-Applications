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
        private static System.Timers.Timer aTimer;

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            IWebDriver driver = ChromeDriverData.Instance.GetChromeInstanceWithUserProfile();
            driver.Navigate().GoToUrl("https://www.binance.com/en/trade/ADA_BNB?type=spot");
            var element = driver.FindElement(By.Id("FormRow-BUY-price"));
            var value = element.GetAttribute("value");

            string filePath = @"D:\values.txt";
            using (StreamWriter outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine(value);
            }

        }
        public static void StartTrading()
        {
            IWebDriver driver = ChromeDriverData.Instance.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();

            NavigateToCryptoPage("ADA", driver);
            CheckCoinValue(driver);
            driver.Close();
            driver.Dispose();
        }

        private static void CheckCoinValue(IWebDriver driver)
        {
            SetTimer();

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
            driver.Close();
            driver.Dispose();

        }
    }
}
