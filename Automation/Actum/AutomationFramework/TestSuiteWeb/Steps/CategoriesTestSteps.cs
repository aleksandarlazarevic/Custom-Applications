using CommonTestSteps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Base;
using SeleniumCore.Enums;
using SeleniumCore.Handlers;
using TechTalk.SpecFlow;
using UIMappings;

namespace TestSuiteWeb.Steps
{
    [Binding]
    public class CategoriesTestSteps : BaseTest
    {
        [Then(@"Choose (.*)")]
        public void ThenChooseCategory(string category)
        {
            RunStep(SharedSteps.Containers.Categories.ChooseCategory, category,
                    new TestStepInfo("[CATEGORIES] - Choose Category", false, Importance.High));
        }

        [Then(@"Add to cart a (.*)")]
        public void ThenAddToCartAModel(string model)
        {
            RunStep(SharedSteps.Containers.Categories.AddToCart, model,
                    new TestStepInfo("[CATEGORIES] - Add model to cart", false, Importance.High));

            string alertMessage = CommonUtilities.GetAlertMessage();

            Assert.AreEqual(alertMessage, "Product added", string.Format("Adding product {0} failed", model));
        }

    }
}
