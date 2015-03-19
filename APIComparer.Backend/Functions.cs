namespace APIComparer.Backend
{
    using System;
    using System.IO;
    using System.Threading;
    using Microsoft.Azure.WebJobs;
    using NServiceBus;
    using NServiceBus.Features;

    public class Functions
    {
        [NoAutomaticTrigger]
        public static void Host(TextWriter log, CancellationToken cancellationToken)
        {
            var configuration = new BusConfiguration();
            configuration.EndpointName("APIComparer.Backend");
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();

            configuration.AzureConfigurationSource();
            configuration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString(Environment.GetEnvironmentVariable("AzureStorageQueueTransport.ConnectionString"));
            configuration.UsePersistence<AzureStoragePersistence>();
            //configuration.ApplyMessageConventions();
            
            var startableBus = Bus.Create(configuration);
            startableBus.Start();
            log.WriteLine("APIComparer.Backend - bus started");

            cancellationToken.WaitHandle.WaitOne();

            startableBus.Dispose();
            log.WriteLine("APIComparer.Backend cancelled at " + DateTimeOffset.UtcNow);
        }
    }
}
