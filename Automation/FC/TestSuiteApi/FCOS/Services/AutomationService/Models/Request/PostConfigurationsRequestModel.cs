using Newtonsoft.Json;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Request
{
    public class PostConfigurationsRequestModel
    {
        public string FromUserId { get; set; }

        public string FromNodeId { get; set; }

        public string TemplateUserId { get; set; }
    }
}