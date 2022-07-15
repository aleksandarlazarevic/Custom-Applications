using Newtonsoft.Json;

namespace IOTests.Model
{
    public class DocumentedPreImage
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("hashFn")]
        public string HashFn { get; set; }
    }
}
