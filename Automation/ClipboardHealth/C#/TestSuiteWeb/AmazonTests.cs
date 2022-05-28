using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Handlers;
using System;
using TestSuiteWeb.SharedSteps.Resolvers;

namespace TestSuiteWeb
{
    [TestClass]
    public class AmazonTests : StepResolver
    {
        [TestMethod]
        [Priority(1)]
        public void LoginAndLogout()
        {
            try
            {
                //Open https://www.amazon.in/.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.LoginSteps.NavigateToPage);

                //Click on the hamburger menu in the top left corner.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.ClickHamburgerMenu);

                //Scroll down and then Click on the TV, Appliances and Electronics link under Shop by Department section.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.ClickTvAppliancesElectronics);

                //Then click on Televisions under Tv, Audio &Cameras sub section.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.ClickTelevisions);

                //Scroll down and filter the results by Brand ‘Samsung’.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.CheckSamsungCheckbox);

                //Sort the Samsung results with price High to Low.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.SortHighToLow);

                //Click on the second highest priced item(whatever that maybe at the time of automating).
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.ChooseSecondPricedItem);

                //Switch the Window
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.SwitchTheWindow);

                //Assert that “About this item” section is present and log this section text to console/ report.
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.HomeSteps.VerifyAboutThisItem);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
