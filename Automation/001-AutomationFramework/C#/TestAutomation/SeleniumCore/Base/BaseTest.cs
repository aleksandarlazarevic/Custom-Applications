using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Handlers;
using System;

namespace SeleniumCore.Base
{
    public abstract class BaseTest
    {
        #region == Fields ==
        #endregion

        #region == Properties ==
        public TestContext TestContext { get; set; }

        public TestDriver TestDriver { get; private set; }
        #endregion

        #region == Methods ==
        [TestInitialize]
        public virtual void TestInitialize()
        {
            SetTestContext();
            InitializeTestParameters();
        }

        [TestCleanup]
        public virtual void TestCleanUp()
        {
            TestDriver.Close();
        }

        private void SetTestContext()
        {
            TestDriver.Initialize(TestContext);
            TestDriver = TestDriver.Instance;
        }

        private void InitializeTestParameters()
        {
            TestInMemoryParameters.Instance.TestIdentifier = TestDriver.TestIdentifier;
            TestInMemoryParameters.Instance.TestIdentifierStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion
    }
}
