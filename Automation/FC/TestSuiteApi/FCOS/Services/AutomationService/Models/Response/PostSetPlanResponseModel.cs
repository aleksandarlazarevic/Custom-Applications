using Newtonsoft.Json;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Response
{
    public class PostSetPlanResponseModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("NodeId")]
        public string NodeId { get; set; }
        
        [JsonProperty("createdUsername")]
        public string CreatedUsername { get; set; }
    }
}