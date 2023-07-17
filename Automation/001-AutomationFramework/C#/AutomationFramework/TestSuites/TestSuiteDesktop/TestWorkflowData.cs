using CommonCore;
using CommonCore.Configuration;
using CommonCore.Contracts;
using System.Reflection;
using TestSuiteDesktop.Hooks;
using WinAppDriverEngine;

namespace TestSuiteDesktop
{
    internal class TestWorkflowData : TestingWorkflowActions
    {
        protected override IEnumerable<Assembly> GetTestingComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[] { typeof(DesktopTestHook).Assembly };

            return assemblies;
        }

        protected override TestConfiguration GetTestingConfiguration()
        {
            return ConfigurationManager.ReadConfigurationFile("DesktopConfig.json");
        }

        protected override IEnumerable<IEngineManager> GetTestingEngines()
        {
            IEnumerable<IEngineManager> engines = new[] { new WinAppDriverManager() };

            return engines;
        }
    }
}
