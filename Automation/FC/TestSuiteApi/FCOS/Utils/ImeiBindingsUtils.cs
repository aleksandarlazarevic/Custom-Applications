using RestSharp;
using System;
using TestSuiteApi.FCOS.Services.ImeiBindings;

namespace TestSuiteApi.FCOS.Utils
{
    public class ImeiBindingsUtils
    {
        public static RestResponse GetAllDevices()
        {
            return Utilities.RestApiManager.StepGetData(DeviceEndpoints.GetUserDevices);
        }

        public static RestResponse RemoveImeiBinding(string deviceId)
        {
            return Utilities.RestApiManager.StepPutData(DeviceEndpoints.RemoveUserDevice + deviceId);
        }
    }
}
