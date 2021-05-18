using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static string CurrentValueApi = string.Empty;

        public static decimal PreviousValue = decimal.Zero;
        public static List<decimal> ValueChanges = new List<decimal>();
        public static decimal AverageValue = decimal.Zero;
        public static decimal SmallestValueInAnInterval = decimal.Zero;
        public static decimal BiggestValueInAnInterval = decimal.Zero;

        public static void DecideBasedOnTheCurrentValue(decimal currentValue)
        {
            if (!IsThisTheStartOfTrading())
            {
                Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " Current average price:" + AverageValue, "AverageValues.txt");

                if ((currentValue < AverageValue) && Trading.buyingState.Equals(BuyingState.AlreadySold))
                {
                    Trading.BuyTheCoin(currentValue);
                }
                else if ((currentValue > ((decimal)1.0001 * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Trading.SellTheCoin(currentValue);
                }
                else if ((currentValue < ((decimal)0.97 * TradeInfo.lastBuyingPrice)) && Trading.buyingState.Equals(BuyingState.AlreadyBought))
                {
                    Utilities.WriteToLog(DateTime.Now.ToString("HH:mm:ss tt") + " PANIC SOLD AT:" + AverageValue, "values.txt");
                    Trading.SellTheCoin(currentValue);
                    Trading.shouldStartTrading = false;
                }
            }
        }

        private static bool IsThisTheStartOfTrading()
        {
            return PreviousValue.Equals(decimal.Zero);
        }

        public static void GetCurrentValue()
        {
            IWebDriver driver = ChromeDriverData.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Trading.SelectedCoinUrl);
            Thread.Sleep(1000);
            var element = driver.FindElement(By.Id("FormRow-BUY-price"));

            //IWebDriver driver = AutomationActions.GetWebdriver(Trading.SelectedCoinUrl);
            //IWebElement element = driver.FindElement(By.Id("FormRow-BUY-price"));
            CurrentValue = decimal.Parse(element.GetAttribute("value"), CultureInfo.InvariantCulture);
            CurrentValueApi = BinanceApi.GetCurrentValue().ToString();


            driver.Close();
            driver.Dispose();
        }
        public static void LogCurrentValue()
        {
            PreviousValue = CurrentValue;
            Utilities.WriteToLog(CurrentValue.ToString() + " API:" + Values.CurrentValueApi + "--" + DateTime.Now.ToString("HH:mm:ss tt"), "values.txt");
        }
    }
}
