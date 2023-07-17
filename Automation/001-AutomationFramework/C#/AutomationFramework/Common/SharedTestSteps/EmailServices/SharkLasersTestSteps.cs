using CommonCore.Tests;
using SeleniumEngine.DriverInitialization;
using SharedTestSteps.Contracts;
using UIMappings.Pages.EmailProviders;

namespace SharedTestSteps.EmailServices
{
    public class SharkLasersTestSteps : IEmailServiceOperator
    {
        private string EmailPrefix
        {
            get
            {
                return String.Format("franchiTest{0}", DateTime.UtcNow.ToString("yyMMddhmsff"));
            }
        }

        public void GenerateEmail()
        {
            TestInMemoryParameters.Instance.GeneratedEmailAddress = string.Format("{0}@{1}", EmailPrefix, "sharklasers.com");
        }

        public void GetEmailAddress()
        {
            try
            {
                WebDriverFactory.WebDriver.GetPage<SharkLasers>()
                                  .ClickOnScrambleAddress()
                                  .ClickOnEdit()
                                  .EnterEmailAddress(EmailPrefix)
                                  .ClickOnSet()
                                  .ImplicitlyWaitForPageToBeReady<SharkLasers>(5)
                                  .SaveGeneratedEmailAddress(TestInMemoryParameters.Instance.SetEmailFor);

                //UIDriver.TakeScreenshot("SHARKLASERS - Get email address");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get generated email address from 'Sharklasers'", ex);
            }
        }

        public void GetVerificationCodeFromEmail()
        {
            string verificationCodeText = string.Empty;

            WebDriverFactory.WebDriver.GetPage<SharkLasers>()
                              .ClickCommercialDownButton()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .WaitForEmailFromCertainSender(TestInMemoryParameters.Instance.VerificationSenderAddress)
                              .ClickOnEmailFromSpecificSender(TestInMemoryParameters.Instance.VerificationSenderAddress)
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(3)
                              .GetVerificationCode(TestInMemoryParameters.Instance.VerificationCodeText, out verificationCodeText);

            TestInMemoryParameters.Instance.VerificationCode = verificationCodeText.Split(new string[] { ": " }, StringSplitOptions.None)[1].Substring(0, 6);

            WebDriverFactory.TakeScreenshot("SHARKLASERS - Get verification code");
        }

        public void VerifyEmailNotificationContent()
        {
            WebDriverFactory.WebDriver.GetPage<SharkLasers>()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .WaitForEmailFromCertainSender(TestInMemoryParameters.Instance.EmailSender)
                              .ClickOnEmailFromSpecificSender(TestInMemoryParameters.Instance.EmailSender)
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(3)
                              .VerifyEmailBodyContent(TestInMemoryParameters.Instance.EmailText);
        }

        public void DeleteAllEmailsFromInbox()
        {
            WebDriverFactory.WebDriver.GetPage<SharkLasers>()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(5)
                              .SelectAllEmailsFromInbox()
                              .ClickOnDelete();
        }
    }
}