namespace APIComparer.Website
{
    using Autofac;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Logging;
    using Shared;

    public static class BusConfig
    {
        public static IBus Setup(ILifetimeScope container)
        {
            LogManager.Use<DefaultFactory>();

            var configuration = new BusConfiguration();
            configuration.EndpointName("APIComparer.Website");
            configuration.UseContainer<AutofacBuilder>(x => x.ExistingLifetimeScope(container));
            configuration.AssembliesToScan(AllAssemblies.Except("Microsoft.Windows.Azure.Storage"));

            configuration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString(AzureEnvironment.GetConnectionString);
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();
            configuration.EnableInstallers();

            return Bus.Create(configuration).Start();
        }
    }
}