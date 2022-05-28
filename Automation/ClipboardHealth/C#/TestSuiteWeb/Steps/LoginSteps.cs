using SeleniumCore.Helpers.Utilities;
using SeleniumCore.WebDriver;
using System.Threading;
using TestSuiteWeb.SharedSteps.Contracts;
using UIMappings.Pages;

namespace TestSuiteWeb.Steps
{
    public class LoginSteps : ILoginSteps
    {
        string _url = "https://www.amazon.in/";
        public virtual void NavigateToPage()
        {
            UIDriver.InitializeWebDriver("ChromeDriver");
            Thread.Sleep(5000);
            UIDriver.WebDriver.Navigate().GoToUrl(_url);
            UIDriver.TakeScreenshot("LOGIN - Succesfully navigated to the main page");
        }
    }
}
