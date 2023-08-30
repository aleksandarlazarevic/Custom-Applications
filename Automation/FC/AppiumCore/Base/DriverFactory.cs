using System;
using AppiumCore.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Support.UI;

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
        private static Lazy<DriverFactory> _instance = new Lazy<DriverFactory>(() => new DriverFactory());

        public static DriverFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private DriverFactory() { }

        public AndroidDriver<AppiumWebElement> AppiumDriver { get; set; }

        public void InitializeAppiumDriver<T>(MobileType mobileType) where T : AppiumDriver<AppiumWebElement>
        {
            AppiumOptions appiumOptions = new AppiumOptions();

            // Set URL of the application under test
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, Settings.BrowserStackAppIdentifier);

            // BrowserStack access credentials
            appiumOptions.AddAdditionalCapability("browserstack.user", Settings.BrowserStackUser);
            appiumOptions.AddAdditionalCapability("browserstack.key", Settings.BrowserStackKey);
            //appiumOptions.AddAdditionalCapability("browserstack.idleTimeout", 35);

            // Other BrowserStack capabilities
            appiumOptions.AddAdditionalCapability("project", "FranchiCzar");
            appiumOptions.AddAdditionalCapability("build", "1.5.8");
            appiumOptions.AddAdditionalCapability("name", "Sanity testing");

            // Device specifications
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, Settings.DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, Settings.PlatformName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Settings.PlatformVersion);

            AppiumDriver = new AndroidDriver<AppiumWebElement>(new Uri("http://hub-cloud.browserstack.com/wd/hub"), appiumOptions);

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

        public void CloseAppiumContext()
        {
            AppiumDriver.CloseApp();
            AppiumDriver.Quit();
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