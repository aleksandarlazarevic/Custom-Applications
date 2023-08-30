using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Dashboard
{
    public class Help : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement RequestSupportButton = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[1]/android.widget.Button");

        public AndroidElement SupportPhoneNumber = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[2]/android.widget.Button/android.widget.TextView");
        #endregion

        #region Methods

        #endregion
    }
}
