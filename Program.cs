using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoaderService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConfiguratorWebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var loaderProxy = services.GetRequiredService<LoaderServiceClient>();
                    
                    // Get designs in advance
                    var designs = Task.Run(async () =>
                        await loaderProxy.GetDesignsAsync()).Result;

                    //var designInstance = Task.Run(async () =>
                    //    await loaderProxy.CreateDesignInstanceAsync(designs[0])).Result;


                    //var design = designInstance.Design;
                    //var group = design.PropertyGroups[0];
                    //group.Properties[2].Value = "16";
                    //group.Properties[3].Value = "10000";

                    //var instance = designInstance;

                    //designInstance = Task.Run(async () =>
                    //    await loaderProxy.UpdateDesignInstanceAsync(instance)).Result;

                }
                catch (Exception)
                {
                    //throw;
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
