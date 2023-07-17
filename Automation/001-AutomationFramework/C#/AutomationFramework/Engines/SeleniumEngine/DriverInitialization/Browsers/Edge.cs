using CommonCore.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Utilities;

namespace SeleniumEngine.DriverInitialization.Browsers
{
    public class Edge : IBrowserDriver
    {
        public virtual IWebDriver Initialize()
        {
            EdgeOptions edgeOptions = new EdgeOptions();

            if (!TestInMemoryParameters.Instance.MultipleBrowserInstances)
            {
                WindowsProcessManager.KillProcess(TestInMemoryParameters.Instance.DriverType, "msedge");
            }

            string driverLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BrowserDrivers\\Edge\\";
            EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService(driverLocation);
            edgeDriverService.UseVerboseLogging = true;

            edgeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            edgeOptions.AddAdditionalOption("download.prompt_for_download", true);
            edgeOptions.AddAdditionalOption("download.default_directory", Directory.GetCurrentDirectory());
            edgeOptions.AddAdditionalOption("profile.default_content_setting_values.automatic_downloads", 1);

            edgeOptions.AddAdditionalOption("--disable-popup-blocking", true);
            edgeOptions.AddAdditionalOption("--disable-extensions", true);
            edgeOptions.AddAdditionalOption("--start-maximized", true);
            edgeOptions.AddAdditionalOption("--safebrowsing-disable-download-protection", true);
            edgeOptions.AddAdditionalOption("--no-sandbox", true);
            edgeOptions.AddAdditionalOption("--disable-dev-shm-usage", true);
            edgeOptions.AddAdditionalOption("--disable-gpu", true);
            edgeOptions.AddAdditionalOption("--whitelisted-ips", "");

            return new EdgeDriver(edgeDriverService, edgeOptions, new TimeSpan(0, 6, 0));
        }
    }
}