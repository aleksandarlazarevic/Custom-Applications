using CommonTestSteps.Contracts;
using UIMappings.Pages;

namespace CommonTestSteps.TestSteps
{
    public class CategoriesTestSteps : GlobalTestSteps, ICategoriesTestSteps
    {
        public void AddToCart(string model)
        {
            GetPage<Categories>().WaitForPageReady()
                                .AddModelToCart(model);
        }

        public void ChooseCategory(string category)
        {
            GetPage<Categories>().WaitForPageReady()
                                .ClickCategory(category);
        }

        public void NavigateToHomeTab()
        {
            GetPage<Categories>().WaitForPageReady()
                                .ClickOnHomeLink();
        }
    }
}
