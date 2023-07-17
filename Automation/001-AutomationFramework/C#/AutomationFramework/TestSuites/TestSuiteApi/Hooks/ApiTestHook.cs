using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TestSuiteApi.Hooks
{
    [Binding]
    public class ApiTestHook
    {
        private static FeatureContext _featureContext;
        private readonly IObjectContainer _objectContainer;
        private readonly TestContext _testContext;
        private static readonly TestWorkflowData _testWorkflowData = new();

        public ApiTestHook(TestContext testContext)
        {
            _testContext = testContext;
        }

        [BeforeTestRun]
        public static void InitializeTestRun()
        {
        }

        [AfterTestRun]
        public static void TestRunCleanUp()
        {
        }

        [BeforeScenario]
        public static void InitializeScenario()
        {
        }

        [AfterScenario]
        public static void ScenarioCleanUp()
        {
        }
    }
}
