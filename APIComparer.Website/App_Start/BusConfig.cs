namespace APIComparer.Website
{
    using Autofac;
    using NServiceBus;
    using NServiceBus.Features;

    public static class BusConfig
    {
        public static IBus Setup(IContainer container)
        {
            var configuration = new BusConfiguration();
            configuration.UseContainer<AutofacBuilder>(x => x.ExistingLifetimeScope(container));
            configuration.AssembliesToScan(AllAssemblies.Except("Microsoft.Windows.Azure.Storage"));

            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();

            var startableBus = Bus.Create(configuration);
            return startableBus.Start();
        }
    }
}