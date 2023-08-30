using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Response
{
    [Binding]
    public class GetWorkFlowResponseModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nodeId { get; set; }
        public string nodeName { get; set; }
        public object setId { get; set; }
        public object setName { get; set; }
        public string userCreatedId { get; set; }
        public string userCreatedName { get; set; }
        public string trigger { get; set; }
        public bool isActive { get; set; }
        public bool isDraft { get; set; }
        public bool isOptIn { get; set; }
        public List<Filter> filters { get; set; }
        public List<object> actions { get; set; }   
    }

    public partial class Filter
    {
        public string id { get; set; }
        public string field { get; set; }
        public string relation { get; set; }
        public List<string> values { get; set; }
    }
}

