using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonTestSteps.Contracts;
using CommonTestSteps.Contracts.FcosAzure.Automation;
using CommonTestSteps.Operators;
using CommonTestSteps.TestSteps;
using CommonTestSteps.TestSteps.Fcos;
using CommonTestSteps.TestSteps.FcosAzure;
using CommonTestSteps.TestSteps.FcosAzure.Automation;
using CommonTestSteps.TestSteps.FranchiCzar;
using CommonTestSteps.TestSteps.Iron24;
using CommonTestSteps.TestSteps.MailServices;
using CommonTestSteps.TestSteps.MathReactor;
using CommonTestSteps.TestSteps.Nael;
using CommonTestSteps.TestSteps.Valhallan;

namespace CommonTestSteps.Containers.Resolvers
{
    public class StepsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEmailServiceOperator>().ImplementedBy<MailinatorTestSteps>().LifestyleTransient().Named("MailinatorComponentInstance"),
                Component.For<IEmailServiceOperator>().ImplementedBy<SharkLasersTestSteps>().LifestyleTransient().Named("SharklasersComponentInstance"),

                Component.For<IFcosAzureTestSteps>().ImplementedBy<FcosAzureTestSteps>().LifestyleTransient().Named("FcosAzureComponentInstance"),
                Component.For<IFcosTestSteps>().ImplementedBy<FcosTestSteps>().LifestyleTransient().Named("FcosComponentInstance"),
                Component.For<IFranchiCzarTestSteps>().ImplementedBy<FranchiCzarTestSteps>().LifestyleTransient().Named("FranchiCzarComponentInstance"),
                Component.For<IIron24TestSteps>().ImplementedBy<Iron24TestSteps>().LifestyleTransient().Named("Iron24ComponentInstance"),
                Component.For<IMathReactorTestSteps>().ImplementedBy<MathReactorTestSteps>().LifestyleTransient().Named("MathReactorComponentInstance"),
                Component.For<INaelTestSteps>().ImplementedBy<NaelTestSteps>().LifestyleTransient().Named("NaelComponentInstance"),
                Component.For<IValhallanTestSteps>().ImplementedBy<ValhallanTestSteps>().LifestyleTransient().Named("ValhallanComponentInstance"),

                #region FcosAzure Modules
                Component.For<IAutomationTestSteps>().ImplementedBy<AutomationTestSteps>().LifestyleTransient().Named("AutomationTestStepsComponentInstance"),
                #endregion

                Component.For<IGlobalTestSteps>().ImplementedBy<GlobalTestSteps>().LifestyleTransient()
                );
        }

    }
}
