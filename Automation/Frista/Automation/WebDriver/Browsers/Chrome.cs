using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Automation.WebDriver.Browsers
{
    public class Chrome : IDriver
    {
        public virtual IWebDriver Initialize()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.EnableVerboseLogging = true;
            chromeDriverService.LogPath = "seleniumLog.txt";
            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            chromeOptions.AddArguments("--disable-popup-blocking",
                                       "--disable-extensions",
                                       "--start-maximized",
                                       "--safebrowsing-disable-download-protection",
                                       "--no-sandbox",
                                       "--disable-dev-shm-usage",
                                       "--disable-gpu");

            return new ChromeDriver(chromeDriverService, chromeOptions, new TimeSpan(0, 6, 0));
        }
    }
}
