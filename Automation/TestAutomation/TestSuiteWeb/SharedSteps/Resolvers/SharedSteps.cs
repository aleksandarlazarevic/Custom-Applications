using SeleniumCore.Containers;
using TestSuiteWeb.SharedSteps.Contracts;

namespace TestSuiteWeb.SharedSteps.Resolvers
{
    public class SharedSteps
    {
        #region Fields
        private static SharedSteps _instance = null;
        private static object _locker = new object();
        #endregion

        #region == Login Page  steps ==
        public ILoginSteps LoginSteps { get; set; }
        #endregion

        public static SharedSteps Containers
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null)
                        _instance = new SharedSteps();

                    return _instance;
                }
            }
        }

        public SharedSteps()
        {
            #region == Login Page ==
            LoginSteps = ObjectInstantiator.Container.Resolve<ILoginSteps>();
            #endregion
        }
    }
}
