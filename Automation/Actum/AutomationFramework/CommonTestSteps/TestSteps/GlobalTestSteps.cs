using CommonTestSteps.Contracts;
using OpenQA.Selenium;
using SeleniumCore;
using SeleniumCore.Enums;
using SeleniumCore.Helpers.Utilities;
using SeleniumCore.WebDriver;
using ServiceStack.Host;

namespace CommonTestSteps.TestSteps
{
    public class GlobalTestSteps : IGlobalTestSteps
    {
        public void CloseBrowser()
        {
            UIDriver.WebDriver.Quit();
        }

        public void CloseCurrentTab()
        {
            UIDriver.WebDriver.SwitchTo().Window(UIDriver.WebDriver.WindowHandles.Last());
            UIDriver.WebDriver.Close();
            UIDriver.WebDriver.SwitchTo().Window(UIDriver.WebDriver.WindowHandles.First());
        }

        public void GoToUrl(string url)
        {
            try
            {
                UIDriver.WebDriver.Navigate().GoToUrl(url);
            }
            catch
            {
                throw new HttpException(string.Format("Unable to navigate to URL [{0}].", url));
            }
        }

        public void OpenBrowser()
        {
            try
            {
                uint elementTimeout;
                uint pageLoudTimeout;

                if (!uint.TryParse(TestInMemoryParameters.Instance.ElementTimeout, out elementTimeout))
                {
                    throw new FormatException("Not able to parse 'ElementTimeout'");
                }

                if (!uint.TryParse(TestInMemoryParameters.Instance.PageLoadTimeout, out pageLoudTimeout))
                {
                    throw new FormatException("Not able to parse 'PageLoadTimeout'");
                }

                UIDriver.InitializeWebDriver(TestInMemoryParameters.Instance.WebDriver, elementTimeout, pageLoudTimeout);

                Thread.Sleep(5000);

                if (!WebsiteManager.IsWebsiteAvailable(TestInMemoryParameters.Instance.Url))
                {
                }

                UIDriver.WebDriver.Navigate().GoToUrl(TestInMemoryParameters.Instance.Url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while navigating to:[{TestInMemoryParameters.Instance.Url}]", ex);
            }
        }

        public void OpenNewTab()
        {
            ((IJavaScriptExecutor)UIDriver.WebDriver).ExecuteScript("window.open();");
            UIDriver.WebDriver.SwitchTo().Window(UIDriver.WebDriver.WindowHandles.Last());
        }

        public void SwitchToBrowserInstance(BrowserInstance browser)
        {
            try
            {
                int instance = (int)browser;
                if (instance > UIDriver.webDrivers.Count)
                {
                    throw new ArgumentException("There is no instance to switch to.");
                }
                else
                {
                    UIDriver.WebDriver = UIDriver.webDrivers[instance - 1];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to switch browser instance", ex);
            }
        }

        public void SwitchToBrowserWindowByOrder(int windowNumber)
        {
            if (windowNumber > UIDriver.WebDriver.WindowHandles.Count)
            {
                throw new ArgumentOutOfRangeException("Selected window number is greater than the number of opened windows!");
            }

            UIDriver.WebDriver.SwitchTo().Window(UIDriver.WebDriver.WindowHandles[windowNumber]);
        }

        public void SwitchToFirstBrowserWindow()
        {
            SwitchToBrowserWindowByOrder(0);
        }

        public void SwitchToLastBrowserWindow()
        {
            UIDriver.WebDriver.SwitchTo().Window(UIDriver.WebDriver.WindowHandles.Last());
        }
    }
}