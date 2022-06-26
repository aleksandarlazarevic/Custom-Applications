using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using SeleniumCore.Handlers;
using RestSharp;
using SharedFunctionalities.Helpers;

namespace SharedFunctionalities.Submit
{
    public class RestApiSubmitter
    {
        #region fields
        string _serviceUrl;
        string _json;
        #endregion

        #region constructors
        public RestApiSubmitter(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }
        #endregion

        #region methods
        public void StepGetData(string source)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(source, Method.Get);
            RestResponse response = new RestResponse();

            try
            {
                response = client.ExecuteAsync(request).Result;
            }
            catch (Exception error)
            {

            }

            if (response == null)
            {
                throw new Exception("Response does not exists");
            }

            if (response.ErrorException != null)
            {
                throw new Exception("Error occurred during the request", response.ErrorException);
            }

            if (JsonHelper.IsJsonFromString(response?.Content))
            {

            }
            else if (XmlHelper.IsXmlFromString(response?.Content))
            {

            }
            else
            {

            }
        }

        public void StepPostData()
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest("/resource/", Method.Post);
            RestResponse response = new RestResponse();

            // Json to post.
            string jsonToSend = JsonHelper.ToJson(_json);

            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            try
            {
                response = client.ExecuteAsync(request).Result;
            }
            catch (Exception error)
            {
            }


            if (response == null)
            {
                throw new Exception("Response does not exists");
            }

            if (response.ErrorException != null)
            {
                throw new Exception("Error occurred during the request", response.ErrorException);
            }

            if (JsonHelper.IsJsonFromString(response?.Content))
            {

            }
            else if (XmlHelper.IsXmlFromString(response?.Content))
            {

            }
            else
            {

            }
        }

        public void StepPutData()
        {
            RestClient client = new RestClient(_serviceUrl);
            var status = 1;
            var id = 3;
            RestRequest request = new RestRequest("/auto/api/v1.0/relays/" + id, Method.Put);
            request.AddJsonBody(new { status = status });
            RestResponse response = client.ExecuteAsync(request).Result;

            if (response == null)
            {
                throw new Exception("Response does not exists");
            }

            if (response.ErrorException != null)
            {
                throw new Exception("Error occurred during the request", response.ErrorException);
            }

            if (JsonHelper.IsJsonFromString(response?.Content))
            {

            }
            else if (XmlHelper.IsXmlFromString(response?.Content))
            {

            }
            else
            {

            }
        }
        #endregion
    }
}
