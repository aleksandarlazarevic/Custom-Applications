using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TestSuiteApi.FCOS.Services.AutomationService.Models.Response;
using TestSuiteApi.FCOS.Services.AutomationService.Objects;
using TestSuiteApi.FCOS.Utils;
using Utilities.OTPServices;

namespace TestSuiteApi.FCOS.Steps
{
    [Binding]
    public class AutomationServiceStepDef
    {
        private RestResponse _response;

        [Given(@"Automation Service: Get WorkFlows")]
        public void GivenAutomationServiceGetWorkFlows()
        {
            _response = AutomationServiceUtils.GetWorkFlows();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Get the Automation Webhook Url")]
        public void GivenAutomationServiceGetAutomationWebhookUrl()
        {
            _response = AutomationServiceUtils.GetAutomationWebHockUrl();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"Automation Service: Get WorkFlow By ID")]
        public void GivenAutomationServiceGetWorkFlowById()
        {
            List<GetWorkFlowResponseModel> getWorkFlow =
                JsonSerializer.Deserialize<List<GetWorkFlowResponseModel>>(_response.Content);
            _response = AutomationServiceUtils.GetWorkFlowById(getWorkFlow[0].id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Get SetPlans")]
        public void GivenAutomationServiceGetAutomationGetSetPlans()
        {
            _response = AutomationServiceUtils.GetSetPlans();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"Automation Service: Get SetPlan By ID")]
        public void GivenAutomationServiceGetAutomationSetPlanById()
        {
            List<GetSetPlansResponseModel> getSetPlans =
                JsonSerializer.Deserialize<List<GetSetPlansResponseModel>>(_response.Content);
            _response = AutomationServiceUtils.GetSetPlanById(getSetPlans[0].id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"Automation Service: Get WorkFlow SetPlan By Work Flow ID")]
        public void GivenAutomationServiceGetAutomationGetWorkFlowSetPlanById()
        {
            List<GetWorkFlowResponseModel> getWorkFlow =
                JsonSerializer.Deserialize<List<GetWorkFlowResponseModel>>(_response.Content);
            _response = AutomationServiceUtils.GetWorkflowSetPlanById(getWorkFlow[0].id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Get Triggers")]
        public void GivenAutomationServiceGetTriggers()
        {
            _response = AutomationServiceUtils.GetTriggers();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Get Configurations")]
        public void GivenAutomationServiceGetConfigurations()
        {
            _response = AutomationServiceUtils.GetConfiguration();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Get WebHook Test")]
        public void GivenAutomationServiceGetWebHookTest()
        {
            _response = AutomationServiceUtils.GetWebHookTest();
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Create WorkFlow")]
        public void GivenAutomationServicePostWorkFlow()
        {
            _response = AutomationServiceUtils.CreateWorkFlow(PostWorkFlowRequestObject.CreateWorkFlowObject());
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"Automation Service: Set WorkFlow Active")]
        public void GivenAutomationServiceSetWorkFlowActive()
        {
            PostWorkFlowResponseModel getWorkFlow =
                JsonSerializer.Deserialize<PostWorkFlowResponseModel>(_response.Content);
            _response = AutomationServiceUtils.SetWorkFlowActive(getWorkFlow.Id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"Automation Service: Set WorkFlow In-Active")]
        public void GivenAutomationServiceSetWorkFlowInActive()
        {
            PostWorkFlowResponseModel getWorkFlow =
                JsonSerializer.Deserialize<PostWorkFlowResponseModel>(_response.Content);
            _response = AutomationServiceUtils.SetWorkFlowInActive(getWorkFlow.Id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Create SetPlan")]
        public void GivenAutomationServicePostSetPlan()
        {
            _response = AutomationServiceUtils.CreateSetPlan(PostSetPlanObject.SetPlanObject());
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }


        [Then(@"Automation Service: Add Plan To Set")]
        public void GivenAutomationServicePostPlanToSet()
        {
            PostWorkFlowResponseModel getWorkFlow =
                JsonSerializer.Deserialize<PostWorkFlowResponseModel>(_response.Content);
            _response = AutomationServiceUtils.CreateSetPlan(PostSetPlanObject.SetPlanObject());
            PostSetPlanResponseModel setPlan =
                JsonSerializer.Deserialize<PostSetPlanResponseModel>(_response.Content);
            _response = AutomationServiceUtils.PostPlanToSet(setPlan.Id, getWorkFlow.ToJson());
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Copy WorkFlow SetPlan To WorkFlow Set")]
        public void GivenAutomationServiceCopyWorkFlowSetPlanToSet()
        {
            _response = AutomationServiceUtils.CreateSetPlan(PostSetPlanObject.SetPlanObject());
            PostSetPlanResponseModel setPlan =
                JsonSerializer.Deserialize<PostSetPlanResponseModel>(_response.Content);
            _response = AutomationServiceUtils.CopyWorkFlowSetPlanToSet(setPlan.Id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: OptIn To WorkFlow Set")]
        public void GivenAutomationServiceOptInWorkFlowSet()
        {
            _response = AutomationServiceUtils.CreateSetPlan(PostSetPlanObject.SetPlanObject());
            PostSetPlanResponseModel setPlan =
                JsonSerializer.Deserialize<PostSetPlanResponseModel>(_response.Content);
            _response = AutomationServiceUtils.OptInToWorkFlowSet(setPlan.Id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: OptOut To WorkFlow Set")]
        public void GivenAutomationServiceOptOutWorkFlowSet()
        {
            _response = AutomationServiceUtils.CreateSetPlan(PostSetPlanObject.SetPlanObject());
            PostSetPlanResponseModel setPlan =
                JsonSerializer.Deserialize<PostSetPlanResponseModel>(_response.Content);
            _response = AutomationServiceUtils.OptOutToWorkFlowSet(setPlan.Id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [Given(@"Automation Service: Create Configurations")]
        public void GivenAutomationServiceCreateConfigurations()
        {
            _response = AutomationServiceUtils.PostConfigurations(PostConfigurationsObject.ConfigurationsRequestBody());
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }
    }
}