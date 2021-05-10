using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebActions.Browsers.Chrome
{
    public sealed class ChromeDriverData
    {
        private static ChromeDriverData instance = null;
        private static readonly object padlock = new object();

        public ChromeDriverData()
        {
        }

        public static ChromeDriverData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ChromeDriverData();
                    }
                    return instance;
                }
            }
        }

        public static string ChromeDriverLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\WebsiteAutomation\WebActions\WebDrivers\Chrome\90.0.4430.24");

        public static IWebDriver GetChromeInstanceWithUserProfile()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("test-type");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("no-sandbox");
            options.AddArgument("disable-infobars");
            options.AddArgument("--headless"); //hide browser
            options.AddArgument("--start-maximized");
            //options.AddArgument("--window-size=1100,300");
            //options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            // Profile [Change:User name]
            options.AddArgument(@"user-data-dir=C:\Users\User\AppData\Local\Google\Chrome\User Data");
            IWebDriver driver = new ChromeDriver(ChromeDriverLocation, options);
            return driver;
        }

        public IWebDriver GetNewChromeInstance()
        {
            IWebDriver driver = new ChromeDriver(ChromeDriverLocation);
            return driver;
        }
    }
}
