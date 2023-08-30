using CommonTestSteps.Contracts;
using SeleniumCore.Base;
using TechTalk.SpecFlow;
using UIMappings.Pages.Fcos;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class LoginTestSteps : BaseTest, ILoginTestSteps
    {
        [When(@"Login to (.*) as (.*), (.*)")]
        public void ThenLoginToWebsiteWithCredentials(string website, string username, string password)
        {
            switch (website)
            {
                case "Fcos":
                    LoginToFcos(username, password);
                    break;
                case "FcosAzure":
                    LoginToFcosAzure(username, password);
                    break;
                case "FranchiCzar":
                    LoginToFranchiCzar(username, password);
                    break;
                case "Iron24":
                    LoginToIron24(username, password);
                    break;
                case "MathReactor":
                    LoginToMathReactor(username, password);
                    break;
                case "Nael":
                    LoginToNael(username, password);
                    break;
                case "Valhallan":
                    LoginToValhallan(username, password);
                    break;
                default:
                    break;
            }
        }

        [Then(@"Log out of (.*)")]
        public void ThenLogOutOfWebsite(string website)
        {
            switch (website)
            {
                case "Fcos":
                    LogoutOfFcos();
                    break;
                case "FcosAzure":
                    LogoutOfFcosAzure();
                    break;
                case "FranchiCzar":
                    LogoutOfFranchiCzar();
                    break;
                case "Iron24":
                    LogoutOfIron24();
                    break;
                case "MathReactor":
                    LogoutOfMathReactor();
                    break;
                case "Nael":
                    LogoutOfNael();
                    break;
                case "Valhallan":
                    LogoutOfValhallan();
                    break;
                default:
                    break;
            }
        }

        #region Login methods
        public void LoginToFcos(string email, string password)
        {
            GetPage<FcosLogin>().WaitForPageReady()
                                .EnterMicrosoftCredentials(email, password)
                                .ClickOnMicrosoftSignIn()
                                .ClickStaySignedInYesButton();
        }

        public void LoginToFcosAzure(string email, string password)
        {
            GetPage<FcosAzureLogin>().WaitForPageReady()
                    .EnterMicrosoftCredentials(email, password)
                    .ClickOnMicrosoftSignIn()
                    .ClickStaySignedInYesButton();
        }

        public void LoginToFranchiCzar(string username, string password)
        {

        }

        public void LoginToIron24(string username, string password)
        {

        }

        public void LoginToMathReactor(string username, string password)
        {

        }

        public void LoginToNael(string username, string password)
        {

        }

        public void LoginToValhallan(string username, string password)
        {

        }
        #endregion

        #region Logout methods
        private void LogoutOfValhallan()
        {

        }

        private void LogoutOfNael()
        {

        }

        private void LogoutOfMathReactor()
        {

        }

        private void LogoutOfIron24()
        {

        }

        private void LogoutOfFranchiCzar()
        {

        }

        private void LogoutOfFcosAzure()
        {

        }

        private void LogoutOfFcos()
        {

        }
        #endregion
    }
}
