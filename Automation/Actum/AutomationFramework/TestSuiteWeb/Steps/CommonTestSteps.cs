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
            RunStep(SharedSteps.Containers.Login.LoginToWebsite, website, username, password,
                    new TestStepInfo("[BROWSER] - Go to Demoblaze website", false, Importance.High));

            bool loginSuccessfull = CheckIfUserHasLoggedIn(username);
            Assert.IsTrue(loginSuccessfull, "User failed to log in");
        }

        [When(@"Login to (.*) with non-existing (.*), (.*)")]
        public void WhenLoginToDemoblazeWithNon_ExistingLolaBunnyLolaBunny(string website, string username, string password)
        {
            RunStep(SharedSteps.Containers.Login.LoginToWebsite, website, username, password,
                    new TestStepInfo("[BROWSER] - Go to Demoblaze website", false, Importance.High));
            
            string alertMessage = GetAlertMessage();                
            Assert.AreEqual(alertMessage, "User does not exist.", "Unexpected error message appeared when trying to log in with non existing account");
        }

        [When(@"Login to (.*) with wrong credentials (.*), (.*)")]
        public void WhenLoginToDemoblazeWithWrongCredentials(string website, string username, string password)
        {
            RunStep(SharedSteps.Containers.Login.LoginToWebsite, website, username, password,
                    new TestStepInfo("[BROWSER] - Go to Demoblaze website", false, Importance.High));
            
            string alertMessage = GetAlertMessage();
            Assert.AreEqual(alertMessage, "Wrong password.", "Unexpected error message appeared when trying to log in with Wrong password");
        }

        [When(@"Sign up new account (.*), (.*), (.*)")]
        public void WhenSignUpNewAccountLonyBunnyLonyBunny(string website, string username, string password)
        {
            RunStep(SharedSteps.Containers.SignUp.SignUpToWebsite, website, username, password,
                    new TestStepInfo("[BROWSER] - Go to Demoblaze website", false, Importance.High));

            string alertMessage = GetAlertMessage();

            Assert.AreEqual(alertMessage, "Sign up successful.", "Unexpected error message appeared when trying to Register");
        }
        #region Login methods
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
