using System.Dynamic;
using System.Text.Json.Nodes;

namespace CommonCore.Configuration
{
    public class SystemConfiguration
    {
        public string Tag { get; set; }
        public DriverConfiguration[] Capabilities { get; set; }
    }
}
