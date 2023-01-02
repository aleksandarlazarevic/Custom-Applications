namespace CommonTestSteps.Contracts
{
    public interface ICartTestSteps
    {
        public void NavigateToCart();
        public void PlaceOrder();
        public void RemoveItemFromCart(string item);
    }
}
