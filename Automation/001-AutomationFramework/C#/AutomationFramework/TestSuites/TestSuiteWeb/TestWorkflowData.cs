using CommonCore;
using CommonCore.Configuration;
using CommonCore.Contracts;
using NUnit.Framework;
using SeleniumEngine;
using System.Reflection;
using TestSuiteWeb.Hooks;

namespace TestSuiteWeb
{
    internal class TestWorkflowData : TestingWorkflowActions
    {
        protected override IEnumerable<Assembly> GetTestingComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[] { typeof(WebTestHook).Assembly };

            return assemblies;
        }

        protected override TestConfiguration GetTestingConfiguration()
        {
            return ConfigurationManager.ReadConfigurationFile("WebConfig.json");
        }

        protected override IEnumerable<IEngineManager> GetTestingEngines()
        {
            IEnumerable<IEngineManager> engines = new[] { new SeleniumManager() };

            return engines;
        }
    }
}
