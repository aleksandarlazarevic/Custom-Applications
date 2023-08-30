using CommonTestSteps;
using SeleniumCore;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using System.Threading;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Steps.FcosAzure.Automation
{
    [Binding]
    public class Timelines : BaseTest
    {
        [Then(@"Go to Automation Timelines")]
        public void ThenGoToAutomationTimelines()
        {
            RunStep(SharedSteps.Containers.FcosAzure.NavigateToAutomationTimelines,
                    new TestStepInfo("[AUTOMATION] - Navigate to Automation Timelines", false, Importance.High));
        }

        [Then(@"Create a timeline assigned to (.*)")]
        public void ThenCreateATimelineAssignedToBroker(string typeOfCompany)
        {
            TestInMemoryParameters.Instance.TypeOfCompany = typeOfCompany;
            RunStep(SharedSteps.Containers.AutomationTestSteps.CreateATimeline,
                    new TestStepInfo("[AUTOMATION TIMELINES] - Click Create A Timeline button", false, Importance.High));
            Thread.Sleep(5000);
        }

        [Then(@"Delete created timeline")]
        public void ThenDeleteCreatedTimeline()
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.DeleteCreatedTimeline,
                    new TestStepInfo("[AUTOMATION TIMELINES] - Delete Created Timeline", false, Importance.High));
        }


    }
}
