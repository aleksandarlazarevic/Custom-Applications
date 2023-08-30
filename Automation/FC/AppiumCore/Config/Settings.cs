using AppiumCore.Base;

namespace AppiumCore.Config
{
    public class Settings
    {
        public static string BrowserStackUser { get; set; }
        public static string BrowserStackKey { get; set; }
        public static string ChromeDriverPath { get; set; }
        public static PlatformName PlatformName { get; set; }
        public static string DeviceName { get; set; }
        public static MobileType MobileType { get; set; }
        public static string PlatformVersion { get; set; }
        public static string BrowserStackAppIdentifier { get;  set; }
    }
}