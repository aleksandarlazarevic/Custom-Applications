using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharedTestSteps.Contracts;
using SharedTestSteps.EmailServices;

namespace SharedTestSteps.Containers.Resolvers
{
    public class StepsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommonBrowserActions>().ImplementedBy<CommonBrowserActions>().LifestyleTransient(),

                Component.For<IEmailServiceOperator>().ImplementedBy<MailinatorTestSteps>().LifestyleTransient().Named("MailinatorComponentInstance"),
                Component.For<IEmailServiceOperator>().ImplementedBy<SharkLasersTestSteps>().LifestyleTransient().Named("SharklasersComponentInstance"),

                Component.For<IGoogleTestSteps>().ImplementedBy<GoogleTestSteps>().LifestyleTransient().Named("GoogleComponentInstance")
                );
        }
    }
}
