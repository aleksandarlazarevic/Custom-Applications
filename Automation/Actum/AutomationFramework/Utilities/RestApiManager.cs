using RestSharp;
using System;
using RestSharp.Authenticators.OAuth2;

namespace Utilities
{
    public class RestApiManager
    {
        #region Fields and Properties
        static string _serviceUrl = SeleniumCore.Helpers.Utilities.EnvironmentVariable.GetEnvironmentVariable("api_url", "https://restful-booker.herokuapp.com/");
        static string _token = "c4bb823b5c3375a";
        #endregion
        #region Methods
        public static RestResponse GetData(string endpoint)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint, Method.Get);

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

            return response;
        }

        public static RestResponse GetDataCompleteUrl(string completeUrl)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(completeUrl, Method.Get);
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

            return response;
        }

        public static RestResponse PostData(string endpoint, string body)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint, Method.Post);
            request.AddHeader("authorization", "Bearer " + _token);
            request.AddJsonBody(body);
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

            return response;
        }

        public static RestResponse PostData(string endpoint)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint, Method.Post);
            request.AddHeader("authorization", "Bearer " + _token);
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

            return response;
        }

        public static RestResponse PutData(string endpoint, string id, object booking)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint + id, Method.Put);
            RestResponse response = new RestResponse();
            request.AddHeader("authorization", "Bearer " + _token);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(booking);
            
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

            return response;
        }

        public static RestResponse PatchData(string endpoint, string id, object booking)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint + id, Method.Patch);
            RestResponse response = new RestResponse();
            request.AddHeader("authorization", "Bearer " + _token);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(booking);

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

            return response;
        }

        public static RestResponse DeleteData(string endpoint, string id)
        {
            RestClient client = new RestClient(_serviceUrl);
            RestRequest request = new RestRequest(_serviceUrl + endpoint + id, Method.Delete);
            request.AddHeader("authorization", "Bearer " + _token);
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

            return response;
        }
        #endregion
    }
}
