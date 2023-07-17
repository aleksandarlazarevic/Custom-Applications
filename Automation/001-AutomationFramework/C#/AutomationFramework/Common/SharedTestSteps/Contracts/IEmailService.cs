using CommonCore;

namespace SharedTestSteps.Contracts
{
    public interface IEmailService : IEmailServiceOperator
    {
        void Initialize();
        void CheckAvailability(List<EmailServiceParameters> services);
    }
}
