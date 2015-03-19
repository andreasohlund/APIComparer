namespace APIComparer.Website
{
    using System;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    public class AzureSubscriptionStorageConfigProvider : IProvideConfiguration<AzureSubscriptionStorageConfig>
    {
        public AzureSubscriptionStorageConfig GetConfiguration()
        {
            return new AzureSubscriptionStorageConfig
            {
                ConnectionString = Environment.GetEnvironmentVariable("AzureStorageQueueTransport.ConnectionString")
            };
        }
    }
}