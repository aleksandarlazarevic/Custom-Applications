using AppiumEngine;
using CommonCore;
using CommonCore.Configuration;
using CommonCore.Contracts;
using System.Reflection;
using TestSuiteMobile.Hooks;

namespace TestSuiteMobile
{
    internal class TestWorkflowData : TestingWorkflowActions
    {
        protected override IEnumerable<Assembly> GetTestingComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[] { typeof(MobileTestHook).Assembly };

            return assemblies;
        }

        protected override TestConfiguration GetTestingConfiguration()
        {
            return ConfigurationManager.ReadConfigurationFile("MobileConfig.json");
        }

        protected override IEnumerable<IEngineManager> GetTestingEngines()
        {
            IEnumerable<IEngineManager> engines = new[] { new AppiumManager() };

            return engines;
        }
    }
}
