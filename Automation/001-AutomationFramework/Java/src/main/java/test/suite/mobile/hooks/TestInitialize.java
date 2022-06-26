using AppiumCore.Base;
using AppiumCore.Config;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using TechTalk.SpecFlow;

namespace TestSuite.Hooks
{
    [Binding]
    public class TestInitialize
    {
       [BeforeScenario]
        public void InitializeTest()
        {
            //Initialize Settings
            ConfigReader.InitializeSettings();

            DriverFactory.Instance.InitializeAppiumDriver<AppiumDriver<AppiumWebElement>>(MobileType.Hybrid);
        }

        [TearDown]
        public void CleanUp()
        {
            DriverFactory.Instance.CloseAppiumContext();
        }
    }
}
