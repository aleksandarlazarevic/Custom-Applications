namespace SharedTestSteps.Contracts
{
    public interface IEmailServiceOperator
    {
        void GetEmailAddress();
        void GenerateEmail();
        void GetVerificationCodeFromEmail();
        void VerifyEmailNotificationContent();
        void DeleteAllEmailsFromInbox();
    }
}
