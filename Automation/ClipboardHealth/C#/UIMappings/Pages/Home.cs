using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using SeleniumExtras.PageObjects;
using System;

namespace UIMappings.Pages
{
    public class Home : BasePage
    {
        public const string AMAZON_LOGO = "//*[contains(@id, 'nav-logo')]";

        [FindsBy(How = How.XPath, Using = "//*[@id='nav-hamburger-menu']")]
        public IWebElement HamburgerMenu { get; set; }

        [FindsBy(How = How.XPath, Using = "//li/a[div[contains(text(), 'TV, Appliances, Electronics')]]")]
        public IWebElement TvAppliancesElectronics { get; set; }

        [FindsBy(How = How.XPath, Using = "//li/a[contains(text(), 'Televisions')]")]
        public IWebElement Televisions { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[span[contains(text(), 'Samsung')]]/div/label/input")]
        public IWebElement SamsungCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='s-result-sort-select']")]
        public IWebElement SortDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 's-main-slot s-result-list s-search-results sg-row')]/div[3]")]
        public IWebElement SecondPricedItem { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='feature-bullets']")]
        public IWebElement FeatureBullets { get; set; }

        public Home ClickOnHamburgerMenu()
        {
            HamburgerMenu.ClickWrapper("Hamburger Menu");
            return this;
        }

        public Home CheckSamsungCheckbox()
        {
            SamsungCheckbox.CheckboxWrapper(true, "Samsung checkbox");
            return this;
        }

        public Home ClickOnTvAppliancesElectronics()
        {
            TvAppliancesElectronics.ClickWrapper("Tv Appliances Electronics");
            return this;
        }

        public Home ClickOnTelevisions()
        {
            Televisions.ClickWrapper("Televisions");
            return this;
        }

        public Home SortHighToLow(string value)
        {
            SortDropdown.SelectComboboxValue(value, "Sort high to low");
            return this;
        }

        public Home ChooseSecondPricedItem()
        {
            SecondPricedItem.ClickWrapper("Second priced item");
            return this;
        }

        public Home VerifyAboutThis(string textToVerify)
        {
            string featureText = FeatureBullets.SafeValue();
            Assert.IsTrue(featureText.Contains(textToVerify), $"Feature bullets do not contain expected text: [{textToVerify}]");
            Console.WriteLine(string.Format("Feature bullets contain text: {0}", textToVerify));
            return this;
        }
    }
}
