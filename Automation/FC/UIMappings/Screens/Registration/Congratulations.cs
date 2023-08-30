using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class Congratulations : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Congratulations!']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Your account registration is complete.']");

        public AndroidElement ContinueLink => Helpers.GetScreenElement("//android.widget.Button");

        #endregion

        #region Methods
        public bool IsCongratulationsScreenDisplayed()
        {
            return Title.Displayed;
        }

        public Congratulations ClickContinueLink()
        {
            ContinueLink.Click();
            return GetInstance<Congratulations>();
        }
        #endregion
    }
}
