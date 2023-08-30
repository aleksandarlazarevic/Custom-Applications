using AppiumCore.Base;
using CommonTestSteps.TestSteps.Mobile;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIMappings.Screens;
using UIMappings.Screens.Login;

namespace TestSuiteMobile.Steps
{
    [Binding]
    public class LoginSteps
    {
        [Then(@"Login screen appears")]
        public void ThenLoginScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("LoginScreen");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<LoginScreen>().IsLoginScreenDisplayed(), "Login screen is not shown");
        }

        [Then(@"Enter email (.*)")]
        public void ThenEnterEmailTestim_IzeeIron_Com(string email)
        {
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().EnterEmailAddress(email);
        }

        [When(@"Click the Login button")]
        public void WhenClickTheLoginButton()
        {
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().ClickLoginButton();
            System.Threading.Thread.Sleep(10000);
        }

        [Then(@"Microsoft Sign in screen appears")]
        public void ThenMicrosoftSignInScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("MicrosoftLogin");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().IsMicrosoftSignInScreenDisplayed(), "Microsoft Sign In screen is not shown");
        }

        [When(@"Enter email (.*) on Microsoft's form")]
        public void WhenEnterEmailTestim_IzeeIron_ComOnMicrosoftsForm(string email)
        {
            System.Threading.Thread.Sleep(5000);
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().EnterEmail(email);
        }

        [When(@"Microsof Next in button is clicked")]
        public void WhenMicrosofNextInButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().ClickNextButton();
            System.Threading.Thread.Sleep(5000);
        }

        [When(@"Microsoft password is entered")]
        public void WhenMicrosoftPasswordIsEntered(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().EnterPassword((string)data.Password);
        }

        [When(@"Microsoft password (.*) is entered")]
        public void WhenMicrosoftPasswordTestingWorksIsEntered(string password)
        {
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().EnterPassword(password);
        }

        [When(@"Microsoft SignIn button is clicked")]
        public void WhenMicrosoftSignInButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().ClickSignInButton();
            System.Threading.Thread.Sleep(5000);
        }

        [When(@"Microsoft Stay Signed in is accepted")]
        public void WhenMicrosoftStaySignedInIsAccepted()
        {
            ScreenFactory.Instance.CurrentPage.As<MicrosoftLogin>().IsMicrosoftSignInSuccessfull();
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"Verify that the email (.*) is not bound to another device")]
        public void ThenVerifyThatTheEmailIsNotBoundToAnotherDevice(string email)
        {
            GlobalMobileTestSteps.RemoveImeiBindings(email);
        }
    }
}