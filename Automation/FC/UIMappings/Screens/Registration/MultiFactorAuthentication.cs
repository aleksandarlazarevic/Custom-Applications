using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Text.RegularExpressions;
using Utilities;
using Utilities.Android;
using Utilities.OTPServices;
using Utilities.SMSServices;

namespace UIMappings.Screens.Registration
{
    public class MultiFactorAuthentication : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Logo => Helpers.GetScreenElement("//android.widget.Image[@text='Company Logo']");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Multi-factor authentication']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Enter a number below that we can send a code via SMS or phone to authenticate you.']");

        public AndroidElement CountryCodeButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[6]");
        public AndroidElement CountryCodeLabel => Helpers.GetScreenElement("//android.view.View[@text='Country Code']");
        public AndroidElement CountryCodeDropdown => Helpers.GetScreenElement("//android.view.View[@text='Country/Region']");
        public AndroidElement CountryCodeSearchbox => Helpers.GetScreenElement("//android.widget.EditText");
        public AndroidElement CountryCodeSearchedCountry => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup");
        
        public AndroidElement PhoneNumberLabel => Helpers.GetScreenElement("//android.view.View[@text='Phone Number']");

        public AndroidElement PhoneNumberTextbox => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.FrameLayout[1]/android.webkit.WebView/android.view.View/android.view.View[1]/android.view.View[1]/android.widget.EditText");

        public AndroidElement SendCodeButton => Helpers.GetScreenElement("//android.widget.Button[@text='Send Code']");

        public AndroidElement CallMeButton => Helpers.GetScreenElement("//android.widget.Button[@text='Call Me']");

        public AndroidElement CancelLink => Helpers.GetScreenElement("//android.widget.Button[@text='Cancel']");

        public AndroidElement USCountryCode => Helpers.GetScreenElement("//android.widget.CheckedTextView[@text='United States (+1)']");

        public AndroidElement SendNewCodeLink => Helpers.GetScreenElement("//android.widget.TextView[@text='click here to send a new code.']");

        public AndroidElement VerificationCodeTextbox => Helpers.GetScreenElement("//android.webkit.WebView/android.view.View/android.view.View/android.view.View[2]/android.widget.EditText");

        public AndroidElement VerifyCodeButton => Helpers.GetScreenElement("//android.widget.Button[@text='Verify Code']");
        
        #endregion

        #region Methods
        public MultiFactorAuthentication ClickCountryCodeDropdown()
        {
            CountryCodeDropdown.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication ClickSendCodeButton()
        {
            SendCodeButton.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication ClickCallMeButton()
        {
            CallMeButton.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication ClickCancelLink()
        {
            CancelLink.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication SelectUSCountryCode()
        {
            USCountryCode.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication ClickSendNewCodeLink()
        {
            SendNewCodeLink.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication EnterPhoneNumber(string number)
        {
            PhoneNumberTextbox.Click();
            PhoneNumberTextbox.SendKeys(number);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication EnterVerificationCode(string code)
        {
            VerificationCodeTextbox.Click();
            VerificationCodeTextbox.SendKeys(code);
            return GetInstance<MultiFactorAuthentication>();
        }

        public MultiFactorAuthentication ClickVerifyCodeButton()
        {
            VerifyCodeButton.Click();
            return GetInstance<MultiFactorAuthentication>();
        }

        public bool IsMultiFactorAuthenticationScreenDisplayed()
        {
            return Title.Displayed;
        }

        public void RetrieveSmsVerificationCode()
        {
            var smsBody = OtpServices.GetSmsBody();
            Regex codeRegex = new Regex(@"(?<!\d)\d{6}(?!\d)");
            MatchCollection verificationCode = codeRegex.Matches(smsBody);
            TestInMemoryParametersShared.Instance.SmsVerificationCode = verificationCode[0].ToString();
        }
        #endregion
    }
}
