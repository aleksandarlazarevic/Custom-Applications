using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharedTestSteps.Contracts;
using SharedTestSteps.EmailServices;

namespace SharedTestSteps.Containers.Resolvers
{
    public class OperatorsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Component.For<IEmailService>().ImplementedBy<EmailServiceTestSteps>().LifestyleTransient().Named("EmailService")
                );
        }
    }
}
