using AppiumCore.Config;
using Newtonsoft.Json;
using RestSharp;
using SeleniumCore.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TestSuiteApi;
using TestSuiteApi.AppCenter.Models.Response;
using TestSuiteApi.FCOS.Services.ImeiBindings.Models.Response;
using TestSuiteApi.FCOS.Utils;
using Utilities;

namespace CommonTestSteps.TestSteps.Mobile
{
    public class GlobalMobileTestSteps
    {
        public static void RemoveImeiBindings(string email)
        {
            GlobalUtilities.GetApiToken();
            RestResponse response = ImeiBindingsUtils.GetAllDevices();
            string boundUserId = string.Empty;
            List<UserDetailsModel> userDetailsList = JsonConvert.DeserializeObject<List<UserDetailsModel>>(response.Content);

            if (userDetailsList.Any(p => p.EmailAddress.Contains(email, StringComparison.OrdinalIgnoreCase)))
            {
                boundUserId = userDetailsList.Where(p => p.EmailAddress.Contains(email, StringComparison.OrdinalIgnoreCase)).Select(p => p.DeviceId).First();
                ImeiBindingsUtils.RemoveImeiBinding(boundUserId);
            }
        }

        public static void UploadLatestAppVersionFromAppCenter(string client, string environment, string os)
        {
            List<GeneralAppInfo> appInfoList = GetAvailableAppsFromAppCenter();
            GeneralAppInfo appInfo = appInfoList.Where(p => p.Name.Contains(client) && 
                                                                               p.Name.Contains(environment) && 
                                                                               p.Os.Equals(os)).First();

            GlobalUtilities.AppCenterUrl = string.Format("https://api.appcenter.ms/v0.1/apps/{0}/{1}/releases/latest", appInfo.Owner.Name, appInfo.Name );
            AppInfoDetails appInfoDetails = GetSpecificAppDetailsFromAppCenter();
            string downloadUrl = appInfoDetails.Download_Url;
            AppUploadResponse appUploadResponse = UploadTheBuildOnBrowserStack(downloadUrl);
            Settings.BrowserStackAppIdentifier = appUploadResponse.App_Url;
        }

        private static AppUploadResponse UploadTheBuildOnBrowserStack(string downloadUrl)
        {
            RestApiManager.ServiceUrl = GlobalUtilities.AppCenterUploadUrl;
            Dictionary<string, string> listOfParameters = new Dictionary<string, string> { { "url", downloadUrl } };
            RestResponse response =  RestApiManager.StepPostDataBasicAuth(Settings.BrowserStackUser, Settings.BrowserStackKey, listOfParameters);
            AppUploadResponse appUploadResponse = JsonConvert.DeserializeObject<AppUploadResponse>(response.Content);

            return appUploadResponse;
        }

        public static List<GeneralAppInfo> GetAvailableAppsFromAppCenter()
        {
            List<GeneralAppInfo> appDetailsList = JsonConvert.DeserializeObject<List<GeneralAppInfo>>(GetAppInfoFromAppCenter().Content);
            return appDetailsList;
        }

        public static AppInfoDetails GetSpecificAppDetailsFromAppCenter()
        {
            AppInfoDetails appDetails = JsonConvert.DeserializeObject<AppInfoDetails>(GetAppInfoFromAppCenter().Content);
            return appDetails;
        }

        private static RestResponse GetAppInfoFromAppCenter()
        {
            RestApiManager.ServiceUrl = GlobalUtilities.AppCenterUrl;
            Dictionary<string, string> listOfHeaders = new Dictionary<string, string> { { "X-Api-Token", GlobalUtilities.AppCenterApiKey } };
            return RestApiManager.StepGetDataWithHeader(listOfHeaders);
        }
    }
}
