using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;
using Utilities.Android;
using AppiumCore.Config;

namespace UIMappings.Screens.Registration
{
    public class RegistrationScreen : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement AddPicture => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement AddPictureLinkText => Helpers.GetScreenElement("//android.widget.TextView[@text='Add Picture (Required)']");

        public AndroidElement CameraButton;

        //public AndroidElement GalleryButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup[2]/android.view.ViewGroup[2]");

        public AndroidElement PicturesAndVideoQuestion => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.ScrollView/android.widget.TextView");
        public AndroidElement AllowPicturesAndVideo => Helpers.GetScreenElement("//android.widget.Button[@text='Allow']");
        public AndroidElement DenyPicturesAndVideo => Helpers.GetScreenElement("//android.widget.Button[@text='Deny']");
        public AndroidElement DenyAndDontAskPicturesAndVideo => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.ScrollView/android.widget.Button[3]");
        #region Android 11
        public AndroidElement AllowPicturesAndVideoAndroid11 => Helpers.GetScreenElement("//android.widget.Button[@text='While using the app']");
        public AndroidElement TurnOnLocationLogs => Helpers.GetScreenElement("//android.widget.Button[@text='Turn on']");
        public AndroidElement PhotoOk => Helpers.GetScreenElement("//android.view.ViewGroup/android.widget.LinearLayout/android.widget.Button[2]");
        public AndroidElement CameraTakePhotoButtonAndroid11 => Helpers.GetScreenElement("//android.widget.RelativeLayout/android.widget.RelativeLayout/android.widget.RelativeLayout/android.widget.ImageButton");
        public AndroidElement EditPhotoAcceptAndroid11 => Helpers.GetScreenElement("//android.widget.RelativeLayout/android.view.ViewGroup/androidx.appcompat.widget.LinearLayoutCompat/android.widget.TextView");
        #endregion


        public AndroidElement CameraTakePhotoButton => Helpers.GetScreenElement("//android.widget.ImageButton[@content-desc='Take photo']");
        public AndroidElement CameraAcceptPhotoButton => Helpers.GetScreenElement("//android.widget.ImageButton[@content-desc='Take photo']");
        public AndroidElement EditPhotoAccept => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.TextView[2]");

        public AndroidElement PhotosAndMediaQuestion => Helpers.GetScreenElement("//android.widget.TextView[@text='Allow Iron 24 to access photos and media on your device?']");
        public AndroidElement AllowPhotosAndMedia => Helpers.GetScreenElement("//android.widget.Button[@text='Allow']");
        public AndroidElement DenyPhotosAndMedia => Helpers.GetScreenElement("//android.widget.Button[@text='Deny']");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Personal details']");

        public AndroidElement FirstName => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.EditText");
        public AndroidElement FirstNameLabel => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[3]");

        public AndroidElement LastName => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.widget.EditText");
        public AndroidElement LastNameLabel => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[5]");

        public AndroidElement DateOfBirth => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[4]/android.widget.EditText");
        public AndroidElement DateOfBirthLabel => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[7]");

        public AndroidElement Email => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.EditText[4]");
        public AndroidElement EmailLabel => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[9]");

