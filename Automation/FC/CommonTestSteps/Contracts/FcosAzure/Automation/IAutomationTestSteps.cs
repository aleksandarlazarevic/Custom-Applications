namespace CommonTestSteps.Contracts.FcosAzure.Automation
{
    public interface IAutomationTestSteps
    {
        void AddAnAction(string action);
        void ClickCreateWorkflowButton();
        void CreateATimeline();
        void DeleteCreatedTimeline();
        void SelectFirstOptionFromWorkflowCompanyDropdown();
        void SelectWorkflowCompany(string company);
        void SelectWorkflowTrigger(string trigger);
        void SaveWorkflow(string description);
    }
}
