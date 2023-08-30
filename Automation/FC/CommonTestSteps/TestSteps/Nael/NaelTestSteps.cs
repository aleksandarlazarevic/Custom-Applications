using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps.Nael
{
    public class NaelTestSteps : GlobalTestSteps, INaelTestSteps
    {
        public void GoToNael()
        {
            TestInMemoryParameters.Instance.Url = "https://playnael.com";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }
    }
}
