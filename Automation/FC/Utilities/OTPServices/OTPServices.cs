using System.Text.Json;
using RestSharp;

namespace Utilities.OTPServices
{
    public static class OtpServices
    {
        private static RestResponse GetSmsList()
        {
            return RestApiManager.StepGetDataFullUrl("https://getnada.com/api/v1/inboxes/fcos-automation@getnada.com");
        }


        public static string GetSmsBody()
        {
            MailListModel mailListModel = JsonSerializer.Deserialize<MailListModel>(GetSmsList().Content);
            return RestApiManager
                .StepGetDataFullUrl("https://getnada.com/api/v1/messages/html/" + mailListModel.msgs[0].uid).Content;
        }
    }
}