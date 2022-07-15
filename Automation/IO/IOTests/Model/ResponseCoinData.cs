using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTests.Model
{
    public class ResponseCoinData
    {
        [JsonProperty("subjects")]
        public string[] Subjects { get; set; }

        [JsonProperty("properties")]
        public string[] Properties { get; set; }
    }
}
