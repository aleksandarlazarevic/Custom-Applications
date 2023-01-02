using OpenQA.Selenium;
using SeleniumCore.Base;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;
using SeleniumCore.Helpers.Extensions;

namespace UIMappings.Pages
{
    public class Cart : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Cart')]")]
        public IWebElement CartLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Place Order')]")]
        public IWebElement PlaceOrderButton { get; set; }

        public IWebElement DeleteLink;

        #endregion

        #region Methods
        public Cart ClickOnCartLink()
        {
            CartLink.ClickWrapper("CartLink");
            return this;
        }

        public Cart PlaceOrder()
        {
            PlaceOrderButton.ClickWrapper("PlaceOrderButton");
            return this;
        }

        public Cart RemoveItemFromCart(string item)
        {
            this.DeleteLink.FindElement(By.XPath(string.Format("//td[contains(text(), '{0}')]../td[4]", item)));

            this.DeleteLink.ClickWrapper("DeleteLink");
            return this;
        }

        public Cart WaitForPageReady()
        {
            By cartLink = this.GetElementBy(this.GetType(), "CartLink");

            WaitForPageToBeReady<Cart>(cartLink, "CartLink", 10);

            return this;
        }
        #endregion
    }
}
