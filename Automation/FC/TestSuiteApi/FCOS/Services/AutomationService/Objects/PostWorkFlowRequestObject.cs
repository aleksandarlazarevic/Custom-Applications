using System.Collections.Generic;
using SpecFlow.Internal.Json;
using TestSuiteApi.FCOS.Services.AutomationService.Models.Request;

namespace TestSuiteApi.FCOS.Services.AutomationService.Objects
{
    public static class PostWorkFlowRequestObject
    {
        private static Trigger TriggerObject()
        {
            var trigger = new Trigger();
            trigger.Id = "bbac44e0-9d23-41de-87c8-b25a6c3d2b58";
            trigger.Display = "a ticket is created";
            trigger.EntityType = "ticket";
            trigger.Action = "created";
            return trigger;
        }

        private static Filter FilterObject()
        {
            var filter = new Filter();
            filter.Field = "Ticket.IsPublic";
            filter.Relation = "EQ";
            filter.Values = new List<string> { "true" };
            return filter;
        }

        private static Configuration ConfigurationObject()
        {
            var configurationObject = new Configuration();
            configurationObject.Template = "";
            configurationObject.FromEntityId = null;
            configurationObject.FromEntityType = "user";
            configurationObject.FromName = "Ali Said";
            configurationObject.NodeIsTriggeringEntity = false;
            configurationObject.AdhocEmailSubject = "Automation Subject";
            configurationObject.AdhocEmailTemplate = "<p>Automation Body<br><br></p>";
            return configurationObject;
        }

        private static Action ActionObject()
        {
            var configurationObject = ConfigurationObject();
            var action = new Action();
            action.DelayInMinutes = null;
            action.action = "sendEmail";
            action.TargetEntityType = null;
            action.TargetTriggeringEntity = false;
            action.IsActive = true;
            action.Configuration = configurationObject;
            action.TargetTriggeringEntityField = null;
            action.TargetEntityId = null;
            action.CustomTarget = "fcos.automation@franchiczar.com";
            return action;
        }

        public static string CreateWorkFlowObject()
        {
            var trigger = TriggerObject();
            var filter = FilterObject();
            var action = ActionObject();
            var postWorkFlowRequestModel = new PostWorkFlowRequestModel
            {
                Id = null,
                Name = "Automation API WorkFlow",
                NodeId = "849a81c7-6fd5-4250-ac90-a80e3da0d6d7",
                NodeName = "FranchiCzar",
                CurrentUserCanEdit = true,
                IsActive = false,
                IsDraft = false,
                Trigger = trigger,
                Filters = new List<Filter> { filter },
                Actions = new List<Action> { action },
                TriggerId = "bbac44e0-9d23-41de-87c8-b25a6c3d2b58"
            };
            return postWorkFlowRequestModel.ToJson();
        }
    }
}