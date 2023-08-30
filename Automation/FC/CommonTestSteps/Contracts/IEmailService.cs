using System.Collections.Generic;
using Utilities.EmailServices;

namespace CommonTestSteps.Contracts
{
    public interface IEmailService : IEmailServiceOperator
    {
        void Initialize();
        void CheckAvailability(List<EmailServiceParameters> services);
    }
}
