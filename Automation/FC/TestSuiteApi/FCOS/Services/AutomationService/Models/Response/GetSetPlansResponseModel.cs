using System;

namespace TestSuiteApi.FCOS.Services.AutomationService.Models.Response
{
    public class GetSetPlansResponseModel
    {
        public string id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string nodeId { get; set; }

        public string nodeName { get; set; }

        public string createdUsername { get; set; }
    }
}