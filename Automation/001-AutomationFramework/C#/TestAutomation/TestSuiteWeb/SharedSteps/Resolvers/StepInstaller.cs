using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestSuiteWeb.SharedSteps.Contracts;
using TestSuiteWeb.Steps;

namespace TestSuiteWeb.SharedSteps.Resolvers
{
    public class StepInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                      Component.For<ILoginSteps>().ImplementedBy<LoginSteps>().LifestyleTransient());
        }
    }
}
