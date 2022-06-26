using AppiumCore.Base;

namespace AppiumCore.Config
{
    class Settings
    {
        public static string AUTPath { get; set; }
        public static string ChromeDriverPath { get; set; }
        public static PlatformName PlatformName { get; set; }
        public static string DeviceName { get; set; }
        public static MobileType MobileType { get; set; }
    }
}
