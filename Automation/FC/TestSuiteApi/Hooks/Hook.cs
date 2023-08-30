using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using Utilities;

namespace TestSuiteApi.Hooks
{
    [Binding]
    public class Hook
    {
        [BeforeTestRun]
        public static void InitializeTestRun()
        {
            GlobalUtilities.GetApiToken();
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