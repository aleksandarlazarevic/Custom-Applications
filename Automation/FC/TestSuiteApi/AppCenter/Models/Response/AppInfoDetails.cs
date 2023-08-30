namespace TestSuiteApi.AppCenter.Models.Response
{
    public class AppInfoDetails
	{
		public string App_Name { get; set; }
		public string App_Display_Name { get; set; }
		public string App_Os { get; set; }
		public string App_Icon_Url { get; set; }
		public OwnerInfoDetails Owner { get; set; }
		public bool Is_External_Build { get; set; }
		public string Origin { get; set; }
		public string Id { get; set; }
		public string Version { get; set; }
		public string Short_Version { get; set; }
		public string Size { get; set; }
		public string Min_Os { get; set; }
		public string Device_Family { get; set; }
		public string Bundle_Identifier { get; set; }
		public string Fingerprint { get; set; }
		public string Uploaded_At { get; set; }
		public string Download_Url { get; set; }
		public string Install_Url { get; set; }
		public bool Mandatory_Update { get; set; }
		public bool Enabled { get; set; }
		public string FileExtension { get; set; }
		public bool Is_Latest { get; set; }
		public string Provisioning_Profile_Name { get; set; }
		public string Provisioning_Profile_Type { get; set; }
		public string Release_Notes { get; set; }
		public string[] Package_Hashes { get; set; }
		public string Destination_Type { get; set; }
		public string Status { get; set; }
		public string Distribution_Group_Id { get; set; }
		public DistributionGroupDetails[] Distribution_Groups { get; set; }
    }
}
