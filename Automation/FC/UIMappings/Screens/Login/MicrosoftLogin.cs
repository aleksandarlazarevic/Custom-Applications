using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using SeleniumExtras.PageObjects;
using Utilities.Android;

namespace UIMappings.Screens.Login
{
    public class MicrosoftLogin : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement EmailAddressTextbox => Helpers.GetScreenElement("//*[@class='android.widget.EditText']");

        public AndroidElement NextButton => Helpers.GetScreenElement("//*[(@class='android.widget.Button') and (@text='Next')]");

        public AndroidElement PasswordTextbox => Helpers.GetScreenElement("//*[@class='android.widget.EditText']");

        [FindsBy(How = How.XPath, Using = "//*[(@class='android.widget.Button') and (@text='Sign in')]")]
        public AndroidElement SignInButton { get; set; }
        #endregion

        #region Methods
        public MicrosoftLogin EnterEmail(string email)
        {
            EmailAddressTextbox.Click();
            EmailAddressTextbox.SendKeys(email);
            return GetInstance<MicrosoftLogin>();
        }

        public MicrosoftLogin EnterPassword(string password)
        {
            PasswordTextbox.Click();
            PasswordTextbox.SendKeys(password);
            return GetInstance<MicrosoftLogin>();
        }

        public MicrosoftLogin ClickNextButton()
        {
            NextButton.Click();
            return GetInstance<MicrosoftLogin>();
        }

        public MicrosoftLogin ClickSignInButton()
        {
            SignInButton = Helpers.GetScreenElement("//*[(@class='android.widget.Button') and (@text='Sign in')]");
            SignInButton.Click();
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<MicrosoftLogin>();
        }

        public bool IsMicrosoftSignInScreenDisplayed()
        {
            return EmailAddressTextbox.Displayed;
        }

        public bool IsMicrosoftSignInSuccessfull()
        {
            return true;
        }
        #endregion

    }
}
