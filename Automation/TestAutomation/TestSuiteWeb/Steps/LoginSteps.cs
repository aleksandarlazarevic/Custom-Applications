using SeleniumCore.WebDriver;
using TestSuiteWeb.SharedSteps.Contracts;
using UIMappings.Pages;

namespace TestSuiteWeb.Steps
{
    public class LoginSteps : ILoginSteps
    {
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
