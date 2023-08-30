using Castle.Windsor;

namespace SeleniumCore.Containers
{
    public class ObjectInstantiator
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
                              new WebDriverContainer()
                        );
                }

                return _container;
            }
        }
    }
}