using CommonCore.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Utilities;

namespace SeleniumEngine.DriverInitialization.Browsers
{
    public class Firefox : IBrowserDriver
    {
        public IWebDriver Initialize()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                WindowsProcessManager.KillProcess(TestInMemoryParameters.Instance.DriverType, "geckodriver");
            }

            string driverLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BrowserDrivers\\Firefox\\";
            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService(driverLocation);

            firefoxOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            firefoxOptions.AddAdditionalFirefoxOption("--disable-popup-blocking", true);
            firefoxOptions.AddAdditionalFirefoxOption("--disable-extensions", true);
            firefoxOptions.AddAdditionalFirefoxOption("--start-maximized", true);
            firefoxOptions.AddAdditionalFirefoxOption("--safebrowsing-disable-download-protection", true);
            firefoxOptions.AddAdditionalFirefoxOption("--no-sandbox", true);
            firefoxOptions.AddAdditionalFirefoxOption("--disable-dev-shm-usage", true);
            firefoxOptions.AddAdditionalFirefoxOption("--disable-gpu", true);
            firefoxOptions.AddAdditionalFirefoxOption("--whitelisted-ips", "");

            return new FirefoxDriver(firefoxDriverService, firefoxOptions, new TimeSpan(0, 6, 0));
        }
    }
}