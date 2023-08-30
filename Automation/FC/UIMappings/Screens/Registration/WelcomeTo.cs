using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class WelcomeTo : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[contains(@text, 'Welcome to')]");

        public AndroidElement InformationalMessage => Helpers.GetScreenElement("//android.widget.TextView[@text='Payment successful. Membership activated.']");

        public AndroidElement GoToHomeScreenLink => Helpers.GetScreenElement("//android.widget.Button");

        #endregion

        #region Methods
        public WelcomeTo ClickGoToHomeScreenLink()
        {
            GoToHomeScreenLink.Click();
            return GetInstance<WelcomeTo>();
        }

        public bool IsWelcomeToScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
