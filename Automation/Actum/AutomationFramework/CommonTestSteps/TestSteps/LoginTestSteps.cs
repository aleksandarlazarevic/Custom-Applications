using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps
{
    public class LoginTestSteps : GlobalTestSteps, ILoginTestSteps
    {
        public void GoToDemoblazeWebsite()
        {
            TestInMemoryParameters.Instance.Url = "https://www.demoblaze.com/";
            OpenBrowser();
        }
    }
}
