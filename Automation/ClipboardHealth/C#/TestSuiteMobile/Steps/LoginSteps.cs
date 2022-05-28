using AppiumCore.Base;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIMappings.Screens;

namespace TestSuite.Steps
{
    [Binding]
    public class LoginSteps 
    {
        [Given(@"The app is launched")]
        public void GivenTheAppIsLaunched()
        {
        }

        [Then(@"Login screen appears")]
        public void ThenLoginScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("LoginScreen");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<LoginScreen>().IsLoginScreenDisplayed(), "Login screen is not shown");
        }

        [Then(@"Enter username and password as")]
        public void ThenEnterUsernameAndPasswordAs(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().LoginAs((string)data.Username, (string)data.Password);
        }

        [Then(@"Enter password")]
        public void ThenEnterPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().Username.SendKeys((string)data.Username);
            Assert.False(ScreenFactory.Instance.CurrentPage.As<LoginScreen>().Login.Enabled);
        }

        [Then(@"Enter username")]
        public void ThenEnterUsername(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().Password.SendKeys((string)data.Password);
            Assert.False(ScreenFactory.Instance.CurrentPage.As<LoginScreen>().Login.Enabled);
        }

        [When(@"Click the Login button")]
        public void WhenClickTheLoginButton()
        {
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().Login.Click();
        }
    }
}
