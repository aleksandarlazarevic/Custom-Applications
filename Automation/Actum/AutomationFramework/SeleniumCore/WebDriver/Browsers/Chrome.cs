using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Handlers;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCore.WebDriver.Browsers
{
    public class Chrome : IDriver
    {
        public virtual IWebDriver Initialize()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                Processes.KillProcess(TestInMemoryParameters.Instance.WebDriver, "chrome");
            }

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
                                       "--whitelisted-ips",
                                       "--disable-gpu");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver(chromeOptions);
        }
    }
}