using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;
using Utilities.Android;

namespace UIMappings.Screens.Dashboard
{
    public class Home : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement WhileUsingTheAppButton;

        public AndroidElement OnlyThisTimeButton;

        public AndroidElement DontAllowButton;

        public AndroidElement HomeButton;

        public AndroidElement HelpButton;

        public AndroidElement MoreButton;
        #endregion

        #region Methods
        public Home ClickMoreButton()
        {
            MoreButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.widget.Button[3]");
            MoreButton.Click();
            return GetInstance<Home>();
        }

        public Home ClickHelpButton()
        {
            HelpButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.widget.Button[2]");
            HelpButton.Click();
            return GetInstance<Home>();
        }

        public Home ClickHomeButton()
        {
            HomeButton.Click();
            return GetInstance<Home>();
        }

        public Home ClickWhileUsingTheAppButton()
        {
            WhileUsingTheAppButton = Helpers.GetScreenElement("//android.widget.Button[@text='While using the app']");
            WhileUsingTheAppButton.Click();
            Thread.Sleep(1000);
            return GetInstance<Home>();
        }

        public Home ClickOnlyThisTimeButton()
        {
            OnlyThisTimeButton = Helpers.GetScreenElement("//android.widget.Button[@text='Only this time']");
            OnlyThisTimeButton.Click();
            return GetInstance<Home>();
        }

        public Home ClickDontAllowButton()
        {
            DontAllowButton = Helpers.GetScreenElement("//android.widget.Button[@text='Deny']");
            DontAllowButton.Click();
            return GetInstance<Home>();
        }

        public bool? IsHomeScreenDisplayed()
        {
            HomeButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.widget.Button[1]");
            return HomeButton.Displayed;
        }
        #endregion
    }
}