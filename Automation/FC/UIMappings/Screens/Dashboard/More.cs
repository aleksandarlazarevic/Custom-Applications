using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using UIMappings.Screens.Login;
using Utilities.Android;

namespace UIMappings.Screens.Dashboard
{
    public class More : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Image = Helpers.GetScreenElement("//android.widget.ImageView");

        public AndroidElement UserName = Helpers.GetScreenElement("//android.widget.TextView");

        public AndroidElement EditProfileLink/* = Helpers.GetScreenElement("//*[(@class='android.widget.TextView') and (@text='Edit profile')]")*/;

        public AndroidElement ManageSubscriptionsButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[5]");

        public AndroidElement AboutThisAppButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[6]");

        public AndroidElement SignOutButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup[7]");

        #endregion

        #region Methods
        public LoginScreen ClickSignOutButton()
        {
            SignOutButton.Click();
            return GetInstance<LoginScreen>();
        }
        #endregion
    }
}
