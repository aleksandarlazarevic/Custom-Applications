using Newtonsoft.Json;
using System.Collections.Generic;

namespace IOTests.Model
{
    public class DocumentedCoinSignatureProperties
    {
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }

        [JsonProperty("signature")]
        public List<Signature> Signature { get; set; }
    }
}
