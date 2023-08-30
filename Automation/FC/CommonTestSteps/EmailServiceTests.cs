using SeleniumCore;
using Utilities;

namespace CommonTestSteps
{
    public class EmailServiceTests
    {
        public static void ObtainRandomEmailAddress()
        {
            TestInMemoryParameters.Instance.ElementTimeout = "45";
            TestInMemoryParameters.Instance.PageLoadTimeout = "180";
            SharedSteps.Containers.EmailService.CheckAvailability(TestInMemoryParametersShared.Instance.EmailServices);
            SharedSteps.Containers.EmailService.Initialize();
            SharedSteps.Containers.Global.GoToMailService();
            SharedSteps.Containers.EmailService.GetEmailAddress();
            SharedSteps.Containers.Global.CloseBrowser();
        }

        public static void GetVerificationCode()
        {
            TestInMemoryParametersShared.Instance.VerificationSenderAddress = "msonlineservicesteam@microsoftonline.com";
            TestInMemoryParametersShared.Instance.VerificationCodeText = "Your code is:";
            SharedSteps.Containers.Global.GoToMailServiceByName();
            SharedSteps.Containers.EmailService.GetVerificationCodeFromEmail();
            SharedSteps.Containers.Global.CloseBrowser();
        }
    }
}
