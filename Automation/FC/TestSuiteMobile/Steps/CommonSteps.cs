using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using SeleniumCore.Helpers.Extensions;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using Utilities.Android;

namespace TestSuiteMobile.Steps
{
	[Binding]
	public class CommonSteps : BaseScreen
    {
        [Given(@"The app is launched")]
        public void GivenTheAppIsLaunched()
        {
            CommonSteps.MinimizeUpdateVersionScreen();
        }

        public static void MinimizeUpdateVersionScreen()
        {
			try
			{
				AndroidElement MinimizeButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]");
				MinimizeButton.Click();
			}
			catch (Exception ex)
			{
				string message = "UpdateVersionScreen is not shown" + ex.Message;
			}        
		}
    }
}
