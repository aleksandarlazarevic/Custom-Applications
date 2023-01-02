using OpenQA.Selenium;
using SeleniumCore.Base;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;
using SeleniumCore.Helpers.Extensions;
using SeleniumCore.WebDriver;

namespace UIMappings.Pages
{
    public class Categories : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Home')]")]
        public IWebElement HomeLink { get; set; }

        public IWebElement Category { get; set; }

        public IWebElement Model { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Add to cart')]")]
        public IWebElement AddToCartButton { get; set; }

        #endregion

        #region Methods
        public Categories ClickOnHomeLink()
        {
            HomeLink.ClickWrapper("HomeLink");
            return this;
        }

        public Categories ClickCategory(string category)
        {
            this.Category = UIDriver.WebDriver.SafeFindElement(By.XPath(string.Format("//a[contains(text(), '{0}')]", category)));
            this.Category.ClickWrapper("Category");
            return this;
        }

        public Categories AddModelToCart(string model)
        {
            this.Model = UIDriver.WebDriver.SafeFindElement(By.XPath(string.Format("//a[contains(text(), '{0}')]", model)));
            this.Model.ClickWrapper("Model");
            this.AddToCartButton.ClickWrapper("AddToCartButton");
            return this;
        }

        public Categories WaitForPageReady()
        {
            By homeLink = this.GetElementBy(this.GetType(), "HomeLink");

            WaitForPageToBeReady<Categories>(homeLink, "HomeLink", 10);

            return this;
        }
        #endregion
    }
}
