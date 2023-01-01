using OpenQA.Selenium;
using SeleniumCore.Base;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;
using SeleniumCore.Helpers.Extensions;

namespace UIMappings.Pages
{
    public class DemoblazeLogin : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[@id='login2']")]
        public IWebElement LogInLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='signin2']")]
        public IWebElement SignUpLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='loginusername']")]
        public IWebElement UsernameTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='loginpassword']")]
        public IWebElement PasswordTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Log in')]")]
        public IWebElement LogInButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Sign up')]")]
        public IWebElement SignUpButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Close')]")]
        public IWebElement CloseButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='nameofuser']")]
        public IWebElement WelcomeUserLink { get; set; }
        #endregion

        #region Methods
        public DemoblazeLogin ClickOnLogInLink()
        {
            LogInLink.ClickWrapper("LogInLink");
            return this;
        }

        public DemoblazeLogin ClickOnSignUpLink()
        {
            SignUpLink.ClickWrapper("SignUpLink");
            return this;
        }

        public DemoblazeLogin EnterCredentials(string email, string password)
        {
            try
            {
                this.UsernameTextbox.SendKeysWrapper(email, "Username");
                this.PasswordTextbox.SendKeysWrapper(password, "Password");
                return this;
            }
            catch
            {
                this.UsernameTextbox.SendKeysWithClear(email, "Username");
                this.PasswordTextbox.SendKeysWithClear(password, "Password");
                return this;
            }
        }

        public DemoblazeLogin ClickOnLogIn()
        {
            LogInButton.ClickWrapper("LogIn");
            return this;
        }

        public DemoblazeLogin ClickOnSignUp()
        {
            SignUpButton.ClickWrapper("SignUp");
            return this;
        }

        public DemoblazeLogin ClickOnCloseLogIn()
        {
            CloseButton.ClickWrapper("CloseButton");
            return this;
        }

        public DemoblazeLogin WaitForPageReady()
        {
            By loginLink = this.GetElementBy(this.GetType(), "LogInLink");

            WaitForPageToBeReady<DemoblazeLogin>(loginLink, "LogInLink", 10);

            return this;
        }

        public bool HasUserLoggedIn(string username)
        {
            return WelcomeUserLink.Text.Contains(username);
        }

        #endregion
    }
}
