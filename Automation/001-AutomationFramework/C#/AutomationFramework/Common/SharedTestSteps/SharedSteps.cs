using SharedTestSteps.Containers;
using SharedTestSteps.Contracts;

namespace SharedTestSteps
{
    public class SharedSteps
    {
        #region Fields and Properties
        private static SharedSteps _instance = null;
        private static object _locker = new object();
        public IEmailServiceOperator Mailinator { get; set; }
        public IEmailServiceOperator Sharklasers { get; set; }
        public IGoogleTestSteps Google { get; set; }

        public ICommonBrowserActions CommonBrowserActions { get; set; }
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
            CommonBrowserActions = SharedContainer.Container.Resolve<ICommonBrowserActions>();

            #region Online email services
            Mailinator = SharedContainer.Container.Resolve<IEmailServiceOperator>("MailinatorComponentInstance");
            Sharklasers = SharedContainer.Container.Resolve<IEmailServiceOperator>("SharklasersComponentInstance");
            #endregion
            Google = SharedContainer.Container.Resolve<IGoogleTestSteps>("GoogleComponentInstance");

        }
    }
}
