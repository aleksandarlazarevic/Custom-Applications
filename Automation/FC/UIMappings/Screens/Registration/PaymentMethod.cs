using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using System.Threading;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class PaymentMethod : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement BackButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[1]");

        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Payment method']");

        public AndroidElement CardNumberTextbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[4]/android.widget.EditText");

        public AndroidElement ExpirationDateTextbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[5]/android.widget.EditText");

        public AndroidElement CardholderTextbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[6]/android.widget.EditText");

        public AndroidElement CvcCvvNumberTextbox => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[7]/android.widget.EditText");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.TextView[13]");

        public AndroidElement ContinueButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[7]/android.widget.Button");

        #endregion

        #region Methods
        public PaymentMethod ClickBackButton()
        {
            BackButton.Click();
            return GetInstance<PaymentMethod>();
        }

        public PaymentMethod ClickContinueButton()
        {
            ContinueButton.Click();
            return GetInstance<PaymentMethod>();
        }

        public PaymentMethod EnterCardNumber(string number)
        {
            CardNumberTextbox.Click();
            CardNumberTextbox.SendKeys(number);
            return GetInstance<PaymentMethod>();
        }

        public PaymentMethod EnterExpirationDate(string date)
        {
            ExpirationDateTextbox.Click();
            ExpirationDateTextbox.SendKeys(date);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Enter);
            return GetInstance<PaymentMethod>();
        }

        public PaymentMethod EnterCardholder(string cardholder)
        {
            Thread.Sleep(1000);
            CardholderTextbox.Click();
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_F, AndroidKeyCode.MetaShift_LEFT_ON);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_R);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_A);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_N);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_C);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_H);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_I);

            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_SPACE);

            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_T, AndroidKeyCode.MetaShift_LEFT_ON);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_E);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_S);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_T);

            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);

            return GetInstance<PaymentMethod>();
        }

        public PaymentMethod EnterCvcCvvNumber(string number)
        {
            Thread.Sleep(1000);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_1);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_2);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Keycode_3);
            AppiumDriver.PressKeyCode(AndroidKeyCode.Enter);

            return GetInstance<PaymentMethod>();
        }

        public bool IsPaymentMethodScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
