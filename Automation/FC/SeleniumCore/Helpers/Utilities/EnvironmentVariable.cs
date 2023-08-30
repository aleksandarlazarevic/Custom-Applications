using System;
namespace SeleniumCore.Helpers.Utilities
{
    public static class EnvironmentVariable
    {

        public static string GetEnvironmentVariable(string name, string defaultValue)
    => Environment.GetEnvironmentVariable(name) is string v && v.Length > 0 ? v : defaultValue;

    }
}

