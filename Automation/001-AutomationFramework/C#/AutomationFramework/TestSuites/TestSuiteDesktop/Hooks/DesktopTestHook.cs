using BoDi;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;

namespace TestSuiteDesktop.Hooks
{
    [Binding]
    public class DesktopTestHook
    {
        private static FeatureContext _featureContext;
        private readonly IObjectContainer _objectContainer;
        private readonly TestContext _testContext;
        private static readonly TestWorkflowData _testWorkflowData = new();

        public DesktopTestHook(TestContext testContext)
        {
            _testContext = testContext;
        }
    }
}
