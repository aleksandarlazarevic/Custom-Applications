using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumCore.Base
{
    public class BaseScreen
    {
        public AndroidDriver<AppiumWebElement> AppiumDriver;

        public BaseScreen() => AppiumDriver = DriverFactory.Instance.AppiumDriver;

        public TScreen GetInstance<TScreen>() where TScreen : BaseScreen, new()
        {
            var T = Activator.CreateInstance(typeof(TScreen));
            return (TScreen)T;
        }

        public TScreen As<TScreen>() where TScreen : BaseScreen
        {
            return (TScreen)this;
        }
    }
}