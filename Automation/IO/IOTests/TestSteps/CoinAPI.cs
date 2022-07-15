using CoreServices;
using RestSharp;

namespace IOTests.TestSteps
{
    public class CoinAPI
    {
        public static RestResponse GetCoinData(string apiPath)
        {
            RestManager rm = new RestManager();
            rm.CreateRequest(apiPath, Method.Get);
            var response = rm.GetResponse();
            return response;
        }

        public static RestResponse PostCoinData(string apiPath, string jsonBody)
        {
            RestManager rm = new RestManager();
            rm.CreateRequest(apiPath, Method.Post);
            rm.CreateRequestBody(jsonBody);
            var response = rm.GetResponse();
            return response;
        }
    }
}
