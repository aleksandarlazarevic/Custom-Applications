using OpenQA.Selenium;

namespace SeleniumEngine.DriverInitialization
{
    public interface IBrowserDriver
    {
        IWebDriver Initialize();
    }
}