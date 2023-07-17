using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsoleRunner.Json
{
    public class JsonTests
    {
        public static void TestConversions()
        {
            string jsonStr = "{\"objects\":[{\"id\":1,\"title\":\"Book\",\"position_x\":0,\"position_y\":0,\"position_z\":0,\"rotation_x\":0,\"rotation_y\":0,\"rotation_z\":0,\"created\":\"2016-09-21T14:22:22.817Z\"},{\"id\":2,\"title\":\"Apple\",\"position_x\":0,\"position_y\":0,\"position_z\":0,\"rotation_x\":0,\"rotation_y\":0,\"rotation_z\":0,\"created\":\"2016-09-21T14:22:52.368Z\"}]}";
            string simpleJsonStr = "{\"id\":1,\"title\":\"Book\"}";

            dynamic output = JsonConvert.DeserializeObject(jsonStr);
            dynamic simpleJsonStrOutput = JsonConvert.DeserializeObject(simpleJsonStr);

            var jobject = JsonConvert.DeserializeObject<JObject>(jsonStr);
            var simplejobject = JsonConvert.SerializeObject(simpleJsonStr);

            JObject pppppp = JsonHelper.StringToJson(simpleJsonStr);
            string rrrr = JsonHelper.JsonToString(pppppp);

            var tt2 = JsonHelper.JsonStringToObject<SimpleJson>(simpleJsonStr);
            var rtrt = JsonHelper.JsonToObject<SimpleJson>(pppppp);

            var tretw = JsonHelper.ObjectToJson(rtrt);
        }
    }
}
