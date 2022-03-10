using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Automation.WebDriver
{
    public class UIDriver
    {
        public static IWebDriver WebDriver { get; set; }

        public static void InitializeWebDriver()
        {
            try
            {
                string driverLocation = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\";
                WebDriver = new ChromeDriver(driverLocation);
                WebDriver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to initialize WebDriver: ", ex);
            }
        }

        public static void DisposeWebDriver()
        {
            try
            {
                WebDriver.Quit();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to dispose WebDriver: ", ex);
            }
        }

        public static IWebElement FindElementByXpath(string xpath)
        {
            IWebElement element = WebDriver.FindElement(By.XPath(xpath));
            return element;
        }
    }
}
