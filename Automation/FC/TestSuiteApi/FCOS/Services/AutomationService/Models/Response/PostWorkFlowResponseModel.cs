using System;
using Newtonsoft.Json;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Response
{
    public class PostWorkFlowResponseModel
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("nodeId")] public string NodeId { get; set; }

        [JsonProperty("nodeName")] public string NodeName { get; set; }

        [JsonProperty("setId")] public object SetId { get; set; }

        [JsonProperty("setName")] public object SetName { get; set; }

        [JsonProperty("isActive")] public bool IsActive { get; set; }

        [JsonProperty("isDraft")] public bool IsDraft { get; set; }

        [JsonProperty("currentNodeHasOptedIn")]
        public object CurrentNodeHasOptedIn { get; set; }

        [JsonProperty("optedInByNodeId")] public object OptedInByNodeId { get; set; }

        [JsonProperty("currentUserCanEdit")] public object CurrentUserCanEdit { get; set; }

        [JsonProperty("trigger")] public Trigger Trigger { get; set; }

        [JsonProperty("filters")] public Request.Filter[] Filters { get; set; }

        [JsonProperty("actions")] public Action[] Actions { get; set; }
    }

    public class Action
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("delayInMinutes")] public object DelayInMinutes { get; set; }

        [JsonProperty("delayRelativeTo")] public object DelayRelativeTo { get; set; }

        [JsonProperty("action")] public string ActionAction { get; set; }

        [JsonProperty("targetEntityType")] public object TargetEntityType { get; set; }

        [JsonProperty("targetTriggeringEntity")]
        public bool TargetTriggeringEntity { get; set; }

        [JsonProperty("targetEntityId")] public object TargetEntityId { get; set; }

        [JsonProperty("targetTriggeringEntityField")]
        public object TargetTriggeringEntityField { get; set; }

        [JsonProperty("customTarget")] public string CustomTarget { get; set; }

        [JsonProperty("isActive")] public bool IsActive { get; set; }

        [JsonProperty("configuration")] public Configuration Configuration { get; set; }

        [JsonProperty("state")] public object State { get; set; }

        [JsonProperty("timezone")] public object Timezone { get; set; }

        [JsonProperty("timeOfDay")] public TimeOfDay TimeOfDay { get; set; }
    }

    public class Configuration
    {
        [JsonProperty("Template")] public string Template { get; set; }

        [JsonProperty("FromEntityType")] public string FromEntityType { get; set; }

        [JsonProperty("FromName")] public string FromName { get; set; }

        [JsonProperty("NodeIsTriggeringEntity")]
        public bool NodeIsTriggeringEntity { get; set; }

        [JsonProperty("AdhocEmailSubject")] public string AdhocEmailSubject { get; set; }

        [JsonProperty("AdhocEmailTemplate")] public string AdhocEmailTemplate { get; set; }
    }

    public class TimeOfDay
    {
        [JsonProperty("hour")] public long Hour { get; set; }

        [JsonProperty("minute")] public long Minute { get; set; }

        [JsonProperty("second")] public long Second { get; set; }
    }

    public partial class Filter
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("field")] public string Field { get; set; }

        [JsonProperty("relation")] public string Relation { get; set; }

        [JsonProperty("values")] public bool[] Values { get; set; }
    }

    public class Trigger
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("display")] public string Display { get; set; }

        [JsonProperty("entityType")] public string EntityType { get; set; }

        [JsonProperty("action")] public string Action { get; set; }

        [JsonProperty("filters")] public object[] Filters { get; set; }
    }
}