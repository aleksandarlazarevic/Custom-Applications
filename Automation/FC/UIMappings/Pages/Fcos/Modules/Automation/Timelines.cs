using OpenQA.Selenium;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using System;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace UIMappings.Pages.Fcos.Modules.Automation
{
    public class Timelines : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Create A Timeline')]")]
        public IWebElement CreateATimelineButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@class, 'form-control ng-pristine ng-valid ng-touched')]")]
        public IWebElement TimelineName { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@class, 'form-control ng-pristine ng-valid ng-touched')]")]
        public IWebElement TimelineDescription { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[contains(@class, 'form-control ng-pristine ng-valid ng-touched')]")]
        public IWebElement TypeOfCompany { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Create Timeline')]")]
        public IWebElement CreateTimeline { get; set; }
        #endregion
        #region Methods
        public Timelines ClickCreateATimelineButton()
        {
            CreateATimelineButton.ClickWrapper("CreateATimelineButton");
            return this;
        }

        public Timelines EnterTimelineName(string name)
        {
            TimelineName.SendKeys(name);
            return this;
        }

        public Timelines EnterTimelineDescription(string description)
        {
            TimelineDescription.SendKeys(description);
            return this;
        }

        public Timelines SetTypeOfCompany(string typeOfCompany)
        {
            TypeOfCompany.SelectComboboxValue(typeOfCompany, "TypeOfCompany");
            return this;
        }

        public Timelines ClickCreateTimeline()
        {
            CreateTimeline.ClickWrapper("CreateTimeline");
            return this;
        }

        public void DeleteRecentlyCreatedTimeline(string name)
        {
        }
        #endregion
    }
}