        public AndroidElement CountryPrefix => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]");
        public AndroidElement FlagIcon => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]/android.widget.TextView[1]");
        public AndroidElement CountryCode => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[2]/android.widget.TextView[2]");
        public AndroidElement CountryListFirstCountry => Helpers.GetScreenElement("//*[(@class='android.widget.TextView') and (@text='United States (+1)')]");
        public AndroidElement CountryCodeButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[6]");
        public AndroidElement CountryCodeLabel => Helpers.GetScreenElement("//android.view.View[@text='Country Code']");
        public AndroidElement CountryCodeDropdown => Helpers.GetScreenElement("//android.view.View[@text='Country/Region']");
        public AndroidElement CountryCodeSearchbox => Helpers.GetScreenElement("//android.widget.EditText");
        public AndroidElement CountryCodeSearchedCountry => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup");

        public AndroidElement PhoneNumber => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[7]/android.widget.EditText");
        public AndroidElement PhoneNumberLabel => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[11]");

        public AndroidElement AgreeWithTheTermsCheckbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[8]/android.view.ViewGroup[1]");
        public AndroidElement TermsAndConditionsLink => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup[3]");

        public AndroidElement NextButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[9]/android.widget.Button");

        public AndroidElement CancelButton => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.Button[2]");

        public AndroidElement ProfileImageErrorTitle => Helpers.GetScreenElement("//android.widget.TextView[@text='Profile image error']");
        public AndroidElement ProfileImageErrorInstructions => Helpers.GetScreenElement("//android.widget.TextView[normalize-space(@text)='For security reasons, you need to upload a picture of your face to create your account.']");
        public AndroidElement ProfileImageErrorContactSupport => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.Button[1]");
        public AndroidElement ProfileImageErrorTryAgain => Helpers.GetScreenElement("//android.widget.TextView[@text='Try again']");
        #endregion

        #region Methods
        public RegistrationScreen TakeAProfilePicture()
        {
            AddPicture.Click();
            DeviceSpecificPhotoTaking(Settings.DeviceName);

            return GetInstance<RegistrationScreen>();
        }

        #region Device specific photo taking
        private void DeviceSpecificPhotoTaking(string deviceName)
        {
            switch (deviceName)
            {
                case "Samsung Galaxy S21":
                    SamsungGalaxyPhotoWorkflow();
                    break;
                case "Google Pixel 3":
                    GooglePixelPhotoWorkflow();
                    break;
                default:
                    break;
            }
        }

        private void SamsungGalaxyPhotoWorkflow()
        {
            CameraButton = Helpers.GetScreenElement("//android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]");
            CameraButton.Click();
            AllowPicturesAndVideoAndroid11.Click();
            CameraTakePhotoButtonAndroid11.Click();
            Thread.Sleep(5000);
            PhotoOk.Click();
            Thread.Sleep(3000);
            EditPhotoAcceptAndroid11.Click();
            Thread.Sleep(3000);
        }

        private void GooglePixelPhotoWorkflow()
        {
            CameraButton = Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[1]");
            CameraButton.Click();
            AllowPicturesAndVideo.Click();
            AllowPhotosAndMedia.Click();
            AddPicture.Click();
            CameraButton.Click();
            AllowPhotosAndMedia.Click();

            CameraTakePhotoButton.Click();
            Thread.Sleep(5000);

            CameraAcceptPhotoButton.Click();
            Thread.Sleep(3000);

            EditPhotoAccept.Click();
            Thread.Sleep(3000);
        }
        #endregion

        public RegistrationScreen EnterFirstName(string firstName)
        {
            FirstName.Click();
            FirstName.SendKeys(firstName);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen EnterLastName(string lastName)
        {
            LastName.Click();
            LastName.SendKeys(lastName);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen EnterDateOfBirth(string dateOfBirth)
        {
            DateOfBirth.Click();
            DateOfBirth.SendKeys(dateOfBirth);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen EnterEmailAddress(string email)
        {
            Email.Click();
            Email.SendKeys(email);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<RegistrationScreen>();
        }

        public MultiFactorAuthentication SelectSpecificCountryCode(string country)
        {
            CountryCodeButton.Click();
            CountryCodeSearchbox.Click();
            CountryCodeSearchbox.SendKeys(country);
            CountryCodeSearchedCountry.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public RegistrationScreen EnterPhoneNumber(string phoneNumber)
        {
            PhoneNumber.SendKeys(phoneNumber);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen AgreeWithTermsAndsConditions()
        {
            AgreeWithTheTermsCheckbox.Click();
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen ClickNextButton()
        {
            NextButton.Click();
            return GetInstance<RegistrationScreen>();
        }

        public RegistrationScreen ClickCancelButton()
        {
            CancelButton.Click();
            return GetInstance<RegistrationScreen>();
        }

        public bool IsRegistrationScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
