using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class FindTheGymsNearYou : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[1]");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Find the gyms near you']");
        public AndroidElement AllowWhileUsingTheApp { get; set; }
        public AndroidElement UseCurrentLocationLink => Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]");

        public AndroidElement InstructionsText => Helpers.GetScreenElement("//android.widget.TextView[normalize-space(@text)='Or enter in your address for a list of clubs near you.']");

        public AndroidElement TypeYourLocationTextbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.view.ViewGroup");

        public AndroidElement TypeYourLocationSearchBar => Helpers.GetScreenElement("//android.widget.EditText[@text='Type your location']");

        public AndroidElement FirstSuggestedLocation => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup");

        public AndroidElement FirstGymNear => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[4]/android.view.ViewGroup[1]");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.Button");

        public AndroidElement ContinueButtonLabel => Helpers.GetScreenElement("//android.widget.TextView[@text='Continue']");

        public AndroidElement ErrorText => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.TextView[2]");
        public AndroidElement ErrorContactSupportButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.Button[1]");
        public AndroidElement ErrorTryAgainLink => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.Button[2]");

        #endregion

        #region Methods
        public FindTheGymsNearYou ClickUseCurrentLocationLink()
        {
            UseCurrentLocationLink.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou ClickAllowWhileUsingTheApp()
        {
            AllowWhileUsingTheApp = Helpers.GetScreenElement("//*[(@class='android.widget.Button') and (@text='While using the app')]");
            AllowWhileUsingTheApp.Click();
            Thread.Sleep(15000);
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou ClickFirstSuggestedLocation()
        {
            FirstSuggestedLocation.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou ClickFirstGymNear()
        {
            FirstGymNear.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public void EnterCustomLocation(string location)
        {
            TypeYourLocationTextbox.Click();
            TypeYourLocationSearchBar.Click();
            TypeYourLocationSearchBar.SendKeys(location);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            FirstSuggestedLocation.Click();
        }

        public FindTheGymsNearYou ClickContactSupportButton()
        {
            ErrorContactSupportButton.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou ClickTryAgainLink()
        {
            ErrorTryAgainLink.Click();
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou EnterLocation(string location)
        {
            TypeYourLocationTextbox.Click();
            TypeYourLocationTextbox.SendKeys(location);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<FindTheGymsNearYou>();
        }

        public FindTheGymsNearYou EnterLocationInSearchBar(string location)
        {
            TypeYourLocationSearchBar.Click();
            TypeYourLocationSearchBar.SendKeys(location);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<FindTheGymsNearYou>();
        }

        public bool IsFindTheGymsNearYouScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
