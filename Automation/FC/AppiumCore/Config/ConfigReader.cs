using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppiumCore.Config
{
    public class ConfigReader
    {
        public static void InitializeSettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            IConfigurationRoot configurationRoot = builder.Build();

            Settings.BrowserStackUser = configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserstackUser;
            Settings.BrowserStackKey = configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserstackKey;
            Settings.BrowserStackAppIdentifier = configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserStackAppIdentifier;
            Settings.PlatformName = configurationRoot.GetSection("testSettings").Get<TestSettings>().PlatformName;
            Settings.PlatformVersion = configurationRoot.GetSection("testSettings").Get<TestSettings>().PlatformVersion;
            Settings.ChromeDriverPath = configurationRoot.GetSection("testSettings").Get<TestSettings>().ChromeDriverPath;
            Settings.DeviceName = configurationRoot.GetSection("testSettings").Get<TestSettings>().DeviceName;
            Settings.MobileType = configurationRoot.GetSection("testSettings").Get<TestSettings>().MobileType;
        }
    }
}