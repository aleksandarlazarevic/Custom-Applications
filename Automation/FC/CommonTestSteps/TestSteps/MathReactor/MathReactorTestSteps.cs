using CommonTestSteps.Contracts;
using SeleniumCore;

namespace CommonTestSteps.TestSteps.MathReactor
{
    public class MathReactorTestSteps : GlobalTestSteps, IMathReactorTestSteps
    {
        public void GoToMathreactor()
        {
            TestInMemoryParameters.Instance.Url = "https://mathreactor.co";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }
    }
}
