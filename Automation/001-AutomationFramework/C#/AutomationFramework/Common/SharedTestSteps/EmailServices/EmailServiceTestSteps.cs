using CommonCore;
using CommonCore.Tests;
using SharedTestSteps.Contracts;
using Utilities;

namespace SharedTestSteps.EmailServices
{
    public class EmailServiceTestSteps : IEmailService
    {
        private IEmailServiceOperator _emailService = null;

        public void Initialize()
        {
            switch (TestInMemoryParameters.Instance.EmailServiceName)
            {
                case "Mailinator":
                    _emailService = new MailinatorTestSteps();
                    break;
                case "Sharklasers":
                    _emailService = new SharkLasersTestSteps();
                    break;

                default:
                    throw new InvalidOperationException("Email service not initialized");
            }
        }

        public void GetVerificationCodeFromEmail()
        {
            _emailService.GetVerificationCodeFromEmail();
        }

        public void GenerateEmail()
        {
            _emailService.GenerateEmail();
        }

        public void GetEmailAddress()
        {
            _emailService.GetEmailAddress();
        }

        public virtual void CheckAvailability(List<EmailServiceParameters> services)
        {
            var service = services.Find(x => WebsiteManager.IsWebsiteAvailable(x.Url));

            if (service == null)
            {
                throw new InvalidOperationException("None of the email providers is currently available");
            }

            TestInMemoryParameters.Instance.EmailServiceUrl = service.Url;
            TestInMemoryParameters.Instance.EmailServiceName = service.Name;
        }

        public void VerifyEmailNotificationContent()
        {
            _emailService.VerifyEmailNotificationContent();
        }

        public void DeleteAllEmailsFromInbox()
        {
            _emailService.DeleteAllEmailsFromInbox();
        }
    }
}