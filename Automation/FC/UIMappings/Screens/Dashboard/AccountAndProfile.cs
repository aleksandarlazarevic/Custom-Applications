using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Dashboard
{
    public class AccountAndProfile : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement EditButton = Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[1]/android.view.ViewGroup");

        public AndroidElement UserName = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.TextView[1]");

        public AndroidElement BirthDate = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.TextView[2]");

        public AndroidElement ProfilePicture = Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[2]/android.view.ViewGroup[1]/android.widget.ImageView");

        public AndroidElement EmailAddressTextbox = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.view.ViewGroup[1]/android.view.ViewGroup[1]");

        public AndroidElement PhoneNumberTextbox = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.view.ViewGroup[2]/android.view.ViewGroup[1]");

        public AndroidElement ContactSupportButton = Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.view.ViewGroup[3]/android.widget.Button");

        #endregion

        #region Methods

        #endregion
    }
}
