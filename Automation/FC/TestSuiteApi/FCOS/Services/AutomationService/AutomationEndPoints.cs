using TechTalk.SpecFlow;

namespace TestSuiteApi.FCOS.Services.AutomationService
{
    [Binding]
    public static class AutomationEndPoints
    {

        public const string GetWorkFlows = "/api/automation/workflows?includeChilden=true";
        public const string AutomationWebhookUrl = "/api/automation/environment?includeChilden=true";
        public const string GetWorkFlow = "/api/automation/workflows/";
        public const string GetSetPlans = "/api/automation/plans/sets?includeChildren=true";
        public const string GetSetPlan = "/api/automation/plans/sets/";
        public const string GetWorkFlowSetPlan = "/api/automation/plans/workflows/";
        public const string GetTriggers = "/api/automation/triggers";
        public const string GetConfigurations = "/api/automation/configuration";
        public const string PostConfigurations = "/api/automation/configuration";
        public const string GetWebHookSampleData = "/api/automation/webhook/test";
        public const string PostWorkFlows = "/api/automation/workflows";
        public const string PostSetPlan = "/api/automation/plans/sets";
        public const string PostPlansToSet = "/api/automation/plans/sets/";
        public const string PostCopyWorkFlowSetPlanToSet = "/api/automation/plans/sets/";
    }
}

