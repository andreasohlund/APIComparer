namespace APIComparer.Backend
{
    using System;
    using System.IO;
    using System.Threading;
    using APIComparer.Shared;
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


            configuration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString(AzureEnvironment.GetConnectionString);
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.EnableInstallers();

            using (Bus.Create(configuration).Start())
            {
                log.WriteLine("APIComparer.Backend - bus started");

                cancellationToken.WaitHandle.WaitOne();
            }

            log.WriteLine("APIComparer.Backend cancelled at " + DateTimeOffset.UtcNow);
        }
    }
}
