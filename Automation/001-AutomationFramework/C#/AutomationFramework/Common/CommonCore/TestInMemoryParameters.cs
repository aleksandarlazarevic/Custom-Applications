using Newtonsoft.Json.Linq;
using System.Xml;
using Utilities;
using static NUnit.Framework.TestContext;

namespace CommonCore.Tests
{
    public sealed class TestInMemoryParameters
    {
        #region Fields and Properties
        private static TestInMemoryParameters instance = null;
        private static readonly object padlock = new object();
        public bool MultipleBrowserInstances { get; set; }
        public string DriverType { get; set; }
        public TestAdapter TestIdentifier { get; set; }
        public DateTime TestIdentifierStartTime { get; set; }
        public string Url { get; set; }
        public string ElementTimeout { get; set; }
        public string PageLoadTimeout { get; set; }
        public string EmailServiceUrl { get; set; }
        public string EmailServiceName { get; set; }
        public string EmailAddress { get; set; }
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
            new EmailServiceParameters() { Name = "Mailinator", Url = "https://www.mailinator.com/" },
            new EmailServiceParameters() { Name = "Sharklasers", Url = "https://www.sharklasers.com/" }
        };

        public string SmsVerificationCode { get; set; }
        public static TestInMemoryParameters Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TestInMemoryParameters();
                    }
                    return instance;
                }
            }
        }

        public string ApiEndpoint { get; set; }
        public JObject JsonApiResponse { get; set; }
        public XmlDocument XmlApiResponse { get; set; }
        public string TextApiResponse { get; set; }
        public string TestResultsDirectory { get; set; }
        #endregion
        #region Methods
        TestInMemoryParameters()
        {
            MultipleBrowserInstances = false;
            DriverType = EnvironmentVariable.GetEnvironmentVariable("browser", "Edge");
        }
        #endregion
    }
}