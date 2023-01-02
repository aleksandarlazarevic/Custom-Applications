using CommonTestSteps;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using TechTalk.SpecFlow;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class CartTestSteps : BaseTest
    {
        [When(@"Navigate to Cart")]
        public void WhenNavigateToCart()
        {
            RunStep(SharedSteps.Containers.Cart.NavigateToCart,
                    new TestStepInfo("[CART] - Navigate to Cart", false, Importance.High));
        }

        [Then(@"Place order")]
        public void ThenPlaceOrder()
        {
            RunStep(SharedSteps.Containers.Cart.PlaceOrder,
                    new TestStepInfo("[CART] - Place Order", false, Importance.High));
        }

        [Then(@"Remove item (.*)")]
        public void ThenRemoveItemNexus(string item)
        {
            RunStep(SharedSteps.Containers.Cart.RemoveItemFromCart, item,
                    new TestStepInfo(string.Format("[CART] - Remove item: {0}", item), false, Importance.High));
        }

    }
}
