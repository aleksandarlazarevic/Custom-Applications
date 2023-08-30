using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Dashboard
{
    public class AboutThisApp : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("////android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement AboutThisAppLabel = Helpers.GetScreenElement("//*[(@class='android.widget.TextView') and (@text='About this app')]");

        public AndroidElement Logo = Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]");

        public AndroidElement VersionNumber = Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.TextView[2]");

        public AndroidElement TermsAndConditions = Helpers.GetScreenElement("//*[(@class='android.widget.ViewGroup') and (@text='Terms And Conditions')]");

        public AndroidElement ContactUsButton = Helpers.GetScreenElement("//*[(@class='android.widget.ViewGroup') and (@text='Contact us')]");

        public AndroidElement FooterInfoLabel = Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.TextView[3]");
        #endregion

        #region Methods

        #endregion
    }
}
