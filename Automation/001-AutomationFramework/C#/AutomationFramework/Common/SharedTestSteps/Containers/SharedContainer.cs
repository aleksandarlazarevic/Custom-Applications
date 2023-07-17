using Castle.Windsor;
using SharedTestSteps.Containers.Resolvers;

namespace SharedTestSteps.Containers
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
