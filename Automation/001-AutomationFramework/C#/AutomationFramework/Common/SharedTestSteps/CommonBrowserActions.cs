using OpenQA.Selenium;
using SeleniumEngine.Extensions;
using SeleniumEngine.DriverInitialization;
using SharedTestSteps.Contracts;
using SeleniumEngine.DriverInitialization.Browsers;
using CommonCore.Tests;
using Utilities;

namespace SharedTestSteps
{
    public class CommonBrowserActions : ICommonBrowserActions
    {
        public void OpenBrowserAndGoToDefaultUrl()
        {
            try
            {
                WebDriverFactory.InitializeWebDriver(TestInMemoryParameters.Instance.DriverType);
                Thread.Sleep(3000);
                WebDriverFactory.WebDriver.NavigateToUrl(TestInMemoryParameters.Instance.Url);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Failed to start the browser: {0}", exception.Message));
            }
        }

        public void CloseBrowser()
        {
            WindowsProcessManager.KillProcess(TestInMemoryParameters.Instance.DriverType);
        }

        public void OpenUrl(string url)
        {
            try
            {
                WebDriverFactory.WebDriver.NavigateToUrl(url);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Failed navigating to URL: {0} - {1}", url, exception.Message));
            }
        }

        public void GoToDefaultEmailService()
        {
            TestInMemoryParameters.Instance.Url = TestInMemoryParameters.Instance.EmailServiceUrl;
            OpenBrowserAndGoToDefaultUrl();
        }

        public void GoToSpecificEmailService(string emailService)
        {
            var service = TestInMemoryParameters.Instance.EmailServices.Find(x => x.Name == emailService);

            if (service == null)
            {
                throw new ArgumentException(string.Format("Email service: {0} is unavailable", emailService));
            }

            TestInMemoryParameters.Instance.Url = TestInMemoryParameters.Instance.EmailServiceUrl = service.Url;
            OpenBrowserAndGoToDefaultUrl();
        }

        public void OpenNewTab()
        {
            ((IJavaScriptExecutor)WebDriverFactory.WebDriver).ExecuteScript("window.open();");
            WebDriverFactory.WebDriver.SwitchTo().Window(WebDriverFactory.WebDriver.WindowHandles.Last());
        }

        public void CloseCurrentTab()
        {
            WebDriverFactory.WebDriver.SwitchTo().Window(WebDriverFactory.WebDriver.WindowHandles.Last());
            WebDriverFactory.WebDriver.Close();
            WebDriverFactory.WebDriver.SwitchTo().Window(WebDriverFactory.WebDriver.WindowHandles.First());
        }

        public void AcceptAlertPopUp()
        {
            Thread.Sleep(5000);

            try
            {
                IAlert alert = WebDriverFactory.WebDriver.SwitchTo().Alert();
                Thread.Sleep(500);
                alert.Accept();
                Thread.Sleep(3000);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Failed accepting alert pop-up: {0}", exception.Message));
            }
        }

        public void SwitchToSpecificBrowserInstance(BrowserInstance browser)
        {
            try
            {
                int instanceNumber = (int)browser;

                if (instanceNumber > WebDriverFactory.webDrivers.Count)
                {
                    throw new ArgumentException(string.Format("Failed to enumerate {0} browser instances", instanceNumber));
                }
                else
                {
                    WebDriverFactory.WebDriver = WebDriverFactory.webDrivers[instanceNumber - 1];
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Failed switching browsers: {0}", exception.Message));
            }
        }

        public void SwitchToSpecificBrowserWindow(int windowNumber)
        {
            if (windowNumber > WebDriverFactory.WebDriver.WindowHandles.Count)
            {
                throw new ArgumentOutOfRangeException(string.Format("There aren't {0} open windows", windowNumber));
            }

            WebDriverFactory.WebDriver.SwitchTo().Window(WebDriverFactory.WebDriver.WindowHandles[windowNumber]);
        }

        public void SwitchToFirstBrowserWindow()
        {
            SwitchToSpecificBrowserWindow(0);
        }

        public void SwitchToLastBrowserWindow()
        {
            WebDriverFactory.WebDriver.SwitchTo().Window(WebDriverFactory.WebDriver.WindowHandles.Last());
        }
    }
}