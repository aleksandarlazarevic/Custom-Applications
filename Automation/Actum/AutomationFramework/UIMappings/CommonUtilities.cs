using SeleniumCore.WebDriver;

namespace UIMappings
{
    public class CommonUtilities
    {
        public static string GetAlertMessage()
        {
            Thread.Sleep(1000);
            string message = UIDriver.WebDriver.SwitchTo().Alert().Text;
            UIDriver.WebDriver.SwitchTo().Alert().Dismiss();

            return message;
        }
    }
}
