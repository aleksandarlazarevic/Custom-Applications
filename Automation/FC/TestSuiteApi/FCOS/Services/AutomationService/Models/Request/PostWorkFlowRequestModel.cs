using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Request
{
    public class PostWorkFlowRequestModel
    {
        [JsonProperty("id")] [JsonIgnore] public object Id { get; set; }

        [JsonProperty("name")] [JsonIgnore] public string Name { get; set; }

        [JsonProperty("nodeId")] [JsonIgnore] public string NodeId { get; set; }

        [JsonProperty("nodeName")]
        [JsonIgnore]
        public string NodeName { get; set; }

        [JsonProperty("currentUserCanEdit")]
        [JsonIgnore]
        public bool CurrentUserCanEdit { get; set; }

        [JsonProperty("isActive")]
        [JsonIgnore]
        public bool IsActive { get; set; }

        [JsonProperty("isDraft")] [JsonIgnore] public bool IsDraft { get; set; }

        [JsonProperty("trigger")] [JsonIgnore] public Trigger Trigger { get; set; }

        [JsonProperty("filters")] [JsonIgnore] public List<Filter> Filters { get; set; }

        [JsonProperty("actions")] [JsonIgnore] public List<Action> Actions { get; set; }

        [JsonProperty("triggerId")]
        [JsonIgnore]
        public string TriggerId { get; set; }
    }

    public class Action
    {
        [JsonProperty("delayInMinutes")]
        [JsonIgnore]
        public object DelayInMinutes { get; set; }

       [JsonIgnore] public string action { get; set; }

        [JsonProperty("targetEntityType")]
        [JsonIgnore]
        public object TargetEntityType { get; set; }

        [JsonProperty("targetTriggeringEntity")]
        [JsonIgnore]
        public bool TargetTriggeringEntity { get; set; }

        [JsonProperty("isActive")]
        [JsonIgnore]
        public bool IsActive { get; set; }

        [JsonProperty("targetTriggeringEntityField")]
        [JsonIgnore]
        public object TargetTriggeringEntityField { get; set; }

        [JsonProperty("configuration")]
        [JsonIgnore]
        public Configuration Configuration { get; set; }

        [JsonProperty("targetEntityId")]
        [JsonIgnore]
        public object TargetEntityId { get; set; }

        [JsonProperty("customTarget")]
        [JsonIgnore]
        public string CustomTarget { get; set; }

        [JsonProperty("errors")] [JsonIgnore] public List<object> Errors { get; set; }
    }

    public class Configuration
    {
        [JsonProperty("template")]
        [JsonIgnore]
        public string Template { get; set; }

        [JsonProperty("fromEntityId")]
        [JsonIgnore]
        public object FromEntityId { get; set; }

        [JsonProperty("fromEntityType")]
        [JsonIgnore]
        public string FromEntityType { get; set; }

        [JsonProperty("fromName")]
        [JsonIgnore]
        public string FromName { get; set; }

        [JsonProperty("nodeIsTriggeringEntity")]
        [JsonIgnore]
        public bool NodeIsTriggeringEntity { get; set; }

        [JsonProperty("adhocEmailSubject")]
        [JsonIgnore]
        public string AdhocEmailSubject { get; set; }

        [JsonProperty("adhocEmailTemplate")]
        [JsonIgnore]
        public string AdhocEmailTemplate { get; set; }
    }

    public class Filter
    {
        [JsonProperty("field")] [JsonIgnore] public string Field { get; set; }

        [JsonProperty("relation")]
        [JsonIgnore]
        public string Relation { get; set; }

        [JsonProperty("values")] [JsonIgnore] public List<string> Values { get; set; }
    }

    public class Trigger
    {
        [JsonProperty("id")] [JsonIgnore] public string Id { get; set; }

        [JsonProperty("display")] [JsonIgnore] public string Display { get; set; }

        [JsonProperty("entityType")]
        [JsonIgnore]
        public string EntityType { get; set; }

        [JsonProperty("action")] [JsonIgnore] public string Action { get; set; }

        [JsonProperty("filters")] [JsonIgnore] public List<object> Filters { get; set; }
    }
}