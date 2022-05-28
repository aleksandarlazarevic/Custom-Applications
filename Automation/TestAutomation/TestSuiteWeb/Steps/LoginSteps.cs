using SeleniumCore.WebDriver;
using System.Threading;
using TestSuiteWeb.SharedSteps.Contracts;
using UIMappings.Pages;

namespace TestSuiteWeb.Steps
{
    public class LoginSteps : ILoginSteps
    {
        string _url = "https://www.google.com/";
        public virtual void StepNavigateToPage()
        {
            UIDriver.InitializeWebDriver("ChromeDriver");
            Thread.Sleep(5000);
            UIDriver.WebDriver.Navigate().GoToUrl(_url);
            UIDriver.TakeScreenshot("LOGIN - Succesfully navigated to the main page");
        }

        public virtual void StepSimpleLogin()
        {
            UIDriver.WebDriver.GetPage<Login>()
                .EnterCredentials("user1", "pass123")
                .ClickOnSignIn()
                .ImplicitlyWaitForPageToBeReady<Login>(5);

            UIDriver.TakeScreenshot("LOGIN - Succesful login");
        }
    }
}
