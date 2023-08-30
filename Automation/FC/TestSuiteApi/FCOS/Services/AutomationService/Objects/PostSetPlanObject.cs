using SpecFlow.Internal.Json;
using TestSuiteApi.FCOS.Services.AutomationService.Models.Request;

namespace TestSuiteApi.FCOS.Services.AutomationService.Objects
{
    public static class PostSetPlanObject
    {
        public static string SetPlanObject()
        {
            var postSetPlanRequest = new PostSetPlanRequestModel
            {
                Id = null,
                Name = "Automation API Test",
                NodeId = "de7df7ad-c3ab-4a19-962d-2d9e84d94fe6",
                Description = "Iron 24"
            };
            return postSetPlanRequest.ToJson();
        }
    }
}