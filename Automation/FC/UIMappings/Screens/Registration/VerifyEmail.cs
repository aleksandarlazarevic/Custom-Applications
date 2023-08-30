using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System.Threading;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class VerifyEmail : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Logo => Helpers.GetScreenElement("//android.widget.Image[@text='Company Logo']");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Verify Your Email Address']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Please submit your email address below for account verification purposes.']");

        public AndroidElement EmailTextBox => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.FrameLayout[1]/android.webkit.WebView/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.widget.ListView/android.view.View/android.view.View[2]/android.widget.ListView/android.view.View/android.view.View/android.widget.EditText");

        public AndroidElement SendVerificationCodeButton => Helpers.GetScreenElement("//android.widget.Button[@text='Send verification code']");

        public AndroidElement CancelLink => Helpers.GetScreenElement("//android.widget.Button[@text='Cancel']");

        public AndroidElement LookForTheEmailInstructions => Helpers.GetScreenElement("//android.view.View[@text='Please look for the email from FranchiCzar and enter the 6-digit verification code below.']");

        public AndroidElement VerificationEmail => Helpers.GetScreenElement("//android.widget.FrameLayout/android.widget.FrameLayout[1]/android.webkit.WebView/android.view.View/android.view.View[1]/android.view.View/android.view.View[1]/android.widget.ListView/android.view.View/android.view.View[2]/android.widget.ListView/android.view.View[1]/android.view.View/android.widget.EditText");

        public AndroidElement VerificationCodeTextbox => Helpers.GetScreenElement("(//android.widget.EditText[@clickable=true()])[2]");

        public AndroidElement VerifyCodeButton => Helpers.GetScreenElement("//android.widget.ListView/android.view.View/android.view.View/android.view.View[2]/android.widget.Button");

        public AndroidElement SendNewCodeButton => Helpers.GetScreenElement("//android.widget.Button[@text='Send New Code']");

        public AndroidElement SuccessIndicatorLabel => Helpers.GetScreenElement("//android.widget.TextView[@text='Success!']");

        public AndroidElement EmailVerifiedIndicatorLabel => Helpers.GetScreenElement("//android.view.View[@text='Your email address has been verified, please select \u2018Continue\u2019 to proceed.']");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.Button[@text='Continue']");

        public AndroidElement CurrentYearLabel => Helpers.GetScreenElement("//android.widget.TextView[@text='2022']");

        public AndroidElement CopyrightLabel => Helpers.GetScreenElement("//android.widget.TextView[normalize-space(@text)='FranchiCzar, LLC. All rights reserved.']");

        #endregion

        #region Methods
        public VerifyEmail ClickSendVerificationCodeButton()
        {
            SendVerificationCodeButton.Click();
            Thread.Sleep(5000);
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail ClickCancelLink()
        {
            CancelLink.Click();
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail ClickVerifyCodeButton()
        {
            VerifyCodeButton.Click();
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail ClickSendNewCodeButton()
        {
            SendNewCodeButton.Click();
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail EnterEmailAddress(string email)
        {
            EmailTextBox.Click();
            EmailTextBox.SendKeys(email);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);
            return GetInstance<VerifyEmail>();
        }

        public VerifyEmail EnterVerificationCode(string code)
        {
            VerificationCodeTextbox.Click();
            VerificationCodeTextbox.SendKeys(code);
            return GetInstance<VerifyEmail>();
        }

        public bool IsVerifyEmailScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
