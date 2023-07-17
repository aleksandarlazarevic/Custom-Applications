using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utilities
{
    public class JsonManager
    {
        public static JObject? StringToJson(string jsonString)
        {
            if (!IsValidJson(jsonString))
            {
                throw new Exception("Not a valid JSON");
            }

            JObject? output = JsonConvert.DeserializeObject<JObject>(jsonString);
            return output;
        }

        public static string JsonToString(JObject json)
        {
            string output = JsonConvert.SerializeObject(json);
            return output;
        }

        public static T? JsonStringToObject<T>(string jsonString) where T : new()
        {
            T? returnObject = JsonConvert.DeserializeObject<T?>(jsonString);
            return returnObject;
        }

        public static T? JsonToObject<T>(JObject json) where T : new()
        {
            T? returnObject = JsonConvert.DeserializeObject<T>(JsonToString(json));
            return returnObject;
        }

        public static string ObjectToJson<T>(T jsonString) where T : new()
        {
            string output = JsonConvert.SerializeObject(jsonString);
            return output;
        }

        public static bool IsValidJson(string stringToCheck)
        {
            try
            {
                JToken asToken = JToken.Parse(stringToCheck);
                return asToken.Type == JTokenType.Object || asToken.Type == JTokenType.Array;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}