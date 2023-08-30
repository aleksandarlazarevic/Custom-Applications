using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class AddMembers : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Add members']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Want to add a family member to your plan? You can manage your family members from the app anytime.']");

        public AndroidElement AddMembersButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.widget.Button");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[4]/android.widget.Button");

        public AndroidElement SkipThisStepLink => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[5]/android.widget.Button");

        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        #endregion

        #region Methods
        public AddMembers ClickSkipThisStepLink()
        {
            SkipThisStepLink.Click();
            return GetInstance<AddMembers>();
        }

        public AddMembers ClickAddMembersButton()
        {
            AddMembersButton.Click();
            return GetInstance<AddMembers>();
        }

        public AddMembers ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<AddMembers>();
        }

        public AddMembers ClickBackButton()
        {
            BackButton.Click();
            return GetInstance<AddMembers>();
        }

        public bool IsAddMembersScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
