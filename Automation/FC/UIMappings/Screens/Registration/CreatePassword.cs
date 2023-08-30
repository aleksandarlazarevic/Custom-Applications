using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class CreatePassword : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Logo => Helpers.GetScreenElement("//android.widget.Image[@text='Company Logo']");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Create Password']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.webkit.WebView/android.view.View/android.view.View/android.widget.TextView[2]");

        public AndroidElement NewPasswordTextbox => Helpers.GetScreenElement("//android.webkit.WebView/android.view.View/android.view.View/android.view.View/android.view.View[1]/android.widget.ListView/android.view.View[1]/android.view.View/android.widget.EditText");

        public AndroidElement ConfirmNewPasswordTextbox => Helpers.GetScreenElement("//android.webkit.WebView/android.view.View/android.view.View/android.view.View/android.view.View[1]/android.widget.ListView/android.view.View[2]/android.view.View/android.widget.EditText");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.Button[@text='Continue']");

        public AndroidElement CancelLink => Helpers.GetScreenElement("//android.widget.Button[@text='Cancel']");

        #endregion

        #region Methods
        public CreatePassword ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<CreatePassword>();
        }

        public CreatePassword ClickCancelLink()
        {
            CancelLink.Click();
            return GetInstance<CreatePassword>();
        }

        public CreatePassword EnterNewPassword(string password)
        {
            NewPasswordTextbox.Click();
            NewPasswordTextbox.SendKeys(password);
            return GetInstance<CreatePassword>();
        }

        public CreatePassword EnterConfirmNewPassword(string password)
        {
            ConfirmNewPasswordTextbox.Click();
            ConfirmNewPasswordTextbox.SendKeys(password);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<CreatePassword>();
        }

        public bool IsCreatePasswordScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
