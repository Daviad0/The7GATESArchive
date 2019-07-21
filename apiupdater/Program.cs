using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using The7GATESArchive;
using The7GATESArchive.DAL;

namespace apiupdater
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
            Console.WriteLine("importing users from api...");
            var updater = new ApiUpdater();
            using (var context = new GatewayContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;

                updater.CreateGates(context);
                while (true) { 
                    updater.UpdateFromApi(context);
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }
    }
}
