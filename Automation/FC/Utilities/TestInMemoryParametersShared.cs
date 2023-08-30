using System;
using System.Collections.Generic;
using Utilities.EmailServices;

namespace Utilities
{
    public class TestInMemoryParametersShared
    {
        private static Lazy<TestInMemoryParametersShared> _instance = new Lazy<TestInMemoryParametersShared>(() => new TestInMemoryParametersShared());
        public string EmailServiceUrl { get; set; }
        public string EmailServiceName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TestIdentifierStartTime { get; set; }
        public string TemporaryPassword { get; set; }
        public string GeneratedEmailAddress { get; set; }
        public string VerificationSenderAddress { get; set; }
        public string VerificationCode { get; set; }
        public string EmailSender { get; set; }
        public string ExpectedNumberOfEmails { get; set; }
        public string SetEmailFor { get; set; }
        public string EmailText { get; set; }
        public string FileNameSearchPatternEmail { get; set; }
        public string ExpectedNumberOfRepetition { get; set; }
        public string VerificationCodeText { get; set; }
        public List<EmailServiceParameters> EmailServices = new List<EmailServiceParameters>()
        {
            new EmailServiceParameters() { Name = "Sharklasers", Url = "https://www.sharklasers.com/" },
            new EmailServiceParameters() { Name = "Mailinator", Url = "https://www.mailinator.com/" }
        };
          
        public static TestInMemoryParametersShared Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public string SmsVerificationCode { get; set; }

        private TestInMemoryParametersShared() { }
    }
}
