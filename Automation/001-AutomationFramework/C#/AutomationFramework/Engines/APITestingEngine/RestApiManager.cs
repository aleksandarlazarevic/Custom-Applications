using CommonCore.Contracts;
using CommonCore.Tests;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Utilities
{
    public class RestApiManager
    {
        #region fields
        private static Uri RootApiUrl = new Uri(TestInMemoryParameters.Instance.ApiEndpoint);
        #endregion

        #region methods
            #region GET requests
            public static RestResponse GetData(string endpoint)
            {
                RestClient client = new RestClient(RootApiUrl);
                RestRequest request = new RestRequest(RootApiUrl + endpoint, Method.Get);

                return RetrieveResponse(client, request);
            }

            public static RestResponse GetDataWithAuthentication(string endpoint)
            {
                RestClientOptions options = new RestClientOptions();
                options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    Environment.GetEnvironmentVariable("apiToken"), "Bearer"
                );
                options.BaseUrl = RootApiUrl;

                RestClient client = new RestClient(options);
                RestRequest request = new RestRequest(RootApiUrl + endpoint, Method.Get);

                return RetrieveResponse(client, request);
            }

            public static RestResponse GetDataWithHeaders(Dictionary<string, string> listOfHeaders)
            {
                RestClient client = new RestClient(RootApiUrl);
                RestRequest request = new RestRequest(RootApiUrl, Method.Get);

                foreach (KeyValuePair<string, string> header in listOfHeaders)
                {
                    request.AddHeader(header.Key, header.Value);
                }

                return RetrieveResponse(client, request);
            }

            public static RestResponse GetDataFullUrl(string fullUrl)
            {
                RestClient client = new RestClient(RootApiUrl);
                RestRequest request = new RestRequest(fullUrl, Method.Get);

                return RetrieveResponse(client, request);
            }
            #endregion
            #region POST requests
            public static RestResponse PostData(string endpoint, string bodyClass)
            {
                RestClientOptions options = new RestClientOptions();
                options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    Environment.GetEnvironmentVariable("apiToken"), "Bearer"
                );
                options.BaseUrl = RootApiUrl;

                RestClient client = new RestClient(options);
                RestRequest request = new RestRequest(RootApiUrl + endpoint, Method.Post);
                request.AddJsonBody(bodyClass);
                request.RequestFormat = DataFormat.Json;

                return RetrieveResponse(client, request);
            }

            public static RestResponse PostData(string endpoint)
            {
                RestClientOptions options = new RestClientOptions();
                options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    Environment.GetEnvironmentVariable("apiToken"), "Bearer"
                );
                options.BaseUrl = RootApiUrl;

                RestClient client = new RestClient(options);
                RestRequest request = new RestRequest(RootApiUrl + endpoint, Method.Post);
                request.RequestFormat = DataFormat.Json;

                return RetrieveResponse(client, request);
            }

            public static RestResponse PostDataBasicAuth(string username, string password, Dictionary<string, string> listOfParameters)
            {
                RestClientOptions options = new RestClientOptions();
                options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    Environment.GetEnvironmentVariable("apiToken"), "Bearer"
                );
                options.BaseUrl = RootApiUrl;

                RestClient client = new RestClient(options);

                RestRequest request = new RestRequest(RootApiUrl, Method.Post);

                foreach (KeyValuePair<string, string> parameter in listOfParameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }

                request.RequestFormat = DataFormat.Json;

                return RetrieveResponse(client, request);
            }
            #endregion
            #region PUT requests
            public static RestResponse PutData(string urlRequest)
            {
                RestClientOptions options = new RestClientOptions();
                options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    Environment.GetEnvironmentVariable("apiToken"), "Bearer"
                );
                options.BaseUrl = RootApiUrl;

                RestClient client = new RestClient(options);
                RestRequest request = new RestRequest(RootApiUrl + urlRequest, Method.Put);

                return RetrieveResponse(client, request);
            }
            #endregion
        private static RestResponse RetrieveResponse(RestClient client, RestRequest request)
        {
            RestResponse response = new RestResponse();

            try
            {
                response = client.ExecuteAsync(request).Result;
            }
            catch (Exception error)
            {
                throw new Exception("Failed executing request: " + error.Message);
            }

            if (response == null)
            {
                throw new Exception("Response does not exist");
            }

            if (response.ErrorException != null)
            {
                throw new Exception("Error occurred during the request", response.ErrorException);
            }

            if (JsonManager.IsValidJson(response?.Content))
            {
                TestInMemoryParameters.Instance.JsonApiResponse = JsonManager.StringToJson(response.Content);
            }
            else if (XmlManager.IsValidXml(response?.Content))
            {
                TestInMemoryParameters.Instance.XmlApiResponse = XmlManager.StringToXml(response.Content);
            }
            else
            {
                TestInMemoryParameters.Instance.TextApiResponse = response.Content;
            }

            return response;
        }
        #endregion
    }
}