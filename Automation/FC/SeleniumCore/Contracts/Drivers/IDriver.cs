using OpenQA.Selenium;

namespace SeleniumCore.Contracts.Drivers
{
    interface IDriver
    {
        IWebDriver Initialize();
    }
}