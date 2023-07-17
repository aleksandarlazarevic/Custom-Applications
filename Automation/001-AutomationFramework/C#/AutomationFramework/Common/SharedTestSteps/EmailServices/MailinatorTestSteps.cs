using CommonCore.Tests;
using SeleniumEngine.DriverInitialization;
using SharedTestSteps.Contracts;
using UIMappings.Pages.EmailProviders;

namespace SharedTestSteps.EmailServices
{
    public class MailinatorTestSteps : IEmailServiceOperator
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
            TestInMemoryParameters.Instance.GeneratedEmailAddress = string.Format("{0}@{1}", EmailPrefix, "mailinator.com");
        }

        public void GetEmailAddress()
        {
            try
            {
                WebDriverFactory.WebDriver.GetPage<Mailinator>()
                                          .EnterEmail(EmailPrefix)
                                          .ClickOnGo()
                                          .ImplicitlyWaitForPageToBeReady<Mailinator>(2)
                                          .SaveGeneratedEmailAddress(TestInMemoryParameters.Instance.SetEmailFor);

                WebDriverFactory.TakeScreenshot("MAILINATOR - Get email address");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get generated email address from 'Mailinator'", ex);
            }
        }

        public void GetVerificationCodeFromEmail()
        {
            string verificationCodeText = string.Empty;

            WebDriverFactory.WebDriver.GetPage<Mailinator>()
                                      .EnterEmail(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                                      .ClickOnGo()
                                      .WaitForEmail(TestInMemoryParameters.Instance.VerificationSenderAddress)
                                      .ClickOnEmail(TestInMemoryParameters.Instance.VerificationSenderAddress)
                                      .GetVerificationCode(TestInMemoryParameters.Instance.VerificationCode, out verificationCodeText);

            TestInMemoryParameters.Instance.VerificationCode = verificationCodeText.Split(new string[] { ": " }, StringSplitOptions.None)[1].Substring(0, 6);

            WebDriverFactory.TakeScreenshot("MAILINATOR - Get verification code");
        }

        public void VerifyEmailNotificationContent()
        {
            WebDriverFactory.WebDriver.GetPage<Mailinator>()
                                      .EnterEmail(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                                      .ClickOnGo()
                                      .WaitForEmail(TestInMemoryParameters.Instance.EmailSender)
                                      .ClickOnEmail(TestInMemoryParameters.Instance.EmailSender)
                                      .ImplicitlyWaitForPageToBeReady<Mailinator>(3)
                                      .VerifyEmailBodyContent(TestInMemoryParameters.Instance.EmailText)
                                      .ImplicitlyWaitForPageToBeReady<Mailinator>(2);
        }

        public void DeleteAllEmailsFromInbox()
        {
            WebDriverFactory.WebDriver.GetPage<Mailinator>()
                                      .EnterEmail(TestInMemoryParameters.Instance.GeneratedEmailAddress)
                                      .ClickOnGo()
                                      .SelectAllEmailsFromInbox()
                                      .ClickOnDelete();
        }
    }
}