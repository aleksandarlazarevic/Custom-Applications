using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Handlers;
using SeleniumCore.WebDriver;
using System;

namespace SeleniumCore.Base
{
    public abstract class BaseTest
    {
        #region Fields and Properties 
        public TestContext TestContext { get; set; }

        public TestDriver TestDriver { get; private set; }
        #endregion

        #region Methods 
        [TestInitialize]
        public void TestInitialize(TestContext testContext)
        {
            SetTestContext(testContext);
            InitializeTestParameters();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            // Close web browser
            UIDriver.WebDriver?.Quit();

            // Clear web driver proccess in the task manager
            Processes.KillProcess("WebDriver");

            TestDriver.Close();
        }

        private void SetTestContext(TestContext testContext)
        {
            TestDriver.Initialize(testContext);
            TestDriver = TestDriver.Instance;
        }

        private void InitializeTestParameters()
        {
            TestInMemoryParameters.Instance.TestIdentifier = TestDriver.TestIdentifier;
            TestInMemoryParameters.Instance.TestIdentifierStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            TestInMemoryParameters.Instance.ElementTimeout = "60";
            TestInMemoryParameters.Instance.PageLoadTimeout = "60";
        }

        public void RunStep(Action action, IStepInfo stepInfo)
        {
            TestDriver.Instance.RunStep(action, stepInfo);
        }

        public void RunStep(Action action)
        {
            TestDriver.Instance.RunStep(action);
        }

        public void RunStep<T>(Action<T> action, T parameter)
        {
            TestDriver.Instance.RunStep(action, parameter);
        }

        public void RunStep<T>(Action<T> action, T parameter, IStepInfo stepInfo)
        {
            TestDriver.Instance.RunStep(action, parameter, stepInfo);
        }

        public void RunStep<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2, IStepInfo stepInfo)
        {
            TestDriver.Instance.RunStep(action, parameter1, parameter2, stepInfo);
        }

        public static T GetPage<T>() where T : BasePage
        {
            return UIDriver.WebDriver.GetPage<T>();
        }
        #endregion
    }
}