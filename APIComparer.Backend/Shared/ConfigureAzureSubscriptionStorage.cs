namespace APIComparer.Shared
{
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class ConfigureAzureSubscriptionStorage : IProvideConfiguration<AzureSubscriptionStorageConfig>
    {
        public AzureSubscriptionStorageConfig GetConfiguration()
        {
            return new AzureSubscriptionStorageConfig
            {
                ConnectionString = AzureEnvironment.GetConnectionString()
            };
        }
    }
}