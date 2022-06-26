using AppiumCore.Base;
using Newtonsoft.Json;

namespace AppiumCore.Config
{
    class TestSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("autPath")]
        public string AUTPath { get; set; }


        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("mobileType")]
        public MobileType MobileType { get; set; }

        [JsonProperty("platformName")]
        public PlatformName PlatformName { get; set; }

        [JsonProperty("chromeDriverPath")]
        public string ChromeDriverPath { get; set; }
    }
}
