using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RioSuaveAPI.ProtectedJsonConfiguration;

namespace RioSuaveAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddProtectedJsonConfiguration)
                .UseStartup<Startup>();

        private static void AddProtectedJsonConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            // We add appsettings again (in addition to CreateDefaultBuilder) due to ordering
            // Would be nice if we could avoid reading in multiple times -- consider using an encrypted DEV file?
            // TODO: Add file input here instead of in the AddProtectedJsonConfiguration() method
            builder.AddProtectedJsonConfiguration()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true,
                    reloadOnChange: true);
        }
    }
}
