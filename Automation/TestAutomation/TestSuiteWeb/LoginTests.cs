using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Contracts.Drivers;
using SeleniumCore.Handlers;
using System;
using TestSuiteWeb.SharedSteps.Resolvers;
using TestSuiteWeb.Steps;

namespace TestSuiteWeb
{
    [TestClass]
    public class LoginTests : StepResolver
    {
        [TestMethod]
        [Priority(1)]
        public void LoginAndLogout()
        {
            try
            {
                TestDriver.RunStep(SharedSteps.Resolvers.SharedSteps.Containers.LoginSteps.StepSimpleLogin);

            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
