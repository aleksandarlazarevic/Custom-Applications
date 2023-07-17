using CommonCore.Contracts;
using CommonCore.Tests;
using SeleniumEngine.DriverInitialization;

namespace SeleniumEngine
{
    public class SeleniumManager : IEngineManager
    {
        public void StartUp()
        {
            TestInMemoryParameters.Instance.DriverType = "Edge";

            WebDriverFactory.InitializeWebDriver(TestInMemoryParameters.Instance.DriverType);
            //featureContext["WebDriver"] = WebDriverFactory.WebDriver;
            //ContainerManager.RegisterType(singleCapability.Name, driver);

        }

        public void ShutDown()
        {
            WebDriverFactory.CleanUp();
        }

        public string CollectData(string destinationPath, string eventName)
        {
            throw new NotImplementedException();
        }
    }
}
