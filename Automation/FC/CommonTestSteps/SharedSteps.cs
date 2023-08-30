using CommonTestSteps.Containers;
using CommonTestSteps.Contracts;
using CommonTestSteps.Contracts.FcosAzure.Automation;

namespace CommonTestSteps
{
    public class SharedSteps
    {
        #region Fields and Properties
        private static SharedSteps _instance = null;
        private static object _locker = new object();
        public IEmailService EmailService { get; set; }
        public IEmailServiceOperator Mailinator { get; set; }
        public IEmailServiceOperator Sharklasers { get; set; }
        public IGlobalTestSteps Global { get; set; }

        public IFcosAzureTestSteps FcosAzure { get; set; }
        #region FcosAzure Modules
        public IAutomationTestSteps AutomationTestSteps { get; set; }

        #endregion
        public IFcosTestSteps Fcos { get; set; }
        public IFranchiCzarTestSteps FranchiCzar { get; set; }
        public IIron24TestSteps Iron24 { get; set; }
        public IMathReactorTestSteps MathReactor { get; set; }
        public INaelTestSteps Nael { get; set; }
        public IValhallanTestSteps Valhallan { get; set; }

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
            EmailService = SharedContainer.Container.Resolve<IEmailService>("EmailService");            
            Mailinator = SharedContainer.Container.Resolve<IEmailServiceOperator>("MailinatorComponentInstance");
            Sharklasers = SharedContainer.Container.Resolve<IEmailServiceOperator>("SharklasersComponentInstance");

            FcosAzure = SharedContainer.Container.Resolve<IFcosAzureTestSteps>("FcosAzureComponentInstance");
            Fcos = SharedContainer.Container.Resolve<IFcosTestSteps>("FcosComponentInstance");
            FranchiCzar = SharedContainer.Container.Resolve<IFranchiCzarTestSteps>("FranchiCzarComponentInstance");
            Iron24 = SharedContainer.Container.Resolve<IIron24TestSteps>("Iron24ComponentInstance");
            MathReactor = SharedContainer.Container.Resolve<IMathReactorTestSteps>("MathReactorComponentInstance");
            Nael = SharedContainer.Container.Resolve<INaelTestSteps>("NaelComponentInstance");
            Valhallan = SharedContainer.Container.Resolve<IValhallanTestSteps>("ValhallanComponentInstance");

            #region FcosAzure Modules
            AutomationTestSteps = SharedContainer.Container.Resolve<IAutomationTestSteps>("AutomationTestStepsComponentInstance");

            #endregion

            Global = SharedContainer.Container.Resolve<IGlobalTestSteps>();
        }
    }
}
