using CommonTestSteps.Contracts;
using CommonTestSteps.Contracts.FcosAzure.Automation;
using SeleniumCore;
using SeleniumCore.WebDriver;
using UIMappings.Pages.Fcos;
using UIMappings.Pages.Fcos.Modules;
using UIMappings.Pages.Fcos.Modules.Automation;

namespace CommonTestSteps.TestSteps.FcosAzure.Automation
{
    public class AutomationTestSteps : GlobalTestSteps, IAutomationTestSteps
    {
        public void CreateATimeline()
        {
            UIDriver.WebDriver.GetPage<Timelines>()
                          .ClickCreateATimelineButton()
                          .EnterTimelineName("TestTimeline")
                          .EnterTimelineDescription("TestDescription")
                          .SetTypeOfCompany(TestInMemoryParameters.Instance.TypeOfCompany)
                          .ClickCreateTimeline();
        }

        public void DeleteCreatedTimeline()
        {
            UIDriver.WebDriver.GetPage<Timelines>()
                          .DeleteRecentlyCreatedTimeline("TestTimeline");
        }

        public void ClickCreateWorkflowButton()
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .ClickCreateButton();
        }

        public void SelectFirstOptionFromWorkflowCompanyDropdown()
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .SelectFirstWorkflowCompany();
        }

        public void SelectWorkflowCompany(string company)
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .SelectWorkflowCompany(company);
        }

        public void SelectWorkflowTrigger(string trigger)
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .SelectWorkflowTrigger(trigger);
        }

        public void AddAnAction(string action)
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .ClickAddAnAction()
                          .SetImmediateAction(action)
                          .EnterDescription("Test Description: ");
        }

        public void SaveWorkflow(string description)
        {
            UIDriver.WebDriver.GetPage<WorkflowsPage>()
                          .EnterWorkflowName("TestWorkflow-" + description)
                          .ClickSaveButton();
        }
    }
}
