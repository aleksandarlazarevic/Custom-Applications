using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Utilities;

namespace TestSuiteApi.Steps
{
    [Binding]
    public class GlobalSteps
    {
        private RestResponse? _response;

        [Given(@"The API is running")]
        public void GivenTheAPIIsRunning()
        {
            _response = RestApiManager.GetData("ping");
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.Created, "API is not available");
        }

    }
}
