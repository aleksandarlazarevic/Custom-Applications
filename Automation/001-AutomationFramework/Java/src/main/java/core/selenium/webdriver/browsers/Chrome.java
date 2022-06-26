using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Handlers;

namespace SeleniumCore.WebDriver.Browsers
{
    public class Chrome : IDriver
    {
        public virtual IWebDriver Initialize()
        {
            var chromeOptions = new ChromeOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                Processes.KillProcess(TestInMemoryParameters.Instance.WebDriver, "chrome");
            }

            string driverLocation = AppDomain.CurrentDomain.BaseDirectory + "BrowserDrivers\\";
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(driverLocation);
            chromeDriverService.EnableVerboseLogging = true;
            chromeDriverService.LogPath = "seleniumLog.txt";

            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", true);
            chromeOptions.AddUserProfilePreference("download.default_directory", Directory.GetCurrentDirectory());
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

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
