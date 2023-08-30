using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using Utilities;

namespace TestSuiteApi
{
    public class GlobalUtilities
    {
        public static string AppCenterUrl = "https://api.appcenter.ms/v0.1/apps";
        public static string AppCenterUploadUrl = "https://api-cloud.browserstack.com/app-automate/upload";
        public static string AppCenterApiKey = "b2faee8c5dda7d779677b0cc3416855519855c11";
        public static string FcosRefreshToken = "eyJraWQiOiJiWllPUmJKaXo1czdMNmFGY3hmSHZaVC1MUUlUa0hfMGhEdGVVekxwN29nIiwidmVyIjoiMS4wIiwiemlwIjoiRGVmbGF0ZSIsInNlciI6IjEuMCJ9.e3YbwHZmL6x4eL4xlUn9EoHGJPFTEilmjFY--JTwNfaXgnVS32nbk9VlZeW8YHAagUTAE0_t36lpzuXl_zMDGmg-8hYKGrSYNZyZTub8E3xh34BaHY5RqCaEaNIsQ5BliqY929PreCA7PRl2FdnNEaXCXyxcjPAudnnmQm4K1YQL0Ha2cHYVRJJqNm8nCuU4-sT2DVklcR-yDuM2Q34fGk8S3USCBPAr-0oqdVjNcrNJAwnbIdnPC7_reTAg6vncxaSbThqerfY_wB3AkfkVmOo7Mh1KGi46azWCR7EsBufvnCJFuXrzj1It33jYKtdwH2sa6P1dzTwkMapGasqh-Q.20GUesnoautb2vse.8D3h4MAPh7u-L-oVIZn7rVCASb26gxZ--fL0WP-zcGyhHaEVz25jP7cSHhzWrEAUXpNpGkh1-eOpzdHNRQg0Tfobx4Rucd3L2sAzAO1p5bDRvJJGp3DQhJHllgdAcXJxdtQ8j1lZ8on5iv44LB9p8qhYibTv08tuH_oSsqmrrOXdSnvV0zCWtvAppRtB8nkFxjqvQCXMopy6Pr6k3x2KENe-UPRbcmjdnSUxqdlT12dxx3QPNXGkrSNJOufRbXlnltXFRYLSkulkMxXDltYc7RItp9SSpeGuRwBAAf9y5gI2k953DHP3B7UImHzuxY-qcZ4Z7siPVUYgnbKaoOqk0MIJA9UP85GudOwYi02QbFudEpj5EFgvm9HwbybRcjm2gajQRL7bhX3UD_x_LssuEiUNM9z52G1-K7-PBczPDVGYkQFahd48FfVb7bK6z5YnOtqF5pHLiMJlT3LNpha_g6LQkrsihjzOLsYew1lXCAr0BQeQsGqW1dOXzT4AiPC_ata-UCPNgVhaYifHaEG1sEANw6eVft1_lbr2z_Gas6C6I7-216WY-llCUsyyZRMoCzTpfJ8tgf5LRLlrI3_buqzqhNLF24oGY2_1ebMnaXy0rUyvsUd-BCWXOcViCr4nqeALju2bSRQ9O4arZ87QQYs-1u7QAUjs_7N2F8qYQFcYMKi0CXI332BNIe7cd5yrNvkV.oepqIhOLV_eiQtwN-H1ARA";

        public static void GetApiToken()
        {
            TestInMemoryParametersShared.Instance.TestIdentifierStartTime = DateTime.Now;
            if (Environment.GetEnvironmentVariable("apiToken") != null &&
                Environment.GetEnvironmentVariable("apiToken") != "") return;
            RestResponse response;
            if (Environment.GetEnvironmentVariable("api_url") == null ||
                Environment.GetEnvironmentVariable("api_url") == "")
            {
                response = RestApiManager.StepGetDataFullUrl(string.Format("https://fcos-uat-app.azurewebsites.net/api/account/accessToken?refreshToken={0}", FcosRefreshToken));
            }
            else
            {
                response = RestApiManager.StepGetDataFullUrl(
                    Environment.GetEnvironmentVariable("api_url") +
                    string.Format("/api/account/accessToken?refreshToken={0}", FcosRefreshToken));
            }

            var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
            var tokenObject = JObject.Parse(obj);
            Environment.SetEnvironmentVariable("apiToken", tokenObject.access_token.ToString());
        }
    }
}
