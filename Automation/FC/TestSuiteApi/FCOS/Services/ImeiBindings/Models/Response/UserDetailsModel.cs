using Newtonsoft.Json;

namespace TestSuiteApi.FCOS.Services.ImeiBindings.Models.Response
{
    public class UserDetailsModel
    {
        public string UserGuid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string ActiveDevice { get; set; }

        public string DeviceId { get; set; }
    }
}
