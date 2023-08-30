using OpenQA.Selenium;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace UIMappings.Pages.Fcos.Modules.Automation
{
    public class Blueprints : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Create')]")]
        public IWebElement CreateButton { get; set; }
        #endregion
        #region Methods
        public Blueprints ClickCreateButton()
        {
            CreateButton.ClickWrapper("CreateButton");
            return this;
        }
        #endregion
    }
}
