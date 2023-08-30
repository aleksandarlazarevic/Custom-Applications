using OpenQA.Selenium.Appium.Android;
using Utilities.Android;
using AppiumCore.Base;
using UIMappings.Screens.Dashboard;

namespace UIMappings.Screens.Login
{
    public class SupportRequest : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[1]");

        public AndroidElement Title = Helpers.GetScreenElement("//android.widget.TextView[@text='New support request']");

        public AndroidElement InstructionsText = Helpers.GetScreenElement("//android.widget.TextView[@text='Please provide your contact details so we can help with your request']");

        public AndroidElement FirstName => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.EditText[1]");

        public AndroidElement LastName => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.EditText[2]");

        public AndroidElement Email => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.EditText[3]");

        public AndroidElement CountryPrefix => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]");

        public AndroidElement FlagIcon => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]/android.widget.TextView[1]");

        public AndroidElement CountryCode => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]/android.widget.TextView[2]");

        public AndroidElement PhoneNumber => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.EditText[4]");

        public AndroidElement CancelButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.Button[1]");

        public AndroidElement NextButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.Button[2]");

        public AndroidElement CountryListFirstCountry => Helpers.GetScreenElement("//*[(@class='android.widget.TextView') and (@text='United States (+1)')]");
        #endregion

        #region Methods
        public SupportRequest EnterFirstName(string firstName)
        {
            FirstName.Click();
            FirstName.SendKeys(firstName);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<SupportRequest>();
        }

        public SupportRequest EnterLastName(string lastName)
        {
            LastName.Click();
            LastName.SendKeys(lastName);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<SupportRequest>();
        }

        public SupportRequest EnterEmail(string email)
        {
            Email.Click();
            Email.SendKeys(email);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<SupportRequest>();
        }

        public SupportRequest EnterPhoneNumber(string phoneNumber)
        {
            PhoneNumber.Click();
            PhoneNumber.SendKeys(phoneNumber);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<SupportRequest>();
        }
        public SupportRequest ClickNextButton()
        {
            NextButton.Click();
            return GetInstance<SupportRequest>();
        }

        public SupportRequest ClickCancelButton()
        {
            CancelButton.Click();
            return GetInstance<SupportRequest>();
        }

        public bool IsSupportRequestScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
