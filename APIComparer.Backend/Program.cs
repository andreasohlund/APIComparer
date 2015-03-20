namespace APIComparer.Backend
{
    using System;
    using APIComparer.Shared;
    using Microsoft.Azure.WebJobs;

    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            JobHost host;
            if (AzureEnvironment.IsRunningInCloud())
            {
                host = new JobHost();
            }
            else
            {
                // To run webjobs locally, can't use storage emulator
                // for local execution, use connection string stored in environment variable
                var configuration = new JobHostConfiguration
                {
                    DashboardConnectionString = AzureEnvironment.GetConnectionString(),
                    StorageConnectionString = AzureEnvironment.GetConnectionString()
                };
                host = new JobHost(configuration);
            }

            Console.WriteLine("Starting Host with NSB");
            host.Call(typeof(Functions).GetMethod("Host"));
            host.RunAndBlock();
        }
    }
}
