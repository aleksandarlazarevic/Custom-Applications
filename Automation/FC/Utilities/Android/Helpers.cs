using System;
using AppiumCore.Base;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace Utilities.Android
{
    public class Helpers
    {
        public static AndroidElement GetScreenElement(string elementLocator)
        {
            AndroidElement screenElement = (AndroidElement)new WebDriverWait(DriverFactory.Instance.AppiumDriver, TimeSpan.FromSeconds(60)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(MobileBy.XPath(elementLocator)));

            return screenElement;
        }
    }
}