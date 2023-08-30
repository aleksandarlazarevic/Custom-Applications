using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using SeleniumCore.WebDriver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace UIMappings.Pages.Fcos.Modules
{
    public class WorkflowsPage : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'btn btn-primary m-1')]")]
        public IWebElement CreateButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='dropdownMenu1']")]
        public IWebElement WorkflowCompanyDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[contains(@class, 'dropdown-item pointer ng-star-inserted')][1]")]
        public IWebElement FirstWorkflowCompany { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@class, 'parent-search')]")]
        public IWebElement WorkflowCompanySearchbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[contains(@class, 'dropdown-item')]")]
        public IWebElement DropdownItems { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[contains(@class, 'form-control ng-pristine ng-valid ng-touched')]")]
        public IWebElement WorkflowTriggerDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Add an action')]")]
        public IWebElement AddAnActionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[contains(@class, 'form-control ng-untouched ng-pristine ng-valid')]")]
        public IWebElement ImmediateActionDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'fr-element fr-view')]")]
        public IWebElement DescriptionTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'btn btn-primary text-white mx-1')]")]
        public IWebElement SaveButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'btn btn-secondary mr-1 ng-star-inserted')]")]
        public IWebElement SaveDraftButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'btn btn-warning ng-star-inserted')]")]
        public IWebElement DeactivateButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Automation')]/../input")]
        public IWebElement WorkflowNameTextbox { get; set; }
        
        #endregion
        #region Methods
        public WorkflowsPage ClickCreateButton()
        {
            CreateButton.ClickWrapper("CreateButton");
            return this;
        }

        public WorkflowsPage SelectFirstWorkflowCompany()
        {
            WorkflowCompanyDropdown.ClickWrapper("WorkflowCompanyDropdown");
            FirstWorkflowCompany.ClickWrapper("FirstWorkflowCompany");
            return this;
        }

        public WorkflowsPage SelectWorkflowCompany(string company)
        {
            WorkflowCompanyDropdown.ClickWrapper("WorkflowCompanyDropdown");
            WorkflowCompanySearchbox.SendKeys(company);
            ReadOnlyCollection<IWebElement> dropdownItems = WorkflowCompanyDropdown.FindElements(By.XPath("//li[contains(@class, 'dropdown-item')]"));
            Assert.Greater(dropdownItems.Count, 0, "No matching workflow companies were found");

            Assert.IsTrue(dropdownItems.Any(p => p.Text.Contains(company)), "Filtering companies returned results but none were with the same company name of: " + company);

            IWebElement matchedItem = dropdownItems.Where(p => p.Text.Contains(company)).First();
            matchedItem.ClickWrapper("First match for Company: " + company);

            return this;
        }

        public WorkflowsPage SelectWorkflowTrigger(string trigger)
        {
            IWebElement element = UIDriver.WebDriver.FindElement(By.XPath(string.Format("//option[contains(text(), '{0}')]", trigger)));
            element.ClickWrapper("WorkflowDropdownTrigger");

            return this;
        }

        public WorkflowsPage ClickAddAnAction()
        {
            AddAnActionButton.ClickWrapper("AddAnActionButton");
            return this;
        }

        public WorkflowsPage SetImmediateAction(string action)
        {
            ImmediateActionDropdown.SelectComboboxValue(action, "AddAnActionButton");
            return this;
        }

        public WorkflowsPage EnterDescription(string description)
        {
            DescriptionTextBox.SendKeys(description);

            //insert customer fields
            foreach (string field in new List<string> { "First name", "Last name", "Full name" })
            {
                IWebElement insertCustomerField = UIDriver.WebDriver.FindElement(By.XPath(string.Format("//button[contains(text(), '{0}')]", field)));
                insertCustomerField.ClickWrapper("insertCustomerField");
            }

            //insert facility fields
            foreach (string field in new List<string> { "Company name", "Phone number"})
            {
                IWebElement insertFacilityField = UIDriver.WebDriver.FindElement(By.XPath(string.Format("//button[contains(text(), '{0}')]", field)));
                insertFacilityField.ClickWrapper("insertFacilityField");
            }

            return this;
        }

        public WorkflowsPage EnterWorkflowName(string name)
        {
            WorkflowNameTextbox.SendKeys(name);
            return this;
        }

        public WorkflowsPage ClickSaveButton()
        {
            SaveButton.ClickWrapper("SaveButton");
            return this;
        }
        #endregion
    }
}
