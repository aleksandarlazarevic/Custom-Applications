using Newtonsoft.Json;
using System.Collections.Generic;

namespace IOTests.Model
{
    public class CoinProperties
    {
        [JsonProperty("sequenceNumber")]
        public string SequenceNumber { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("signatures")]
        public List<Signature> Signatures { get; set; }
    }
}