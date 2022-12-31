using CommonTestSteps.Containers;
using CommonTestSteps.Contracts;

namespace CommonTestSteps
{
    public class SharedSteps
    {
        #region Fields and Properties
        private static SharedSteps _instance = null;
        private static object _locker = new object();
        public IGlobalTestSteps Global { get; set; }

        public ICartTestSteps Cart { get; set; }
        public ICategoriesTestSteps Categories { get; set; }
        public ILoginTestSteps Login { get; set; }
        public ISignUpTestSteps SignUp { get; set; }

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
            Cart = SharedContainer.Container.Resolve<ICartTestSteps>("CartComponentInstance");
            Categories = SharedContainer.Container.Resolve<ICategoriesTestSteps>("CategoriesComponentInstance");
            Login = SharedContainer.Container.Resolve<ILoginTestSteps>("LoginComponentInstance");
            SignUp = SharedContainer.Container.Resolve<ISignUpTestSteps>("SignUpComponentInstance");

            Global = SharedContainer.Container.Resolve<IGlobalTestSteps>();
        }
    }
}
