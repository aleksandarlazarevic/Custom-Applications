using CommonCore.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumEngine.DriverInitialization.Browsers
{
    public class Chrome : IBrowserDriver
    {
        public virtual IWebDriver Initialize()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                WindowsProcessManager.KillProcess(TestInMemoryParameters.Instance.DriverType, "chrome");
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
                                       "--ignore-ssl-errors=yes",
                                       "--ignore-certificate-errors",
                                       "--disable-gpu");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver(chromeOptions);
        }
    }
}