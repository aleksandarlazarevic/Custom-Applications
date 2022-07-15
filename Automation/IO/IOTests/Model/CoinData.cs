using Newtonsoft.Json;

namespace IOTests.Model
{
    public class CoinData
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("url")]
        public CoinProperties Url { get; set; }

        [JsonProperty("name")]
        public CoinProperties Name { get; set; }

        [JsonProperty("ticker")]
        public CoinProperties Ticker { get; set; }

        [JsonProperty("decimals")]
        public CoinProperties Decimals { get; set; }

        [JsonProperty("policy")]
        public string Policy { get; set; }

        [JsonProperty("logo")]
        public CoinProperties Logo { get; set; }

        [JsonProperty("description")]
        public CoinProperties Description { get; set; }
    }
}
