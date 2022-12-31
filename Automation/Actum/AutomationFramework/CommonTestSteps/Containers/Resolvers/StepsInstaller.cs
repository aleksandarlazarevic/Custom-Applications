using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonTestSteps.Contracts;
using CommonTestSteps.TestSteps;

namespace CommonTestSteps.Containers.Resolvers
{
    public class StepsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

            #region Login
                Component.For<ILoginTestSteps>().ImplementedBy<LoginTestSteps>().LifestyleTransient().Named("LoginComponentInstance"),
            #endregion
            #region Sign Up
                Component.For<ISignUpTestSteps>().ImplementedBy<SignUpTestSteps>().LifestyleTransient().Named("SignUpComponentInstance"),
            #endregion
            #region Cart
                Component.For<ICartTestSteps>().ImplementedBy<CartTestSteps>().LifestyleTransient().Named("CartComponentInstance"),
            #endregion
            #region Categories
                Component.For<ICategoriesTestSteps>().ImplementedBy<CategoriesTestSteps>().LifestyleTransient().Named("CategoriesComponentInstance"),
            #endregion

                Component.For<IGlobalTestSteps>().ImplementedBy<GlobalTestSteps>().LifestyleTransient()
                );
        }

    }
}
