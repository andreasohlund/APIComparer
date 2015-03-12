namespace APIComparer.Website
{
    using System.Configuration;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    public class AzureSubscriptionStorageConfigProvider : IProvideConfiguration<AzureSubscriptionStorageConfig>
    {
        public AzureSubscriptionStorageConfig GetConfiguration()
        {
            return new AzureSubscriptionStorageConfig
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["NServiceBus/Transport"].ConnectionString,
            };
        }
    }
}