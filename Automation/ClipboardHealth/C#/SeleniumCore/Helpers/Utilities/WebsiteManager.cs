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
                return new RestClient(url).ExecuteAsync(new RestRequest(Method.Get.ToString())).Result.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
