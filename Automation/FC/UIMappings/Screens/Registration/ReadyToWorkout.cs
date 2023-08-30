using AppiumCore.Base;
using OpenQA.Selenium.Appium.Android;
using Utilities.Android;

namespace UIMappings.Screens.Registration
{
    public class ReadyToWorkout : BaseScreen
    {
        #region Fields and Properties
        public AndroidElement Title => Helpers.GetScreenElement("//android.widget.TextView[@text='Ready to workout?']");

        public AndroidElement InstructionsTitle => Helpers.GetScreenElement("//android.widget.TextView[@text='You're almost there.']");

        public AndroidElement Instructions => Helpers.GetScreenElement("//android.widget.TextView[@text='Choose the membership option that's right for you.']");

        public AndroidElement LetsGoButton => Helpers.GetScreenElement("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.widget.Button");

        #endregion

        #region Methods
        public ReadyToWorkout ClickLetsGoButton()
        {
            LetsGoButton.Click();
            return GetInstance<ReadyToWorkout>();
        }

        public bool IsReadyToWorkoutScreenDisplayed()
        {
            return Title.Displayed;
        }
        #endregion
    }
}
