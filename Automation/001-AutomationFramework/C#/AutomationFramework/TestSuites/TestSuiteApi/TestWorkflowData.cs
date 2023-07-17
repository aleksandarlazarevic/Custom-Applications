using CommonCore;
using CommonCore.Configuration;
using CommonCore.Contracts;
using System.Reflection;
using APITestingEngine;
using TestSuiteApi.Hooks;

namespace TestSuiteApi
{
    internal class TestWorkflowData : TestingWorkflowActions
    {
        protected override IEnumerable<Assembly> GetTestingComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[] { typeof(ApiTestHook).Assembly };

            return assemblies;
        }

        protected override TestConfiguration GetTestingConfiguration()
        {
            return ConfigurationManager.ReadConfigurationFile("ApiConfig.json");
        }

        protected override IEnumerable<IEngineManager> GetTestingEngines()
        {
            IEnumerable<IEngineManager> engines = new[] { new ApiTestManager() };

            return engines;
        }
    }
}
