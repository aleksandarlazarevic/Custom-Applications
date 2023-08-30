using CommonTestSteps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using System.Threading;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Steps.FcosAzure.Automation
{
    [Binding]
    public class Workflows : BaseTest
    {
        [Then(@"Go to Automation Workflows")]
        public void ThenGoToAutomationWorkflows()
        {
            RunStep(SharedSteps.Containers.FcosAzure.NavigateToAutomationWorkflows,
                    new TestStepInfo("[AUTOMATION] - Navigate to Automation Workflows", false, Importance.High));
        }

        [Then(@"Click Create")]
        public void ThenClickCreate()
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.ClickCreateWorkflowButton,
                    new TestStepInfo("[AUTOMATION WORKFLOWS] - Click Create Workflow button", false, Importance.High));
        }

        [Then(@"Select first option from Workflow company dropdown")]
        public void ThenSelectFirstOptionFromWorkflowCompanyDropdown()
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.SelectFirstOptionFromWorkflowCompanyDropdown,
                    new TestStepInfo("[AUTOMATION WORKFLOWS] - Select first option from Workflow company dropdown menu", false, Importance.High));
        }

        [Then(@"Select (.*) as a workflow company")]
        public void ThenSelectAWorkflowCompany(string company)
        {
            Assert.IsFalse(string.IsNullOrEmpty(company));
            RunStep(SharedSteps.Containers.AutomationTestSteps.SelectWorkflowCompany, company,
                    new TestStepInfo(string.Format("[AUTOMATION WORKFLOWS] - Select {0} as a workflow company", company), false, Importance.High));
        }

        [Then(@"Set the workflow to Trigger When (.*)")]
        public void ThenSetTheWorkflowTrigger(string trigger)
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.SelectWorkflowTrigger, trigger,
                    new TestStepInfo(string.Format("[AUTOMATION WORKFLOWS] - Select workflow trigger: {0}", trigger), false, Importance.High));
        }

        [Then(@"Add an action (.*)")]
        public void ThenAddAnAction(string action)
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.AddAnAction, action,
                    new TestStepInfo(string.Format("[AUTOMATION WORKFLOWS] - Add an action"), false, Importance.High));
        }

        [Then(@"Save Workflow (.*)")]
        public void ThenSaveWorkflow(string description)
        {
            RunStep(SharedSteps.Containers.AutomationTestSteps.SaveWorkflow, description,
                    new TestStepInfo(string.Format("[AUTOMATION WORKFLOWS] - Save Workflow"), false, Importance.High));
        }


    }
}
