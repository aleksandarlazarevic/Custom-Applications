using Castle.Windsor;
using CommonTestSteps.Containers.Resolvers;

namespace CommonTestSteps.Containers
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
                    _container.Install(new StepsInstaller(),
                                       new OperatorsInstaller());
                }

                return _container;
            }
        }
    }
}
