using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class IsThisCorrect : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Is this correct?']");

        public AndroidElement PayNowButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[5]/android.widget.Button");

        #endregion

        #region Methods
        public IsThisCorrect ClickBackButton()
        {
            BackButton.Click();
            return GetInstance<IsThisCorrect>();
        }

        public IsThisCorrect ClickPayNowButton()
        {
            PayNowButton.Click();
            return GetInstance<IsThisCorrect>();
        }

        public bool IsIsThisCorrectScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
