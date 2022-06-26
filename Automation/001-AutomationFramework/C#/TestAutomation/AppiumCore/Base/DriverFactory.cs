using AppiumCore.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Support.UI;
using System;

namespace AppiumCore.Base
{
    public enum MobileType
    {
        Native,
        Hybrid
    }

    public enum PlatformName
    {
        Android,
        iOS
    }

    public class DriverFactory
    {
        private AppiumLocalService _appiumLocalService;
        private static Lazy<DriverFactory> _instance = new Lazy<DriverFactory>(() => new DriverFactory());

        //Lazy instance access
        public static DriverFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private DriverFactory() { }

        public AppiumDriver<AppiumWebElement> AppiumDriver { get; set; }

        public void InitializeAppiumDriver<T>(MobileType mobileType) where T : AppiumDriver<AppiumWebElement>
        {
            var driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, Settings.PlatformName);
            //driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, Settings.DeviceName);
            //driverOptions.AddAdditionalCapability(MobileCapabilityType.App, Settings.AUTPath);
            //driverOptions.AddAdditionalCapability("chromedriverExecutable", Settings.ChromeDriverPath);
            driverOptions.AddAdditionalCapability("appPackage", "com.zuvvii.app");
            driverOptions.AddAdditionalCapability("appActivity", "crc643075d4721943448d.MainActivity");
            driverOptions.AddAdditionalCapability("noReset", "true");
            driverOptions.AddAdditionalCapability("automationName", "UiAutomator2");

            var builder = StartAppiumLocalService();

            AppiumDriver = new AndroidDriver<AppiumWebElement>(builder, driverOptions);

            if (mobileType == MobileType.Hybrid)
            {
                var contexts = ((IContextAware)AppiumDriver).Contexts;
                string webviewContext = null;

                for (var i = 0; i < contexts.Count; i++)
                {
                    Console.WriteLine(contexts[i]);
                    if (contexts[i].Contains("WEBVIEW"))
                    {
                        webviewContext = contexts[i];
                        break;
                    }
                }

                ((IContextAware)AppiumDriver).Context = webviewContext;
            }
        }

        private AppiumLocalService StartAppiumLocalService()
        {
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            if (!_appiumLocalService.IsRunning)
                _appiumLocalService.Start();

            return _appiumLocalService;
        }

        private AppiumLocalService StartAppiumLocalService(int portNumber)
        {
            _appiumLocalService = new AppiumServiceBuilder().UsingPort(portNumber).Build();
            if (!_appiumLocalService.IsRunning)
                _appiumLocalService.Start();

            return _appiumLocalService;
        }

        public void CloseAppiumContext()
        {
            AppiumDriver.CloseApp();
        }

        public void WaitForElement(AppiumWebElement appiumWebElement)
        {
            DefaultWait<AppiumDriver<AppiumWebElement>> fluentWait = new DefaultWait<AppiumDriver<AppiumWebElement>>(AppiumDriver);
            fluentWait.Timeout = TimeSpan.FromSeconds(15);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Until(x => appiumWebElement.Displayed);
        }
    }
}
