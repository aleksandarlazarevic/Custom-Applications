using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonTestSteps.Contracts;
using CommonTestSteps.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestSteps.Containers.Resolvers
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
