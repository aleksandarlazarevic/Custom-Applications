using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOTests.TestSteps;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json;
using RestSharp;
using IOTests.Model;
using IOTests.Utils;
using System.Net;
using System.Linq;

namespace IOTests
{
    public class APITests
    {
        #region POST requests
        // Fails due to inaccurate github documentation
        [Test, Order(1)]
        [Category("APITests")]
        public void PostCoinSubjects()
        {
            string dataToPost = Constants.NoAssetNameCoinPutSubjectsRequest;
            var coinData = CoinAPI.PostCoinData("query", dataToPost);

            HttpStatusCode statusCode = coinData.StatusCode;
            int numericStatusCode = (int)statusCode;

            ResponseCoinData coinDataObject = JsonConvert.DeserializeObject<ResponseCoinData>(coinData.Content);

            // Assert that any data is returned and that it's not an empty json
            Assert.That(coinData.ContentLength, Is.GreaterThan(6));

            // Assert that returned subjects array contains any elements
            Assert.That(coinDataObject.Subjects.Length, Is.GreaterThan(0));

            // Assert that status code is one of the following: CREATED, FOUND, CONFLICT
            Assert.That(numericStatusCode, Is.EquivalentTo(new[] { 201, 302, 409 }), string.Format("Status code not as expected! Actual status code: {0}", numericStatusCode));
        }

        // Fails due to inaccurate github documentation
        [Test, Order(2)]
        [Category("APITests")]
        public void PostCoinSubjectsWithProperties()
        {
            string dataToPost = Constants.NoAssetNameCoinPutSubjectsWithPropertiesRequest;
            var coinData = CoinAPI.PostCoinData("query", dataToPost);

            HttpStatusCode statusCode = coinData.StatusCode;
            int numericStatusCode = (int)statusCode;

            ResponseCoinData coinDataObject = JsonConvert.DeserializeObject<ResponseCoinData>(coinData.Content);

            // Assert that any data is returned and that it's not an empty json
            Assert.That(coinData.ContentLength, Is.GreaterThan(6));

            // Assert that returned subjects array contains any elements
            Assert.That(coinDataObject.Subjects.Length, Is.GreaterThan(0));

            // Assert that returned properties array contains any values
            Assert.That(coinDataObject.Properties, Is.Not.Null);

            // Assert that status code is one of the following: CREATED, FOUND, CONFLICT
            Assert.That(numericStatusCode, Is.EquivalentTo(new[] { 201, 302, 409 }), string.Format("Status code not as expected! Actual status code: {0}", numericStatusCode));
        }
        #endregion

        #region GET coin metadata values
        // Fails due to inaccurate github documentation
        [Test, Order(3)]
        [Category("APITests")]
        public void GetAmazingCoinDataInfo()
        {
            RestResponse coinData = CoinAPI.GetCoinData(Constants.NoAssetNameCoinHash);

            // Check if data is returned
            Assert.That(coinData.Content, Is.Not.Contains("not found"), "No data was returned");

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinData coinDataObject = JsonConvert.DeserializeObject<CoinData>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinDataObject);
        }

        // Fails due to inaccurate github documentation
        // The goal of this test is to assert that the documented response format is wrong when using successfully retrieved data
        [Test, Order(4)]
        [Category("APITests")]
        public void GetGoodCoinDataInfoPerDocumentation()
        {
            RestResponse coinData = CoinAPI.GetCoinData(Constants.AmazingCoinHash);

            // Check if data is returned
            Assert.That(coinData.Content, Is.Not.Contains("not found"), "No data was returned");

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            DocumentedCoinData coinDataObject = JsonConvert.DeserializeObject<DocumentedCoinData>(coinData.Content);
            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinDataObject);
        }

        [Test, Order(5)]
        [Category("APITests")]
        public void GetGoodCoinDataInfo()
        {
            RestResponse coinData = CoinAPI.GetCoinData(Constants.AmazingCoinHash);

            // Check if data is returned
            Assert.That(coinData.Content, Is.Not.Contains("not found"), "No data was returned");

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinData coinDataObject = JsonConvert.DeserializeObject<CoinData>(coinData.Content);
            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinDataObject);
        }
        #endregion

        #region GET coin property values
        // Fails due to inaccurate github documentation
        [Test, Order(6)]
        [Category("APITests")]
        public void GetNoAssetNameCoinUrlProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.NoAssetNameCoinHash, Constants.UrlProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(7)]
        [Category("APITests")]
        public void GetCoinUrlProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.UrlProperty));
            
            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(8)]
        [Category("APITests")]
        public void GetCoinNameProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.NameProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(9)]
        [Category("APITests")]
        public void GetCoinTickerProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.TickerProperty));
            
            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(10)]
        [Category("APITests")]
        public void GetCoinDecimalsProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.DecimalsProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(11)]
        [Category("APITests")]
        public void GetCoinPolicyProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.PolicyProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            string policyPropertyValue = JsonConvert.DeserializeObject<string>(coinData.Content);

            // Assert that a value is returned
            Assert.That(policyPropertyValue, Is.Not.Null);
        }

        [Test, Order(12)]
        [Category("APITests")]
        public void GetCoinLogoProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.LogoProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }

        [Test, Order(13)]
        [Category("APITests")]
        public void GetCoinDescriptionProperty()
        {
            RestResponse coinData = CoinAPI.GetCoinData(string.Format("{0}/properties/{1}", Constants.AmazingCoinHash, Constants.DescriptionProperty));

            // Check response code
            Assert.That(coinData.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Wrong status code! Actual status code: " + coinData.StatusCode);

            CoinProperties coinPropertiesObject = JsonConvert.DeserializeObject<CoinProperties>(coinData.Content);

            SharedSteps.CheckIfValuesAreReturnedAsExpected(coinPropertiesObject);

        }
        #endregion
    }

    public class OrderedTestAttribute : Attribute
    {
        public int Order { get; set; }


        public OrderedTestAttribute(int order)
        {
            Order = order;
        }
    }

    public class TestStructure
    {
        public Action Test;
    }

    class Int
    {
        public int I;
    }
}
