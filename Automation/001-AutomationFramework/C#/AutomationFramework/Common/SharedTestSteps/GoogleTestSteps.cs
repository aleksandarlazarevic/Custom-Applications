using CommonCore.Tests;
using SharedTestSteps.Contracts;

namespace SharedTestSteps
{
    public class GoogleTestSteps : CommonBrowserActions, IGoogleTestSteps
    {
        public void GoToGoogle()
        {
            TestInMemoryParameters.Instance.Url = "https://www.google.com/";
            OpenBrowserAndGoToDefaultUrl();
        }
    }
}
