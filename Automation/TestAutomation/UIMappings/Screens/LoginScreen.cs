using AppiumCore.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Threading;

namespace UIMappings.Screens
{
    public class LoginScreen : BaseScreen
    {
        #region Fields and Properties
        public AppiumWebElement Username => AppiumDriver.FindElementByAccessibilityId("username");

        public AppiumWebElement Password => AppiumDriver.FindElementByAccessibilityId("password");

        public AppiumWebElement Login => AppiumDriver.FindElementByAccessibilityId("login");

        public bool IsLoginScreenDisplayed()
        {
            return Username.Displayed;
        }
        #endregion

        #region Methods
        public HomeScreen LoginAs(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            AppiumDriver.HideKeyboard();
            Login.Click();
            return GetInstance<HomeScreen>();
        }

        public IAlert LoginScreenAlert()
        {
            // todo: dynamic wait for alert
            Thread.Sleep(TimeSpan.FromSeconds(10));
            return AppiumDriver.SwitchTo().Alert();
        }
        #endregion
    }
}
