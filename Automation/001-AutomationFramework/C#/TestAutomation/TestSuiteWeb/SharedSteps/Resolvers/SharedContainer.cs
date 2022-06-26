using Castle.Windsor;

namespace TestSuiteWeb.SharedSteps.Resolvers
{
    public class SharedContainer
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new WindsorContainer();
                    _container.Install(
                              new StepInstaller()
                        );
                }

                return _container;
            }
        }
    }
}
