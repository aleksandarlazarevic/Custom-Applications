using RestSharp;
using TestSuiteApi.FCOS.Services.AutomationService;

namespace TestSuiteApi.FCOS.Utils
{
    public static class AutomationServiceUtils
    {
        public static RestResponse GetWorkFlows()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetWorkFlows);
        }

        public static RestResponse GetAutomationWebHockUrl()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.AutomationWebhookUrl);
        }

        public static RestResponse GetWorkFlowById(string workFlowId)
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetWorkFlow + workFlowId);
        }

        public static RestResponse GetSetPlans()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetSetPlans);
        }

        public static RestResponse GetSetPlanById(string setPlanId)
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetSetPlan + setPlanId);
        }

        public static RestResponse GetWorkflowSetPlanById(string workFlowId)
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetWorkFlowSetPlan + workFlowId);
        }

        public static RestResponse GetTriggers()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetTriggers);
        }

        public static RestResponse GetConfiguration()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetConfigurations);
        }

        public static RestResponse GetWebHookTest()
        {
            return Utilities.RestApiManager.StepGetData(AutomationEndPoints.GetWebHookSampleData);
        }

        public static RestResponse CreateWorkFlow(string bodyClass)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostWorkFlows, bodyClass);
        }

        public static RestResponse SetWorkFlowActive(string workflowId)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostWorkFlows + "/" + workflowId +
                                                         "/active");
        }

        public static RestResponse SetWorkFlowInActive(string workflowId)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostWorkFlows + "/" + workflowId +
                                                         "/inactive");
        }

        public static RestResponse CreateSetPlan(string bodyClass)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostSetPlan, bodyClass);
        }

        public static RestResponse PostPlanToSet(string setId, string bodyClass)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostPlansToSet + setId + "/workflows",
                bodyClass);
        }

        public static RestResponse CopyWorkFlowSetPlanToSet(string setId)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostCopyWorkFlowSetPlanToSet + setId +
                                                         "/copy");
        }

        public static RestResponse OptInToWorkFlowSet(string setId)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostCopyWorkFlowSetPlanToSet + setId +
                                                         "/optin");
        }

        public static RestResponse OptOutToWorkFlowSet(string setId)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostCopyWorkFlowSetPlanToSet + setId +
                                                         "/optout");
        }

        public static RestResponse PostConfigurations(string bodyClass)
        {
            return Utilities.RestApiManager.StepPostData(AutomationEndPoints.PostConfigurations, bodyClass);
        }
    }
}