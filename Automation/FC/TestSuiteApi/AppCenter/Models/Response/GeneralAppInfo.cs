namespace TestSuiteApi.AppCenter.Models.Response
{
    public class GeneralAppInfo
    {
        public string Id { get; set; }

        public string App_Secret { get; set; }

        public string Description { get; set; }

        public string Display_Name { get; set; }

        public string Name { get; set; }

        public string Os { get; set; }

        public string Platform { get; set; }

        public string Origin { get; set; }

        public string Icon_Url { get; set; }

        public string Created_At { get; set; }

        public string Updated_At { get; set; }

        public string Release_Type { get; set; }

        public GeneralOwnerInfo Owner { get; set; }

        public string Azure_Subscription { get; set; }

        public string[] Member_Permissions { get; set; }
    }
}
