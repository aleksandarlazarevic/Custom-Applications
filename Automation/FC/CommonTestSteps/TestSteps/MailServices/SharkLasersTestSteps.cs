using CommonTestSteps.Contracts;
using SeleniumCore.WebDriver;
using System;
using UIMappings.Pages.EmailProviders;
using Utilities;
using Utilities.EmailServices;

namespace CommonTestSteps.TestSteps.MailServices
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
            TestInMemoryParametersShared.Instance.GeneratedEmailAddress = string.Format("{0}@{1}", EmailPrefix, "sharklasers.com");
        }

        public void GetEmailAddress()
        {
            try
            {
                UIDriver.WebDriver.GetPage<SharkLasers>()
                                  .ClickOnScrambleAddress()
                                  .ClickOnEdit()
                                  .EnterEmailAddress(EmailPrefix)
                                  .ClickOnSet()
                                  .ImplicitlyWaitForPageToBeReady<SharkLasers>(5)
                                  .SaveGeneratedEmailAddress(TestInMemoryParametersShared.Instance.SetEmailFor);

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

            UIDriver.WebDriver.GetPage<SharkLasers>()
                              .ClickCommercialDownButton()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .WaitForEmailFromCertainSender(TestInMemoryParametersShared.Instance.VerificationSenderAddress)
                              .ClickOnEmailBySenderAddress(TestInMemoryParametersShared.Instance.VerificationSenderAddress)
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(3)
                              .GetVerificationCode(TestInMemoryParametersShared.Instance.VerificationCodeText, out verificationCodeText);

            TestInMemoryParametersShared.Instance.VerificationCode = verificationCodeText.Split(new string[] { ": " }, StringSplitOptions.None)[1].Substring(0, 6);

            UIDriver.TakeScreenshot("SHARKLASERS - Get verification code");
        }

        public void VerifyEmailNotificationContent()
        {
            UIDriver.WebDriver.GetPage<SharkLasers>()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .WaitForEmailFromCertainSender(TestInMemoryParametersShared.Instance.EmailSender)
                              .ClickOnEmailBySenderAddress(TestInMemoryParametersShared.Instance.EmailSender)
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(3)
                              .VerifyEmailBodyContent(TestInMemoryParametersShared.Instance.EmailText);
        }

        public void DeleteAllEmailsFromInbox()
        {
            UIDriver.WebDriver.GetPage<SharkLasers>()
                              .ClickOnEdit()
                              .EnterEmailAddress(TestInMemoryParametersShared.Instance.GeneratedEmailAddress)
                              .ClickOnSet()
                              .ImplicitlyWaitForPageToBeReady<SharkLasers>(5)
                              .SelectAllEmailsFromInbox()
                              .ClickOnDelete();
        }
    }
}
