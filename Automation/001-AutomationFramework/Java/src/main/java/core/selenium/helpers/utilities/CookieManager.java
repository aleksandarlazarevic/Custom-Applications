using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using SeleniumCore.WebDriver;

namespace SeleniumCore.Helpers.Utilities
{
    public static class CookieManager
    {
        public static string GetCookieValue(string cookieName)
        {
            return UIDriver.WebDriver?.Manage().Cookies?.AllCookies?.FirstOrDefault(x => x.Name.Contains(cookieName))?.Value;
        }

        public static string GetCookieJsonValue(string cookieName, string jpath)
        {
            try
            {
                var cookie = UIDriver.WebDriver?.Manage().Cookies?.AllCookies?.FirstOrDefault(x => x.Name.Contains(cookieName))?.Value;

                if (cookie == null)
                {
                    return null;
                }

                string decodedJson = HttpUtility.UrlDecode(cookie);
                JToken jToken = JToken.Parse(decodedJson)?.SelectToken(jpath);
                string jValue = jToken?.Value<string>();

                return jValue;
            }
            catch
            {
                return null;
            }
        }
    }
}
