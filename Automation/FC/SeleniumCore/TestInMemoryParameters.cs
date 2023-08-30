using SeleniumCore.Helpers.Utilities;

namespace SeleniumCore
{
    public sealed class TestInMemoryParameters
    {
        private static TestInMemoryParameters instance = null;
        private static readonly object padlock = new object();
        public bool MultipleBrowserInstances { get; set; }
        public string WebDriver { get; set; }
        public string TestIdentifier { get; set; }
        public string TestIdentifierStartTime { get; set; }
        public string Url { get; set; }
        public string ElementTimeout { get; set; }
        public string PageLoadTimeout { get; set; }
        TestInMemoryParameters()
        {
            MultipleBrowserInstances = false;
            WebDriver = EnvironmentVariable.GetEnvironmentVariable("browser", "Edge");
        }

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

        public string TypeOfCompany { get; set; }
    }
}