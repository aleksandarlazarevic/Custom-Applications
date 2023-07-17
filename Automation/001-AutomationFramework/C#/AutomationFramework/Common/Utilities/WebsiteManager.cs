using RestSharp;
using System.Net;

namespace Utilities
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