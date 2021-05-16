using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebActions.Browsers.Chrome;

namespace WebActions
{
    public class AutomationActions
    {
        internal static IWebDriver GetWebdriver(string url)
        {
            IWebDriver driver = ChromeDriverData.GetChromeInstanceWithUserProfile();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            return driver;
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
