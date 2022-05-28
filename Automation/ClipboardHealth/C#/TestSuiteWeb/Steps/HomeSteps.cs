using OpenQA.Selenium;
using SeleniumCore.Helpers.Utilities;
using SeleniumCore.WebDriver;
using SeleniumExtras.PageObjects;
using TestSuiteWeb.SharedSteps.Contracts;
using UIMappings.Pages;

namespace TestSuiteWeb.Steps
{
    public class HomeSteps : IHomeSteps
    {
        [FindsBy(How = How.XPath, Using = "//li/a[div[contains(text(), 'TV, Appliances, Electronics')]]")]
        public IWebElement TvAppliancesElectronics { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[span[contains(text(), 'Brands')]]")]
        public IWebElement BrandsSection { get; set; }

        string _newWindowHandle;

        public void CheckSamsungCheckbox()
        {
            JavaScriptHelper.MoveScrollTillElementIsFound(UIDriver.WebDriver, BrandsSection);

            UIDriver.WebDriver.GetPage<Home>()
                .CheckSamsungCheckbox()
                .ImplicitlyWaitForPageToBeReady<Home>(3);
        }

        public void ChooseSecondPricedItem()
        {
            UIDriver.WebDriver.GetPage<Home>()
                .ChooseSecondPricedItem()
                .ImplicitlyWaitForPageToBeReady<Home>(3);
        }

        public void ClickHamburgerMenu()
        {
            UIDriver.WebDriver.GetPage<Home>()
                            .WaitForPageToBeReady<Home>(By.Id(Home.AMAZON_LOGO), "Home Page")
                            .ClickOnHamburgerMenu();
        }

        public void ClickTelevisions()
        {
            UIDriver.WebDriver.GetPage<Home>()
                .ClickOnTelevisions()
                .ImplicitlyWaitForPageToBeReady<Home>(3);
        }

        public void ClickTvAppliancesElectronics()
        {
            JavaScriptHelper.MoveScrollTillElementIsFound(UIDriver.WebDriver, TvAppliancesElectronics);

            UIDriver.WebDriver.GetPage<Home>()
                .ClickOnTvAppliancesElectronics();
        }

        public void SortHighToLow()
        {
            UIDriver.WebDriver.GetPage<Home>()
                .SortHighToLow("Price: High to Low")
                .ImplicitlyWaitForPageToBeReady<Home>(3);
        }

        public void SwitchTheWindow()
        {
            _newWindowHandle = UIDriver.WebDriver.CurrentWindowHandle;

            UIDriver.WebDriver.SwitchTo().Window(_newWindowHandle);
        }

        public void VerifyAboutThisItem()
        {
            UIDriver.WebDriver.GetPage<Home>()
                .VerifyAboutThis("About this item");
        }
    }
}
