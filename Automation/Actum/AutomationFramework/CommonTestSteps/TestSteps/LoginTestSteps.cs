using CommonTestSteps.Contracts;
using SeleniumCore;
using UIMappings.Pages;

namespace CommonTestSteps.TestSteps
{
    public class LoginTestSteps : GlobalTestSteps, ILoginTestSteps
    {
        public void GoToDemoblazeWebsite()
        {
            TestInMemoryParameters.Instance.Url = "https://www.demoblaze.com/";
            OpenBrowser();
        }

        public void LoginToWebsite(string website, string username, string password)
        {
            switch (website)
            {
                case "demoblaze":
                    LoginToDemoblaze(username, password);
                    break;
                default:
                    break;
            }
        }
        public void LoginToDemoblaze(string email, string password)
        {
            GetPage<DemoblazeLogin>().WaitForPageReady()
                                .ClickOnLogInLink()
                                .EnterCredentials(email, password)
                                .ClickOnLogIn();
        }
    }
}
