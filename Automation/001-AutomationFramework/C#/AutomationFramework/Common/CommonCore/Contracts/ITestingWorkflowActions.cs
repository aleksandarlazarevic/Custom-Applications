using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CommonCore.Contracts
{
    public interface ITestingWorkflowActions
    {
        public void TestExecutionInitialization();
        public void TestExecutionFinalization();
        public void TestFeatureInitialization(FeatureContext featureContext);
        public void TestFeatureFinalization();
        public void TestScenarioInitialization(TestContext testContext, FeatureContext featureContext, ScenarioContext scenarioContext);
        public void TestScenarioFinalization(TestContext testContext, ScenarioContext scenarioContext, StepInfo? stepInfo);
        public void TestScenarioBlockInitialization(TestContext testContext, ScenarioContext scenarioContext);
        public void TestScenarioBlockFinalization(TestContext testContext, ScenarioContext scenarioContext);
        public void TestStepInitialization(TestContext testContext, ScenarioContext scenarioContext);
        public void TestStepFinalization(TestContext testContext, ScenarioContext scenarioContext);
    }
}
