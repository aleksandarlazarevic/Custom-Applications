using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps.Iron24
{
    public class Iron24TestSteps : GlobalTestSteps, IIron24TestSteps
    {
        public void GoToIron24()
        {
            TestInMemoryParameters.Instance.Url = "https://iron24.com";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }
    }
}
