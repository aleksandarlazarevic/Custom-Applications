using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps.Valhallan
{
    public class ValhallanTestSteps : GlobalTestSteps, IValhallanTestSteps
    {
        public void GoToValhallan()
        {
            TestInMemoryParameters.Instance.Url = "https://valhallan.com";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {
        }
    }
}
