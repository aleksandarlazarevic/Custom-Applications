using Newtonsoft.Json;
using System.Dynamic;
using System.Text.Json;
using Utilities;

namespace CommonCore.Configuration
{
    public class ConfigurationManager
    {
        public static TestConfiguration? ReadConfigurationFile(string filePath)
        {
            string configurationFile = File.ReadAllText(filePath);

            return JsonManager.JsonStringToObject<TestConfiguration>(configurationFile);
        }
    }
}
