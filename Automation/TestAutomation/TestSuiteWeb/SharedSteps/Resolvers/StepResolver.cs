using SeleniumCore.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.WebDriver;
using SeleniumCore.Handlers;

namespace TestSuiteWeb.SharedSteps.Resolvers
{
    public abstract class StepResolver : BaseTest
    {
        #region == Properties ==

        #endregion

        public StepResolver()
        {

        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            // Close web browser
            UIDriver.WebDriver?.Quit();

            // Clear web driver proccess in the task manager
            Processes.KillProcess("WebDriver");

            base.TestCleanUp();
        }
    }
}
