using AppiumCore.Base;
using AppiumCore.Config;
using CommonTestSteps.TestSteps.Mobile;
using OpenQA.Selenium.Appium;
using System;
using TechTalk.SpecFlow;
using Utilities;

namespace TestSuiteMobile.Hooks
{
    [Binding]
    public class TestInitialization
    {
        [BeforeTestRun]
        public static void InitializeTestRun()
        {

        }

        [AfterTestRun]
        public static void TestRunCleanUp()
        {
            DriverFactory.Instance.CloseAppiumContext();
        }

        [BeforeScenario]
        public static void InitializeScenario()
        {
            ConfigReader.InitializeSettings();
            // Valhallan
            GlobalMobileTestSteps.UploadLatestAppVersionFromAppCenter("Iron_24", "UAT", "Android");
            DriverFactory.Instance.InitializeAppiumDriver<AppiumDriver<AppiumWebElement>>(MobileType.Native);
            TestInMemoryParametersShared.Instance.TestIdentifierStartTime = DateTime.Now;
        }

        [AfterScenario]
        public static void ScenarioCleanUp()
        {
            DriverFactory.Instance.CloseAppiumContext();
        }
    }
}