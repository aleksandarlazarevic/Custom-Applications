using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps.FranchiCzar
{
    public class FranchiCzarTestSteps : GlobalTestSteps, IFranchiCzarTestSteps
    {
        public void GoToFranchiCzar()
        {
            TestInMemoryParameters.Instance.Url = "https://franchiczar.com";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }
    }
}
