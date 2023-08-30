using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class AddNewMember : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Add new member']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Please add member details']");

        public AndroidElement FirstName => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]/android.widget.EditText");

        public AndroidElement LastName => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.EditText");

        public AndroidElement DateOfBirth => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.widget.EditText");

        public AndroidElement Email => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[4]/android.widget.EditText");

        public AndroidElement PhoneNumber => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[6]/android.widget.EditText");

        public AndroidElement CountryCode => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[5]");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[7]/android.widget.Button");

        public AndroidElement CancelLink => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[8]/android.widget.Button");

        #endregion

        #region Methods
        public AddNewMember ClickCancelLink()
        {
            CancelLink.Click();
            return GetInstance<AddNewMember>();
        }

        public AddNewMember ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<AddNewMember>();
        }

        public bool IsAddNewMemberScreenDisplayed()
        {
            return Title.Displayed;
        }

        public AddNewMember EnterFirstName(string firstName)
        {
            FirstName.Click();
            FirstName.SendKeys(firstName);
            return GetInstance<AddNewMember>();
        }

        public AddNewMember EnterLastName(string lastName)
        {
            LastName.Click();
            LastName.SendKeys(lastName);
            return GetInstance<AddNewMember>();
        }

        public AddNewMember EnterDateOfBirth(string dateOfBirth)
        {
            DateOfBirth.Click();
            DateOfBirth.SendKeys(dateOfBirth);
            return GetInstance<AddNewMember>();
        }

        public AddNewMember EnterEmail(string email)
        {
            Email.Click();
            Email.SendKeys(email);
            return GetInstance<AddNewMember>();
        }

        public AddNewMember EnterPhoneNumber(string phoneNumber)
        {
            PhoneNumber.Click();
            PhoneNumber.SendKeys(phoneNumber);
            return GetInstance<AddNewMember>();
        }
        #endregion
    }
}
