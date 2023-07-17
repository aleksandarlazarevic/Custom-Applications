using TechTalk.SpecFlow;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class CommonTestSteps
    {
        [Given(@"(.*) webpage is started")]
        public void GivenWebpageIsStarted(string pageName)
        {
            switch (pageName)
            {
                case "Google":
                    //RunStep(SharedSteps.Containers.Google.GoToGoogle,
                    //        new TestStepInfo("Go to Google", false, Priority.High));
                    break;
                default:
                    break;
            }
        }

        [Then(@"(.*) is entered in the searchbox")]
        public void ThenIsEnteredInTheSearchbox(string searchText)
        {
            throw new PendingStepException();
        }
    }
}
