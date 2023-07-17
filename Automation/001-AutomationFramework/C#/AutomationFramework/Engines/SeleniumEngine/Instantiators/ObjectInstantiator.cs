using Castle.Windsor;

namespace SeleniumEngine.Instantiators
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
                              new WebDriverInstantiator()
                        );
                }

                return _container;
            }
        }
    }
}