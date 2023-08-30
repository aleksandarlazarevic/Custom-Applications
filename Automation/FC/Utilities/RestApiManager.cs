using RestSharp;
using System;
using RestSharp.Authenticators.OAuth2;
using SeleniumCore.Helpers.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Net.Http;

namespace Utilities
{
    public static class RestApiManager
    {
        #region fields

        public static string ServiceUrl = EnvironmentVariable.GetEnvironmentVariable("api_url", "https://fcos-uat-app.azurewebsites.net");

        #endregion

        #region methods

        public static RestResponse StepGetData(string endpoint)
        {
            RestClient client = new RestClient(ServiceUrl);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                Environment.GetEnvironmentVariable("apiToken"), "Bearer"
            );
            RestRequest request = new RestRequest(ServiceUrl + endpoint, Method.Get);
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
                throw new Exception("Response does not exist");
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

            return response;
        }

        public static RestResponse StepGetDataWithHeader(Dictionary<string, string> listOfHeaders)
        {
            RestClient client = new RestClient(ServiceUrl);
            RestRequest request = new RestRequest(ServiceUrl, Method.Get);

            foreach (KeyValuePair<string, string> header in listOfHeaders)
            {
                request.AddHeader(header.Key, header.Value);
            }
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
                throw new Exception("Response does not exist");
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

            return response;
        }


        public static RestResponse StepGetDataFullUrl(string fullUrl)
        {
            RestClient client = new RestClient(ServiceUrl);
            RestRequest request = new RestRequest(fullUrl, Method.Get);
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
                throw new Exception("Response does not exist");
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

            return response;
        }

        public static RestResponse StepPostData(string endpoint, string bodyClass)
        {
            RestClient client = new RestClient(ServiceUrl);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                Environment.GetEnvironmentVariable("apiToken"), "Bearer"
            );
            RestRequest request = new RestRequest(ServiceUrl + endpoint, Method.Post);
            request.AddJsonBody(bodyClass);
            RestResponse response = new RestResponse();
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
                throw new Exception("Response does not exist");
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

            return response;
        }

        public static RestResponse StepPostData(string endpoint)
        {
            RestClient client = new RestClient(ServiceUrl);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                Environment.GetEnvironmentVariable("apiToken"), "Bearer"
            );
            RestRequest request = new RestRequest(ServiceUrl + endpoint, Method.Post);
            RestResponse response = new RestResponse();
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
                throw new Exception("Response does not exist");
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

            return response;
        }

        public static RestResponse StepPostDataBasicAuth(string username, string password, Dictionary<string, string> listOfParameters)
        {
            RestClient client = new RestClient(ServiceUrl);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(username, password);

            RestRequest request = new RestRequest(ServiceUrl, Method.Post);

            foreach (KeyValuePair<string, string> parameter in listOfParameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            RestResponse response = new RestResponse();
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
                throw new Exception("Response does not exist");
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

            return response;
        }

        public static void StepPutData()
        {
            RestClient client = new RestClient(ServiceUrl);
            var status = 1;
            var id = 3;
            RestRequest request = new RestRequest("/auto/api/v1.0/relays/" + id, Method.Put);
            request.AddJsonBody(new { status = status });
            RestResponse response = client.ExecuteAsync(request).Result;

            if (response == null)
            {
                throw new Exception("Response does not exist");
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

        public static RestResponse StepPutData(string urlRequest)
        {
            RestClient client = new RestClient(ServiceUrl);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                Environment.GetEnvironmentVariable("apiToken"), "Bearer"
            );
            RestRequest request = new RestRequest(ServiceUrl + urlRequest, Method.Put);
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
                throw new Exception("Response does not exist");
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

            return response;
        }
        #endregion
    }
}