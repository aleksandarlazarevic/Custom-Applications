using SpecFlow.Internal.Json;
using TestSuiteApi.FCOS.Services.AutomationService.Models.Request;

namespace TestSuiteApi.FCOS.Services.AutomationService.Objects
{
    public static class PostConfigurationsObject
    {
        public static string ConfigurationsRequestBody()
        {
            var postConfigurationsRequestModel = new PostConfigurationsRequestModel()
            {
                FromNodeId = "5c3bfa48-24be-41db-a3c5-f2a3b75f0cf3",
                FromUserId = "5c3bfa48-24be-41db-a3c5-f2a3b75f0cf3",
                TemplateUserId = "5c3bfa48-24be-41db-a3c5-f2a3b75f0cf3",
            };
            return postConfigurationsRequestModel.ToJson();
        }
    }
}