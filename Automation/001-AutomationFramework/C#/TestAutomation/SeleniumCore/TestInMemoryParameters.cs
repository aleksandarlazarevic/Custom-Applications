namespace SeleniumCore
{
    public sealed class TestInMemoryParameters
    {
        private static TestInMemoryParameters instance = null;
        private static readonly object padlock = new object();

        TestInMemoryParameters()
        {
            MultipleBrowserInstances = false;
            WebDriver = "Chrome";
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

        public bool MultipleBrowserInstances { get; set; }
        public string WebDriver { get; set; }
        public string TestIdentifier { get; set; }
        public string TestIdentifierStartTime { get; set; }
    }
}
