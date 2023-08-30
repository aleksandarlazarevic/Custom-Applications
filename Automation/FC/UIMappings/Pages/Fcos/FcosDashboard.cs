using OpenQA.Selenium;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace UIMappings.Pages.Fcos
{
    public class FcosDashboard : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//img[@alt='Logo']")]
        public IWebElement FranchiCzarLogo { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Automation')]")]
        public IWebElement AutomationDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Workflows')]")]
        public IWebElement AutomationWorkflows { get; set; }
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Blueprints')]")]
        public IWebElement AutomationBlueprints { get; set; }
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Timelines')]")]
        public IWebElement AutomationTimelines { get; set; }
        #endregion
        #region Methods
        public FcosDashboard ClickCreateButton()
        {
            AutomationWorkflows.ClickWrapper("AutomationWorkflows");
            return this;
        }

        public FcosDashboard ClickAutomationWorkflows()
        {
            AutomationDropdown.ClickWrapper("AutomationDropdown");

            AutomationWorkflows.ClickWrapper("AutomationWorkflows");
            return this;
        }
        public FcosDashboard ClickAutomationBlueprints()
        {
            AutomationDropdown.ClickWrapper("AutomationDropdown");

            AutomationBlueprints.ClickWrapper("AutomationBlueprints");
            return this;
        }
        public FcosDashboard ClickAutomationTimelines()
        {
            AutomationDropdown.ClickWrapper("AutomationDropdown");

            AutomationTimelines.ClickWrapper("AutomationTimelines");
            return this;
        }
        #endregion
    }
}
