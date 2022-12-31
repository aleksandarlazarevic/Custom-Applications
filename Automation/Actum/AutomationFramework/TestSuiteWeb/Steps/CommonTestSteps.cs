using CommonTestSteps;
using NUnit.Framework;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using SeleniumCore.WebDriver;
using TechTalk.SpecFlow;
using UIMappings.Pages;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class CommonTestSteps : BaseTest
    {
        [Given(@"The website (.*) is started")]
        public void GivenTheWebsiteIsStarted(string website)
        {
            switch (website)
            {
                case "demoblaze":
                    RunStep(SharedSteps.Containers.Login.GoToDemoblazeWebsite,
                            new TestStepInfo("[BROWSER] - Go to Demoblaze website", false, Importance.High));
                    break;
                default:
                    throw new Exception();
            }
        }

        [When(@"Login to (.*) as (.*), (.*)")]
        public void WhenLoginToDemoblaze(string website, string username, string password)
        {
            LoginToWebsite(website, username, password);

            bool loginSuccessfull = CheckIfUserHasLoggedIn(username);
            Assert.IsTrue(loginSuccessfull, "User failed to log in");
        }

        [When(@"Login to (.*) with non-existing (.*), (.*)")]
        public void WhenLoginToDemoblazeWithNon_ExistingLolaBunnyLolaBunny(string website, string username, string password)
        {
            LoginToWebsite(website, username, password);
            string alertMessage = GetAlertMessage();
                
            Assert.AreEqual(alertMessage, "User does not exist.", "Unexpected error message appeared when trying to log in with non existing account");
        }

        [When(@"Login to (.*) with wrong credentials (.*), (.*)")]
        public void WhenLoginToDemoblazeWithWrongCredentials(string website, string username, string password)
        {
            LoginToWebsite(website, username, password);
            string alertMessage = GetAlertMessage();

            Assert.AreEqual(alertMessage, "Wrong password.", "Unexpected error message appeared when trying to log in with Wrong password");
        }

        #region Login methods
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

        private bool CheckIfUserHasLoggedIn(string username)
        {
            return GetPage<DemoblazeLogin>().HasUserLoggedIn(username);
        }

        private string GetAlertMessage()
        {
            Thread.Sleep(1000);
            string message = UIDriver.WebDriver.SwitchTo().Alert().Text;
            UIDriver.WebDriver.SwitchTo().Alert().Dismiss();

            return message;
        }
        #endregion
    }
}
