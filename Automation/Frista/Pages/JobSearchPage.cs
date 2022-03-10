using Automation.WebDriver;
using OpenQA.Selenium;

namespace Pages
{
    public class JobSearchPage
    {
        public const string Category_All = "//*[@class='skk_filters_dd_reg']//option[contains(text(), 'All')]";
        public const string Category_ITSAP = "//*[@class='skk_filters_dd_reg']//option[contains(text(), 'IT / SAP')]";
        public const string SearchButton = "//input[@class='skk_filters_btn_search']";
        public const string CareersUrl = "https://www.frista.com/en/career/";

        public JobSearchPage ChooseCategoryAll()
        {
            UIDriver.FindElementByXpath(Category_All).Click();
            return this;
        }

        public JobSearchPage ChooseCategoryITSAP()
        {
            UIDriver.FindElementByXpath(Category_ITSAP).Click();
            return this;
        }

        public JobSearchPage ClickSearchButton()
        {
            UIDriver.FindElementByXpath(SearchButton).Click();
            return this;
        }
    }
}
