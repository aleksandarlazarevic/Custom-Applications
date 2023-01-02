using CommonTestSteps.Contracts;
using UIMappings.Pages;

namespace CommonTestSteps.TestSteps
{
    public class CartTestSteps : GlobalTestSteps, ICartTestSteps
    {
        public void NavigateToCart()
        {
            GetPage<Cart>().WaitForPageReady()
                                .ClickOnCartLink();
        }

        public void PlaceOrder()
        {
            GetPage<Cart>().WaitForPageReady()
                                .PlaceOrder();
        }

        public void RemoveItemFromCart(string item)
        {
            GetPage<Cart>().WaitForPageReady()
                                .RemoveItemFromCart(item);
        }
    }
}
