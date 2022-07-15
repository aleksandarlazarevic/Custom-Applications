using Newtonsoft.Json;
using System.Collections.Generic;

namespace IOTests.Model
{
    public class DocumentedCoinProperties
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("anSignatures")]
        public List<DocumentedCoinSignatureProperties> AnSignatures { get; set; }
    }
}
