namespace APIComparer.Shared
{
    using System;
    using System.Configuration;

    internal class AzureEnvironment
    {
        public static string GetConnectionString()
        {
            var connectionString = IsRunningInCloud() ?
                ConfigurationManager.ConnectionStrings["NServiceBus/Transport"].ConnectionString
                : Environment.GetEnvironmentVariable("APICoparer.ConnectionString", EnvironmentVariableTarget.User);

            return connectionString;
        }

        public static bool IsRunningInCloud()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
        }

        public static string GetTempPath()
        {
            return Environment.GetEnvironmentVariable("TEMP");
        }
    }
}
