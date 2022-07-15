using Newtonsoft.Json;

namespace IOTests.Model
{
    public class Signature
    {
        [JsonProperty("signature")]
        public string signature { get; set; }

        [JsonProperty("publicKey")]
        public string publicKey { get; set; }
    }
}