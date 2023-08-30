using CommonTestSteps.Contracts;
using SeleniumCore.WebDriver;
using System;
using System.Web;
using UIMappings.Pages.EmailProviders;
using Utilities;
using Utilities.EmailServices;

namespace CommonTestSteps.TestSteps.MailServices
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
            TestInMemoryParametersShared.Instance.GeneratedEmailAddress = string.Format("{0}@{1}", EmailPrefix, "mailinator.com");
        }

        public void GetEmailAddress()
        {
            try
            {
                UIDriver.WebDriver.GetPage<Mailinator>()
                                  .EnterEmail(EmailPrefix)
                                  .ClickOnGo()
                                  .ImplicitlyWaitForPageToBeReady<Mailinator>(2)
                                  .SaveGeneratedEmailAddress(TestInMemoryParametersShared.Instance.SetEmailFor);

                UIDriver.TakeScreenshot("MAILINATOR - Get email address");
            }
            catch (Exception ex)
            {
                throw new HttpException("Unable to get generated email address from 'Mailinator'", ex);
            }
        }

        public void GetVerificationCodeFromEmail()
        {
            string verificationCodeText = string.Empty;

            UIDriver.WebDriver.GetPage<Mailinator>()
                              .EnterEmail(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnGo()
                              .WaitForEmail(TestInMemoryParametersShared.Instance.VerificationSenderAddress)
                              .ClickOnEmail(TestInMemoryParametersShared.Instance.VerificationSenderAddress)
                              .GetVerificationCode(TestInMemoryParametersShared.Instance.VerificationCode, out verificationCodeText);

            TestInMemoryParametersShared.Instance.VerificationCode = verificationCodeText.Split(new string[] { ": " }, StringSplitOptions.None)[1].Substring(0, 6);

            UIDriver.TakeScreenshot("MAILINATOR - Get verification code");
        }

        public void VerifyEmailNotificationContent()
        {
            UIDriver.WebDriver.GetPage<Mailinator>()
                              .EnterEmail(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnGo()
                              .WaitForEmail(TestInMemoryParametersShared.Instance.EmailSender)
                              .ClickOnEmail(TestInMemoryParametersShared.Instance.EmailSender)
                              .ImplicitlyWaitForPageToBeReady<Mailinator>(3)
                              .VerifyEmailBodyContent(TestInMemoryParametersShared.Instance.EmailText)
                              .ImplicitlyWaitForPageToBeReady<Mailinator>(2);
        }

        public void DeleteAllEmailsFromInbox()
        {
            UIDriver.WebDriver.GetPage<Mailinator>()
                              .EnterEmail(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnGo()
                              .SelectAllEmailsFromInbox()
                              .ClickOnDelete();
        }
    }
}
