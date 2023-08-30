using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.WebDriver.Browsers;

namespace SeleniumCore.Containers
{
    public class WebDriverContainer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
             Component.For<IDriver>().ImplementedBy<Chrome>().LifestyleTransient().Named("Chrome"),
             Component.For<IDriver>().ImplementedBy<Edge>().LifestyleTransient().Named("Edge"));
        }
    }
}