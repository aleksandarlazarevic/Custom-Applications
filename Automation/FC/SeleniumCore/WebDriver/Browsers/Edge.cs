using OpenQA.Selenium;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Handlers;
using System;
using System.IO;
using OpenQA.Selenium.Edge;

namespace SeleniumCore.WebDriver.Browsers
{
    public class Edge : IDriver
    {
        public virtual IWebDriver Initialize()
        {
            EdgeOptions edgeOptions = new EdgeOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                Processes.KillProcess(TestInMemoryParameters.Instance.WebDriver, "msedge");
            }

            string driverLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BrowserDrivers\\Edge\\";
            EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService(driverLocation);
            edgeDriverService.UseVerboseLogging = true;

            edgeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            edgeOptions.AddAdditionalCapability("download.prompt_for_download", true);
            edgeOptions.AddAdditionalCapability("download.default_directory", Directory.GetCurrentDirectory());
            edgeOptions.AddAdditionalCapability("profile.default_content_setting_values.automatic_downloads", 1);

            edgeOptions.AddAdditionalCapability("--disable-popup-blocking", true);
            edgeOptions.AddAdditionalCapability("--disable-extensions", true);
            edgeOptions.AddAdditionalCapability("--start-maximized", true);
            edgeOptions.AddAdditionalCapability("--safebrowsing-disable-download-protection", true);
            edgeOptions.AddAdditionalCapability("--no-sandbox", true);
            edgeOptions.AddAdditionalCapability("--disable-dev-shm-usage", true);
            edgeOptions.AddAdditionalCapability("--disable-gpu", true);
            edgeOptions.AddAdditionalCapability("--whitelisted-ips", "");

            return new EdgeDriver(edgeDriverService, edgeOptions, new TimeSpan(0, 6, 0));
        }

    }
}
