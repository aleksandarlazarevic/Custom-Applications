using AventStack.ExtentReports;
using BoDi;
using CommonCore.Tests;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumEngine.DriverInitialization;
using System.Drawing.Imaging;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Hooks
{
    [Binding]
    public sealed class WebTestHook
    {
        private static FeatureContext _featureContext;
        private readonly IObjectContainer _objectContainer;
        private static TestContext _testContext;
        private static ScenarioContext _scenarioContext;
        StepInfo? _stepInfo;

        public TestData TestManager { get; private set; }

        private static readonly TestWorkflowData _testWorkflowData = new();

        public WebTestHook(IObjectContainer objectContainer,FeatureContext featureContext, ScenarioContext scenarioContext, TestContext testContext)
        {
            _objectContainer = objectContainer;
            _featureContext = featureContext;
            _testContext = testContext;
            _scenarioContext = scenarioContext;
        }

        #region Test Run setup
        [BeforeTestRun]
        public static void InitializeTestRun()
        {
            _testWorkflowData.TestExecutionInitialization();
            //_objectContainer.RegisterInstanceAs<IWebDriver>(WebDriverFactory.WebDriver);
        }
        ////public class OrangeCRM_UISteps
        ////{

        ////    MyInfoPage _MyInfoPage;
        ////    CRMLoginPage _CRMLoginPage;
        ////    PIMPage _PIMPage;
        ////    TopNavBar _TopNavBar;
        ////    SideNavBar _SideNavBar;
        ////    ScenarioContext _ScenarioContext;

        ////    public OrangeCRM_UISteps(IObjectContainer objectContainer)
        ////    {
        ////        _MyInfoPage = objectContainer.Resolve<MyInfoPage>();
        ////        _CRMLoginPage = objectContainer.Resolve<CRMLoginPage>();
        ////        _PIMPage = objectContainer.Resolve<PIMPage>();
        ////        _TopNavBar = objectContainer.Resolve<TopNavBar>();
        ////        _SideNavBar = objectContainer.Resolve<SideNavBar>();
        ////        _ScenarioContext = objectContainer.Resolve<ScenarioContext>();
        ////    }
        [AfterTestRun]
        public static void TestRunCleanUp()
        {
            // Create graphic interpretation of results
        }
        #endregion

        #region Feature setup
        [BeforeFeature]
        public static void BeforeFeature()
        {
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            _testWorkflowData.TestFeatureFinalization();
        }
        #endregion

        #region Test scenario setup
        [BeforeScenario(Order = 0)]
        public void InitializeScenario()
        {
            var tt = FeatureContext.Current;
            var ttt = ScenarioContext.Current;

            //_featureContext = featureContext;
            //_testContext = testContext;
            //_scenarioContext = scenarioContext;
            _testWorkflowData.TestFeatureInitialization(_featureContext);

            _testWorkflowData.TestScenarioInitialization(_testContext, _featureContext, _scenarioContext);
            //IWebDriver? driver = _objectContainer.Resolve<IWebDriver>();

            //_objectContainer.RegisterInstanceAs(_driver);
            //_objectContainer.RegisterInstanceAs(_testData);
        }

        [AfterScenario]
        public void ScenarioCleanUp()
        {
            _testWorkflowData.TestScenarioFinalization(_testContext, _scenarioContext, _stepInfo);

            //if (scenarioContext.TestError != null)
            //{
            //    _driver.TakeScreenshot().SaveAsFile(Path.Combine("..", "..", "TestResults", $"{scenarioContext.ScenarioInfo.Title}.png"), ScreenshotImageFormat.Png);
            //}
            //_driver?.Dispose();

            //        string name = ScenarioContext.Current.ScenarioInfo.Title + ".jpeg";
            //        GenericHelper.TakeScreenShot(name);
            //        Console.WriteLine(ScenarioContext.Current.TestError.Message);
            //        Console.WriteLine(ScenarioContext.Current.TestError.StackTrace);
        }

        [BeforeScenario("web")]
        public void BeforeWebScenario()
        {
        }

        [AfterScenario("web")]
        public void AfterWebScenario()
        {
        }

        [BeforeScenarioBlock]
        public void BeforeScenarioBlock()
        {
            _testWorkflowData.TestScenarioBlockInitialization(_testContext, _scenarioContext);
        }

        [AfterScenarioBlock]
        public void AfterScenarioBlock()
        {
            _testWorkflowData.TestScenarioBlockFinalization(_testContext, _scenarioContext);
        }
        #endregion

        #region Test step setup
        [BeforeStep]
        public void BeforeStep()
        {
            _testWorkflowData.TestStepInitialization(_testContext, _scenarioContext);
        }

        [AfterStep]
        public void AfterStep()
        {
            _stepInfo = ScenarioStepContext.Current.StepInfo;

            _testWorkflowData.TestStepFinalization(_testContext, _scenarioContext);
        }
        #endregion
    }
}