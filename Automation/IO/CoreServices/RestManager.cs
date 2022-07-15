using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoreServices
{
    public class RestManager
    {
        private static RestManager instance = null;
        private static readonly object padlock = new object();
        private string RestApiBaseUrl = @"http://metadata-server-mock.herokuapp.com/metadata/";
        string jsonString = string.Empty;
        public static RestClient restClient;
        public static RestRequest request;
        public static RestResponse restResponse;

        public RestManager()
        {
        }

        public static RestManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RestManager();
                    }
                    return instance;
                }
            }
        }

        public void CreateRestClient(string urlSuffix = null)
        {
            if (urlSuffix != null)
            {
                RestApiBaseUrl = RestApiBaseUrl + urlSuffix;
            }

            restClient = new RestClient(RestApiBaseUrl);
        }

        public void CreateRequestBody(string data)
        {
            //jsonString = JsonSerializer.Serialize(data);
            string jsonBody = JsonConvert.SerializeObject(data);
            var jsonBody2 = JsonConvert.DeserializeObject(data);
            JObject json = JObject.Parse(data);
            request.AddJsonBody(json);
        }

        public void CreateRequest(string resourcePath, Method method)
        {
            CreateRestClient(resourcePath);
            request = new RestRequest(RestApiBaseUrl, method);
            request.RequestFormat = DataFormat.Json;
        }

        public RestResponse GetResponse()
        {
            restResponse = restClient.ExecuteAsync(request).Result;

            return restResponse;
        }
    }
}
