namespace APIComparer.Website
{
    using APIComparer.Shared;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    public class AzureSubscriptionStorageConfigProvider : IProvideConfiguration<AzureSubscriptionStorageConfig>
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