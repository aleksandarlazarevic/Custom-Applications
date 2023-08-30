using OpenQA.Selenium;
using SeleniumCore.Base;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;
using SeleniumCore.Helpers.Extensions;

namespace UIMappings.Pages.Fcos
{
    public class FcosLogin : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//button[@id='FranchiCzarExchange']")]
        public IWebElement Microsoft365LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='signInName']")]
        public IWebElement EmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='next']")]
        public IWebElement SignInButton { get; set; }


        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        public IWebElement MicrosoftSignInEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='idSIButton9']")]
        public IWebElement MicrosoftNextButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        public IWebElement MicrosoftPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='idSIButton9']")]
        public IWebElement MicrosoftSignInButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='idSIButton9']")]
        public IWebElement StaySignedInYesButton { get; set; }
        #endregion

        #region Methods
        public FcosLogin EnterCredentials(string email, string password)
        {
            try
            {
                this.EmailAddress.SendKeysWrapper(email, "Username");
                this.Password.SendKeysWrapper(password, "Password");
                return this;
            }
            catch
            {
                this.EmailAddress.SendKeysWithClear(email, "Username");
                this.Password.SendKeysWithClear(password, "Password");
                return this;
            }
        }

        public FcosLogin EnterMicrosoftCredentials(string email, string password)
        {
            this.Microsoft365LoginButton.ClickWrapper("Microsoft 365 Login");

            try
            {
                this.MicrosoftSignInEmail.SendKeysWrapper(email, "Username");
                this.MicrosoftNextButton.ClickWrapper("Microsoft Next Button");
                this.MicrosoftPassword.SendKeysWrapper(password, "Password");
                return this;
            }
            catch
            {
                this.MicrosoftSignInEmail.SendKeysWithClear(email, "Username");
                this.MicrosoftPassword.SendKeysWithClear(password, "Password");
                return this;
            }
        }

        public FcosLogin ClickOnSignIn()
        {
            SignInButton.ClickWrapper("SignIn");
            return this;
        }

        public FcosLogin ClickOnMicrosoftSignIn()
        {
            MicrosoftSignInButton.ClickWrapper("SignIn");
            return this;
        }

        public FcosLogin ClickStaySignedInYesButton()
        {
            StaySignedInYesButton.ClickWrapper("Stay Signed In Yes Button");
            return this;
        }

        public FcosLogin WaitForPageReady()
        {
            By email = this.GetElementBy(this.GetType(), "EmailAddress");

            WaitForPageToBeReady<FcosLogin>(email, "Email", 10);

            return this;
        } 
        #endregion
    }
}
