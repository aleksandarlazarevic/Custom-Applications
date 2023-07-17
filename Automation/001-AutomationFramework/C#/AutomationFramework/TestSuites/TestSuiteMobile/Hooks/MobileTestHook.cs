using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TestSuiteMobile.Hooks
{
    [Binding]
    public class MobileTestHook
    {
        private static FeatureContext _featureContext;
        private readonly IObjectContainer _objectContainer;
        private readonly TestContext _testContext;
        private static readonly TestWorkflowData _testWorkflowData = new();

        public MobileTestHook(TestContext testContext)
        {
            _testContext = testContext;
        }
    }
}
