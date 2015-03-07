using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace APIComparer.Backend
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            JobHost host;
            string connectionString;
            // To run webjobs locally, can't use storage emulator
            // for local execution, use connection string stored in environment vatiable
            if ((connectionString = Environment.GetEnvironmentVariable("AzureStorageQueueTransport.ConnectionString")) != null)
            {
                var configuration = new JobHostConfiguration
                {
                    DashboardConnectionString = connectionString,
                    StorageConnectionString = connectionString
                };
                host = new JobHost(configuration);
            }
            // for production, use DashboardConnectionString and StorageConnectionString defined at Azure website
            else
            {
                host = new JobHost();
            }

            Console.WriteLine("Starting Host with NSB");
            host.Call(typeof(Functions).GetMethod("Host"));
            host.RunAndBlock();
        }
    }
}
