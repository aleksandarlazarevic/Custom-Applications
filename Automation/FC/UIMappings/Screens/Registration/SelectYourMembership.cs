using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class SelectYourMembership : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Select your membership']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Choose a membership that is right for you!']");

        public AndroidElement MemebershipPeriod => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.HorizontalScrollView/android.view.ViewGroup/android.widget.TextView[1]");

        public AndroidElement Price => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.HorizontalScrollView/android.view.ViewGroup/android.widget.TextView[2]");

        public AndroidElement PricingDescription => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.HorizontalScrollView/android.view.ViewGroup/android.widget.TextView[3]");

        public AndroidElement SelectButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.widget.HorizontalScrollView/android.view.ViewGroup/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.Button");

        public AndroidElement CancelLink => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.Button");

        #endregion

        #region Methods
        public SelectYourMembership ClickSelectButton()
        {
            SelectButton.Click();
            return GetInstance<SelectYourMembership>();
        }

        public SelectYourMembership ClickCancelLink()
        {
            CancelLink.Click();
            return GetInstance<SelectYourMembership>();
        }

        public bool IsSelectYourMembershipScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
