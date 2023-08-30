using System.Net;
using RestSharp;

namespace SeleniumCore.Helpers.Utilities
{
    public static class WebsiteManager
    {
        public static bool IsWebsiteAvailable(string url)
        {
            try
            {
                HttpStatusCode statusCode = new RestClient(url).ExecuteAsync(new RestRequest()).Result.StatusCode;
                return statusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}