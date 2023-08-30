using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Base;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Hooks
{
    [Binding]
    public class TestInitialization : BaseTest
    {
        private static TestInitialization initializator;

        [BeforeTestRun]
        public static void InitializeTestRun(TestContext testContext)
        {
            initializator = new TestInitialization();
            initializator.InitializeTestPrerequisites(testContext);
        }

        [AfterTestRun]
        public static void TestRunCleanUp()
        {
            initializator.TestCleanUp();
        }

        [BeforeScenario]
        public static void InitializeScenario()
        {
        }

        [AfterScenario]
        public static void ScenarioCleanUp()
        {
        }

        public void InitializeTestPrerequisites(TestContext testContext)
        {
            TestInitialize(testContext);
        }
    }
}
