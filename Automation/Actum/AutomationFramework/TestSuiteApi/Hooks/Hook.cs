using TechTalk.SpecFlow;

namespace TestSuiteApi.Hooks
{
    [Binding]
    public class Hook
    {
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
