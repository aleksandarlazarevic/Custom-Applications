using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeleniumEngine.DriverInitialization;
using SeleniumEngine.DriverInitialization.Browsers;

namespace SeleniumEngine.Instantiators
{
    public class WebDriverInstantiator : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
             Component.For<IBrowserDriver>().ImplementedBy<Chrome>().LifestyleTransient().Named("Chrome"),
             Component.For<IBrowserDriver>().ImplementedBy<Edge>().LifestyleTransient().Named("Edge"),
             Component.For<IBrowserDriver>().ImplementedBy<Firefox>().LifestyleTransient().Named("Firefox"));
        }
    }
}