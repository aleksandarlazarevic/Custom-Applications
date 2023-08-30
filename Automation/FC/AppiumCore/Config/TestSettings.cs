using AppiumCore.Base;
using Newtonsoft.Json;

namespace AppiumCore.Config
{
    public class TestSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("browserstackUser")]
        public string BrowserstackUser { get; set; }

        [JsonProperty("browserstackKey")]
        public string BrowserstackKey { get; set; }

        [JsonProperty("browserStackAppIdentifier")]
        public string BrowserStackAppIdentifier { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("mobileType")]
        public MobileType MobileType { get; set; }

        [JsonProperty("platformName")]
        public PlatformName PlatformName { get; set; }

        [JsonProperty("platformVersion")]
        public string PlatformVersion { get; set; }

        [JsonProperty("chromeDriverPath")]
        public string ChromeDriverPath { get; set; }
    }
}