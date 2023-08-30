using CommonTestSteps.Contracts;
using SeleniumCore;
using SeleniumCore.WebDriver;
using UIMappings.Pages.Fcos;
using UIMappings.Pages.Fcos.Modules;
using UIMappings.Pages.Fcos.Modules.Automation;

namespace CommonTestSteps.TestSteps.FcosAzure
{
    public class FcosAzureTestSteps : GlobalTestSteps, IFcosAzureTestSteps
    {
        public void GoToFCOSAzure()
        {
            TestInMemoryParameters.Instance.Url = "https://fcos-uat-app.azurewebsites.net/";
            OpenBrowser();
        }

        public void ValidateFirstPageLoaded()
        {

        }

        public void NavigateToAutomationTimelines()
        {
                UIDriver.WebDriver.GetPage<FcosDashboard>()
                              .ClickAutomationTimelines();
        }

        public void NavigateToAutomationWorkflows()
        {
            UIDriver.WebDriver.GetPage<FcosDashboard>()
                          .ClickAutomationWorkflows();
        }        
    }
}
