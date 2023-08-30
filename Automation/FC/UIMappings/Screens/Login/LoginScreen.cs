using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.PageObjects;
using UIMappings.Screens.Dashboard;
using Utilities.Android;

namespace UIMappings.Screens.Login
{
    public class LoginScreen : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement EmailAddressTextbox => Helpers.GetScreenElement("//*[@class='android.widget.EditText']");

        public AndroidElement EnterEmailButton => Helpers.GetScreenElement("//*[(@class='android.view.ViewGroup') and (@clickable='true')]");

        public AndroidElement RequestSupportLink => Helpers.GetScreenElement("//*[(@class='android.widget.TextView') and (@text='Request support')]");

        public AndroidElement QuestionMark => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.view.ViewGroup[3]");

        public AndroidElement TopPicture => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement Logo => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text=\"Let's do this!\"]");

        public AndroidElement InstructionsText => Helpers.GetScreenElement("//android.widget.TextView[normalize-space(@text)='Please provide your email address.']");

        [FindsBy(How = How.XPath, Using = "//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[3]")]
        public AndroidElement EmailFormatErrorMessage { get; set; }

        #endregion

        #region Methods
        public LoginScreen EnterEmailAddress(string email)
        {
            EmailAddressTextbox.Click();
            EmailAddressTextbox.SendKeys(email);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<LoginScreen>();
        }

        public LoginScreen ClickLoginButton()
        {
            EnterEmailButton.Click();
            return GetInstance<LoginScreen>();
        }

        public bool IsLoginScreenDisplayed()
        {
            return EmailAddressTextbox.Displayed;
        }
        #endregion
    }
}