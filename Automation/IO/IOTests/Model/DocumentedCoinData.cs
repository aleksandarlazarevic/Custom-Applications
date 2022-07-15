using Newtonsoft.Json;

namespace IOTests.Model
{
    public class DocumentedCoinData
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("preImage")]
        public DocumentedPreImage PreImage { get; set; }

        [JsonProperty("name")]
        public DocumentedCoinProperties Name { get; set; }

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
